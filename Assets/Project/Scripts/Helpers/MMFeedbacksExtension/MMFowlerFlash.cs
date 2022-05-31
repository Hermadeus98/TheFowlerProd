using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

namespace TheFowler
{
    [AddComponentMenu("")]
    [FeedbackPath("TheFowler/Flash")]
    public class MMFowlerFlash : MMFeedback
    {

        public float durationIn, waitDuration, durationOut;
        public Color flashColor = Color.white;
        public Sprite sprite;

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            FlashHandler.Instance.Flash(durationIn, waitDuration, durationOut, flashColor, sprite);
        }
    }
}

