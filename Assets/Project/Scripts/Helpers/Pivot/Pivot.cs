using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class Pivot : SerializedMonoBehaviour
    {
        [SerializeField] private Transform pivot;

        [Button]
        private void GoTo() => Update();
        
        private void Update()
        {
            transform.position = pivot.position;
            transform.rotation = pivot.rotation;
            transform.localScale = pivot.localScale;
        }
    }
}
