using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class FeedbackSequencer : SerializedMonoBehaviour
    {
        [SerializeField] private FeedbackSequence[] Sequences;

        private Coroutine c;
        
        [Button]
        private void Play()
        {
            if(c != null) StopCoroutine(c);
            c = StartCoroutine(PlaySequencer());
        }

        public IEnumerator PlaySequencer()
        {
            for (int i = 0; i < Sequences.Length; i++)
            {
                Sequences[i].sequence.PlayFeedbacks();
                
                while (Sequences[i].sequence.IsPlaying)
                {
                    yield return null;
                }
            }
        }
    }

    [Serializable]
    public class FeedbackSequence
    {
        public MMFeedbacks sequence;
    }
}
