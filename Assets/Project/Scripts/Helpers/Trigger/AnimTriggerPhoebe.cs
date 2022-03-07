using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class AnimTriggerPhoebe : AnimTriggerBase
    {
        [SerializeField] private ParticleSystem vfx_attack;

        public void SFX_Slap()
        {
            vfx_attack?.Play();
        }

        private void Update()
        {
            if (Sockets.IsNotNull())
            {
                if(vfx_attack.IsNotNull()) vfx_attack.transform.position = Sockets.hand_Right.position;
            }
        }
    }
}
