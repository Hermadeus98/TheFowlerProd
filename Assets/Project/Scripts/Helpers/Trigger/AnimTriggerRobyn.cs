using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class AnimTriggerRobyn : SerializedMonoBehaviour
    {
        [TabGroup("References")] [SerializeField]
        private Sockets robyn_sockets;
        
        [TabGroup("VFX_Slap")] [SerializeField]
        private ParticleSystem vfx_slap_particles;

        [TabGroup("VFX_Slap")] [SerializeField]
        private MMFeedbacks feedbacks_attack;
        
        public void VFX_Slap()
        {
            if (vfx_slap_particles == null) return;
            vfx_slap_particles.Play();
            feedbacks_attack.PlayFeedbacks();
        }

        private void Update()
        {
            if (vfx_slap_particles == null) return;
            vfx_slap_particles.transform.position = robyn_sockets.hand_Left.position;
        }
    }
}