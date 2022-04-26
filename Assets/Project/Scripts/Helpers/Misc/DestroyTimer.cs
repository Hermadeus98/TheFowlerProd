using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DestroyTimer : MonoBehaviour
    {
        [SerializeField] private float delay = 15;
        
        private void Start()
        {
            Destroy(gameObject, delay);
        }
    }
}
