using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

namespace TheFowler
{
    public class AnimTriggerRobyn : AnimTriggerBase
    {
        [TabGroup("VFX_Slap")] [SerializeField]
        private ParticleSystem vfx_slap_particles;

        [TabGroup("VFX_Attack")]
        [SerializeField]
        private VisualEffect vfx_attack_anticipation, vfx_attack_explosion, vfx_attack_landing;


        public void VFX_Slap()
        {
            PlayVFX(vfx_slap_particles);
            GamepadVibration.Rumble(.0f, 1f, 0.2f);
        }

        public void AttackAnticipation()
        {
            PlayVFX(vfx_attack_anticipation);
            GamepadVibration.Rumble(.0f, 1f, 0.1f);
        }

        public void AttackExplosion()
        {
            PlayVFX(vfx_attack_explosion);
            GamepadVibration.Rumble(.0f, 1f, 0.1f);
        }

        public void AttackLanding()
        {
            PlayVFX(vfx_attack_landing);
            GamepadVibration.Rumble(.0f, 2f, 0.2f);
        }

        private void PlayVFX(ParticleSystem vfx)
        {
            if (vfx == null) return;

            vfx?.Play();
        }

        private void PlayVFX(VisualEffect ve)
        {
            if (ve == null) return;

            ve?.Play();
        }

        private void Update()
        {
            if (Sockets.IsNotNull())
            {
                if(vfx_slap_particles.IsNotNull()) vfx_slap_particles.transform.position = Sockets.hand_Left.position;
            }
        }
    }
}