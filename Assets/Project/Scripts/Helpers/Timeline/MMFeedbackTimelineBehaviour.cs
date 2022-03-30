using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Playables;

namespace TheFowler
{
    [Serializable]
    public class MMFeedbackTimelineBehaviour : PlayableBehaviour
    {
        private bool firstFrameHappened = false;
        
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (Application.isPlaying)
            {
                var feedback = playerData as MMFeedbacks;

                if (feedback != null)
                {
                    if (!firstFrameHappened)
                    {
                        feedback.PlayFeedbacks();
                        firstFrameHappened = true;
                    }
                }
            }
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            firstFrameHappened = false;
        }
    }
}