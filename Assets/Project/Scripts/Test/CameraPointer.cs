using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler.Test
{
    public class CameraPointer : MonoBehaviour
    {
        public Transform A, B;

        private Vector3 middlePos;
        
        private void Update()
        {
            var middle_X = GetMiddle(A.position.x, B.position.x);
            var middle_Y = GetMiddle(A.position.y, B.position.y);
            var middle_Z = GetMiddle(A.position.z, B.position.z);

            middlePos = new Vector3(middle_X, middle_Y, middle_Z);
            transform.position = middlePos;
        }

        float GetMiddle(float a, float b)
        {
            return (a + b) / 2;
        }

        private void OnDrawGizmos()
        {
            var middle_X = GetMiddle(A.position.x, B.position.x);
            var middle_Y = GetMiddle(A.position.y, B.position.y);
            var middle_Z = GetMiddle(A.position.z, B.position.z);

            middlePos = new Vector3(middle_X, middle_Y, middle_Z);

            transform.position = middlePos;
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(middlePos, .25f);
        }
    }
}
