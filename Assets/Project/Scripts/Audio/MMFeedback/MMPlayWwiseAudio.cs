using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace TheFowler
{
    [AddComponentMenu("")]
    [FeedbackPath("Audio/WwisePlaySound")]
    public class MMPlayWwiseAudio : MMFeedback
    {
        [SerializeField] private AK.Wwise.Event soundPath;
        [SerializeField] private GameObject gameObject;
        
        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            if (Active)
            {
                soundPath.Post(gameObject);
            }
        }
    }
}
