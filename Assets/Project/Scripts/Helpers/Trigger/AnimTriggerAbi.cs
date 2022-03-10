using System;
using System.Collections;
using System.Collections.Generic;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class AnimTriggerAbi : AnimTriggerBase
    {
        [TabGroup("SkillExecution")]
        public ParticleSystem vfx_StandTom, vfx_Cymbal_L, vfx_Cymbal_R;

        public void SFX_StandTom()
        {
            if (vfx_StandTom == null) return;
            vfx_StandTom?.Play();
            
            GamepadVibration.Rumble(.6f, 0f, 0.1f);
        }

        public void SFX_Cymbal_L()
        {
            if (vfx_Cymbal_L == null) return;
            vfx_Cymbal_L?.Play();
            
            GamepadVibration.Rumble(.6f, 0f, 0.1f);
            GamepadVibration.Rumble(.0f, 1f, 0.2f);
        }

        public void SFX_Cymbal_R()
        {
            if (vfx_Cymbal_R == null) return;
            vfx_Cymbal_R?.Play();
            
            GamepadVibration.Rumble(.6f, 0f, 0.1f);
            GamepadVibration.Rumble(.0f, 1f, 0.2f);
        }

        public void SFX_Cymbal_L_R()
        {
            SFX_Cymbal_L();
            SFX_Cymbal_R();
        }

        private void Update()
        {
            if (Sockets != null)
            {
                if(vfx_StandTom.IsNotNull()) vfx_StandTom.transform.position = Sockets.drum_StandTom.position;
                if(vfx_Cymbal_L.IsNotNull()) vfx_Cymbal_L.transform.position = Sockets.drum_Cymbal_L.position;
                if(vfx_Cymbal_R.IsNotNull()) vfx_Cymbal_R.transform.position = Sockets.drum_Cymbal_R.position;
            }
        }
    }
}
