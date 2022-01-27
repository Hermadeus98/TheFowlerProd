using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class Pivot : MonoBehaviour
    {
        [SerializeField] private Transform pivot;

        private void Update()
        {
            transform.position = pivot.position;
            transform.rotation = pivot.rotation;
            transform.localScale = pivot.localScale;
        }
    }
}
