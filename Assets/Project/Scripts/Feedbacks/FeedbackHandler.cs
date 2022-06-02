using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class FeedbackHandler : SerializedMonoBehaviour
    {
        /*public Dictionary<string, MMFeedbacks> FeedbacksMap = new Dictionary<string, MMFeedbacks>();

        public void PlayFeedback(string key)
        {
            if(string.IsNullOrEmpty(key))
                return;
            
            if (!FeedbacksMap.ContainsKey(key))
            {
                Debug.LogError("Feedback map don't contains " + key);
                return;
            }
            
            FeedbacksMap[key].PlayFeedbacks();
        }
        
        [Button]
        public void Generate()
        {
            FeedbacksMap = new Dictionary<string, MMFeedbacks>();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                FeedbacksMap.Add(transform.GetChild(i).gameObject.name, transform.GetChild(i).GetComponent<MMFeedbacks>());
            }
        }*/
    }
}
