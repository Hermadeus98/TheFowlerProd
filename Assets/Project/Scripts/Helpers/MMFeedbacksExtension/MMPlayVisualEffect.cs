using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.VFX;

namespace TheFowler
{
    [AddComponentMenu("")]
    [FeedbackPath("TheFowler/Play Visual Effect")]
    public class MMPlayVisualEffect : MMFeedback
    {
        [SerializeField] private VisualEffect VisualEffect;
        
        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            if (VisualEffect != null)
            {
                VisualEffect.Play();
            }
        }
    }
}
