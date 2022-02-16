using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;
using Event = AK.Wwise.Event;

namespace TheFowler
{
    public class EventBricks : SerializedMonoBehaviour
    {
        public List<EventBrick> Bricks;

        private bool next;
        private Coroutine currentBrickIE;
        
        [Button]
        public void Play()
        {
            StartCoroutine(PlayBricks());
        }

        IEnumerator PlayBricks()
        {
            for (int i = 0; i < Bricks.Count; i++)
            {
                currentBrickIE = StartCoroutine(Bricks[i].Execute());
                
                while (Bricks[i].isPlaying)
                {
                    if (next)
                    {
                        StopCoroutine(currentBrickIE);
                        Bricks[i].Stop();
                    }

                    yield return null;
                }

                next = false;
            }
            
            yield break;
        }

        [Button]
        public void Next()
        {
            next = true;
        }
    }

    [Serializable]
    public class EventBrick
    {
        [TitleGroup("Settings")] [SerializeField]
        private string sequenceName;
        [TitleGroup("Settings")] [SerializeField]
        private int loop = 1;
        
        [TitleGroup("Gameplay")]
        public bool waitAnInput = true;
        [TitleGroup("Gameplay"), ReadOnly]
        public bool hasAnInput = false; //Si il y un input l'action est fini.

        [TitleGroup("Sequence")]
        public MMFeedbacks sequence;
        [TitleGroup("Sequence"), ReadOnly]
        public bool isPlaying;

        
        public IEnumerator Execute()
        {
            isPlaying = true;

            for (int i = 0; i < loop; i++)
            {
                hasAnInput = false;
                
                if (waitAnInput)
                {
                    sequence.PlayFeedbacks();
                    while (!hasAnInput)
                    {
                        Debug.Log(sequenceName);
                        yield return null;
                    }
                    sequence.StopFeedbacks();
                }
                else
                {
                    sequence.PlayFeedbacks();
                    while (sequence.IsPlaying)
                    {
                        Debug.Log(sequenceName);
                        yield return null;
                    }
                }
            }

            isPlaying = false;
        }

        public void Stop()
        {
            isPlaying = false;
        }

        public void SetLoop(int loopCount)
        {
            loop = loopCount;
        }

        [Button]
        public void Input() => hasAnInput = true;
    }
}
