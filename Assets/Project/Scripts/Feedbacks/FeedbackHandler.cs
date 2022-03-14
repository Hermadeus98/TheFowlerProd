using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class FeedbackHander : SerializedMonoBehaviour
    {
        public Dictionary<string, MMFeedbacks> FeedbacksMap = new Dictionary<string, MMFeedbacks>();

        public void Generate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                FeedbacksMap.Add(transform.GetChild(i).gameObject.name, transform.GetChild(i).GetComponent<MMFeedbacks>());
            }
        }
    }
}
