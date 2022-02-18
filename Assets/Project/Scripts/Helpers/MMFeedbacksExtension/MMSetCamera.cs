using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace TheFowler
{
    [FeedbackPath("TheFowler/Set Camera")]
    public class MMSetCamera : MMFeedback
    {
#if UNITY_EDITOR
        public override Color FeedbackColor { get { return MMFeedbacksInspectorColors.CameraColor; } }
#endif
        
        [SerializeField] private cameraPath path;
        
        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            CameraManager.Instance.SetCamera(path);
        }
    }
}
