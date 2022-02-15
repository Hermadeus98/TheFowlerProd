using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

[AddComponentMenu("")]
[FeedbackPath("TheFowler/UnityEvent")]
public class MMEventCustom : MMFeedback
{
    [SerializeField] private UnityEngine.Events.UnityEvent InstantEvent;

    protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
    {
        InstantEvent.Invoke();
    }
}
