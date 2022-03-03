using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class AnimTriggerAbi : SerializedMonoBehaviour
    {
        [TabGroup("References")] [SerializeField]
        public Sockets abi_Sockets;

        public ParticleSystem sfx_StandTom, sfx_Cymbal_L, sfx_Cymbal_R;

        public void SFX_StandTom()
        {
            if (sfx_StandTom == null) return;
            sfx_StandTom?.Play();
        }

        public void SFX_Cymbal_L()
        {
            if (sfx_Cymbal_L == null) return;
            sfx_Cymbal_L?.Play();
        }

        public void SFX_Cymbal_R()
        {
            if (sfx_Cymbal_R == null) return;
            sfx_Cymbal_R?.Play();
        }

        public void SFX_Cymbal_L_R()
        {
            SFX_Cymbal_L();
            SFX_Cymbal_R();
        }

        private void Update()
        {
            if (abi_Sockets != null)
            {
                sfx_StandTom.transform.position = abi_Sockets.drum_StandTom.position;
                sfx_Cymbal_L.transform.position = abi_Sockets.drum_Cymbal_L.position;
                sfx_Cymbal_R.transform.position = abi_Sockets.drum_Cymbal_R.position;
            }
        }
    }
}
