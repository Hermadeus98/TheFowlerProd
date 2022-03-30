using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class CameraVelocity : MonoBehaviour
    {
        [ReadOnly] public float velocity;

        private Vector3 previous;

        private void Start()
        {
            previous = transform.position;
        }

        private void Update()
        {
            velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
            previous = transform.position;

            AkSoundEngine.SetRTPCValue("Game_MainCam_Velocity", velocity, gameObject);
        }
    }
}
