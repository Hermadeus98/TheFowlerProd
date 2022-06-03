using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TheFowler;
using UnityEngine;

[AddComponentMenu("")]
[FeedbackPath("TheFowler/Destruction")]
public class MMDestruction : MMFeedback
{
    
    protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
    {
        DestructionSystem.Instance.useAnimatic = false;
        DestructionSystem.Instance.Destruct();
    }
}
