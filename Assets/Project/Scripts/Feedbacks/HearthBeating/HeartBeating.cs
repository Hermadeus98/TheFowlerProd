using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class HeartBeating : MonoBehaviourSingleton<HeartBeating>
    {
        public bool isBeating;
        
        public float bigDelay = .6f, littleDelay = .2f;

        private float t;
        
        private void Update()
        {
            if(!isBeating)
                return;

            if (t > 0)
            {
                t -= Time.deltaTime;
            }
            else
            {
                t = bigDelay;
                StartCoroutine(Pulse());
            }
        }

        private IEnumerator Pulse()
        {
            GamepadVibration.Rumble(1f, 0f, 0.05f);
            yield return new WaitForSeconds(littleDelay);
            GamepadVibration.Rumble(1f, 0f, 0.05f);
        }

        public void BeatingForTime(float t)
        {
            StartCoroutine(BeatingForTimeIE(t));
        }

        IEnumerator BeatingForTimeIE(float t)
        {
            isBeating = true;
            yield return new WaitForSeconds(t);
            isBeating = false;
        }
    }
}
