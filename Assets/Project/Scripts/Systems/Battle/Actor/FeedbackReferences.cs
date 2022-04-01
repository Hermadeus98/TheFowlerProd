using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
namespace TheFowler
{
    public class FeedbackReferences : MonoBehaviour
    {
        [TabGroup("Action Preview")]
        public MMFeedbacks actionPreviewIn, actionPreviewOut;

        [TabGroup("Battle Components")]
        [SerializeField] private List<MMFeedbacks> feedbackFromComponents;

        public List<MMFeedbacks> FeedbackFromComponents => feedbackFromComponents;

        public void StopBattleComponentsFB()
        {
            //if (FeedbackFromComponents == null) return;
            //for (int i = 0; i < FeedbackFromComponents.Count; i++)
            //{
            //    FeedbackFromComponents[i].StopFeedbacks();
            //}
        }

    }
}

