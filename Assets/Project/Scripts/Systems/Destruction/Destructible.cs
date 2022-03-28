using System;
using System.Collections.Generic;

using DG.Tweening;

using Sirenix.OdinInspector;
using Sirenix.Utilities;

using UnityEngine;
using Random = UnityEngine.Random;

namespace TheFowler
{
    public class Destructible : SerializedMonoBehaviour, IDesctructible
    {
        public MeshFilter meshFilter;
        public MeshCollider collider;
        public DestructionIteration[] iterations;

        [SerializeField] private int currentIteration = 0;

        public bool DisableCollider = true;

        public bool Explosed = false;
        public Transform emissionPos;
        public float explosionForce;

        public List<DestructionData> destructionDatas;

        public Vector3 minTorque, maxTorque;

        [Button]
        private void Bake()
        {
            destructionDatas = new List<DestructionData>();

            for (int i = 0; i < iterations.Length; i++)
            {
                for (int j = 0; j < iterations[i].elements.Length; j++)
                {
                    destructionDatas.Add(new DestructionData()
                    {
                        t = iterations[i].elements[j].transform,
                        pos = iterations[i].elements[j].transform.position,
                        rot = iterations[i].elements[j].transform.rotation
                    });
                }
            }
        }

        [Button]
        public void Rebuild()
        {
            foreach (var e in destructionDatas)
            {
                e.t.TryGetComponent<Rigidbody>(out var rb);
                if (rb != null) rb.isKinematic = true;

                var rdm = Random.Range(0, 1f);
                
                e.t.DOMove(e.pos, rdm).SetEase(Ease.InOutBounce);
                e.t.DORotate(e.rot.eulerAngles, rdm).SetEase(Ease.InOutBounce);
            }

            currentIteration = 0;
        }

        [Button]
        public void Destruct()
        {
            if(currentIteration > iterations.Length - 1)
                return;

            meshFilter.mesh = null;
            if (DisableCollider && collider != null)
                collider.enabled = false;
           if(collider != null) collider.sharedMesh = iterations[currentIteration].newMesh;
            
            iterations[currentIteration].elements.ForEach(DestroyElement);
           
            currentIteration++;
        }

        public void DestroyElement(GameObject obj)
        {
            obj.SetActive(true);
            obj.TryGetComponent<Rigidbody>(out var rb);
            if (rb != null)
            {
                rb.isKinematic = false;
                if (Explosed)
                {
                    rb.AddExplosionForce(explosionForce, emissionPos.transform.position, 20f);
                    rb.AddTorque(Random.Range(minTorque.x, maxTorque.x), Random.Range(minTorque.y, maxTorque.y), Random.Range(minTorque.z, maxTorque.z), ForceMode.Impulse);
                }
            }
        }
    }

    [Serializable]
    public class DestructionIteration
    {
        public Mesh newMesh;
        public GameObject[] elements;
    }

    public class DestructionData
    {
        public Transform t;
        public Vector3 pos;
        public Quaternion rot;
    }
}
