using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace TheFowler
{
    public class SecurityFallBox : MonoBehaviour
    {
        public static SecurityFallBox Instance;
        public Transform playerSpawn;

        private void Awake()
        {
            Instance = this;
        }

        [Button]
        private void OnCollisionEnter(Collision other)
        {
            if(other.transform.GetComponent<ColliderChecker>() != null)
            {
               
                if (other.transform.GetComponent<ColliderChecker>().CompareFlag(ColliderFlag.PLAYER))
                {
                    other.transform.position = playerSpawn.position;
                }

            }
        }

    }
}

