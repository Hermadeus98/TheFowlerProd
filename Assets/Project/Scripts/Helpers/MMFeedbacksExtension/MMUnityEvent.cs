using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

namespace TheFowler
{
    [AddComponentMenu("")]
    [FeedbackPath("TheFowler/UnityEvent")]
    public class MMUnityEvent : MMFeedback
    {
        public UnityEngine.Events.UnityEvent InstantEvent;

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            InstantEvent?.Invoke();
        }

    }
}

