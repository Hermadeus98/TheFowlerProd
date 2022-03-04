using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class AnimTriggerBase : SerializedMonoBehaviour
    {
        [SerializeField] protected MMFeedbacks feedback_attack;

        public void VFX_Attack()
        {
            feedback_attack?.PlayFeedbacks();
        }
    }
}
