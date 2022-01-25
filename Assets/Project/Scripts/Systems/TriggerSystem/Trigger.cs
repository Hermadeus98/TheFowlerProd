using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace TheFowler
{
    [RequireComponent(typeof(Rigidbody))]
    public class Trigger : SerializedMonoBehaviour
    {
        [FoldoutGroup("Flags")]
        [SerializeField] private ColliderFlag FlagToTrigger;

        [FoldoutGroup("Events")]
        [SerializeField] public UnityEvent onTriggerEnterFront, onTriggerEnterBack, onTriggerExit;

        [FoldoutGroup("Settings")]
        [SerializeField] private bool disableOnEnter = true;
        [FoldoutGroup("Settings")]
        [SerializeField] private bool disableOnExit = false;

        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ColliderChecker>(out var checker))
            {
                if (checker.CompareFlag(FlagToTrigger))
                {
                    Vector3 forward = transform.TransformDirection(Vector3.forward);
                    Vector3 toOther = checker.transform.position - transform.position;

                    if (Vector3.Dot(forward, toOther) < 0)
                    {
                        onTriggerEnterFront?.Invoke();
                    }
                    else
                    {
                        onTriggerEnterBack?.Invoke();
                    }
                    

                    if (disableOnEnter)
                        gameObject.SetActive(false);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<ColliderChecker>(out var checker))
            {
                if (checker.CompareFlag(FlagToTrigger))
                {
                    onTriggerExit?.Invoke();

                    if (disableOnExit)
                        gameObject.SetActive(false);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (TryGetComponent<BoxCollider>(out var boxCollider))
            {
                var c = Color.green;
                c.a = .2f;
                Gizmos.color = c;
                Gizmos.DrawCube(transform.position + boxCollider.center, boxCollider.bounds.size);
                ForGizmo(transform.position, transform.forward, Color.red);
            }
        }
        
        public static void ForGizmo(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(pos, direction);
       
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
        }
        
        //---<EDITOR>--------------------------------------------------------------------------------------------------<
#if UNITY_EDITOR
        [MenuItem("GameObject/LD/Trigger Event", false, 20)]
        private static void Create(MenuCommand menuCommand)
        {
            var obj = Resources.Load("LD/ColliderEvent");
            var go = PrefabUtility.InstantiatePrefab(obj, Selection.activeTransform) as GameObject;
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            go.name = obj.name;
            Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
            Selection.activeObject = go;
        }
#endif
    }
}
