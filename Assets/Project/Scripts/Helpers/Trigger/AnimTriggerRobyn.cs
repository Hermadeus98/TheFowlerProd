using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class AnimTriggerRobyn : AnimTriggerBase
    {
        [TabGroup("VFX_Slap")] [SerializeField]
        private ParticleSystem vfx_slap_particles;

        public void VFX_Slap()
        {
            if (vfx_slap_particles == null) return;

            vfx_slap_particles?.Play();
        }

        private void Update()
        {
            if (Sockets.IsNotNull())
            {
                vfx_slap_particles.transform.position = Sockets.hand_Left.position;
            }
        }
    }
}