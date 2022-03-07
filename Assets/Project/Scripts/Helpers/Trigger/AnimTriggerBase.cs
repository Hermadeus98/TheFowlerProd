using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class AnimTriggerBase : SerializedMonoBehaviour
    {
        [SerializeField] protected MMFeedbacks feedback_attack;

        [TabGroup("References")]
        [SerializeField] protected Sockets Sockets;
        
        [TabGroup("Exploration")] [SerializeField]
        protected AudioGenericEnum audio_FootStep;
        
        //---<VFX>-----------------------------------------------------------------------------------------------------<
        public void VFX_Attack()
        {
            feedback_attack?.PlayFeedbacks();
        }

        //---<AUDIO>---------------------------------------------------------------------------------------------------<
        public void Audio_Walk_Foot_L()
        {
            SoundManager.PlaySound(audio_FootStep, Sockets.foot_Left.gameObject);
        }
        
        public void Audio_Walk_Foot_R()
        {
            SoundManager.PlaySound(audio_FootStep, Sockets.foot_Right.gameObject);
        }

        public void Audio_Run_Foot_L()
        {
            SoundManager.PlaySound(audio_FootStep, Sockets.foot_Left.gameObject);
        }
        
        public void Audio_Run_Foot_R()
        {
            SoundManager.PlaySound(audio_FootStep, Sockets.foot_Right.gameObject);
        }
    }
}
