using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

namespace TheFowler
{
    [AddComponentMenu("")]
    [FeedbackPath("TheFowler/GameInstructions")]
    public class MMGameInstructions : MMFeedback
    {

        [SerializeField] private GameInstructions gameInstructions;
        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            gameInstructions.Call();
        }
    }
}

