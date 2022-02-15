using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace TheFowler
{
    [AddComponentMenu("")]
    [FeedbackPath("Audio/WwisePlayGenericSound")]
    public class MMPlayWwiseGenericAudio : MMFeedback
    {
        [SerializeField] private AudioGenericEnum key;
        [SerializeField] private GameObject handler;
        
        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            SoundManager.PlaySound(key, handler);
        }
    }
}
