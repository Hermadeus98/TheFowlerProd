using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace TheFowler
{
    public class EventBricks : SerializedMonoBehaviour
    {
        public List<EventBrick> Bricks;

        private bool next;
        private Coroutine currentBrickIE;

        [ReadOnly] public bool isPlaying;
 
        [HideInInspector] public Action OnStart, OnEnd;
        
         public EventBrick CurrentBrick { get; private set; }
        
        [Button]
        public void Play()
        {
            StartCoroutine(PlayBricks());
        }

        IEnumerator PlayBricks()
        {
            isPlaying = true;
            OnStart?.Invoke();
            
            for (int i = 0; i < Bricks.Count; i++)
            {
                CurrentBrick = Bricks[i];
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

            isPlaying = false;
            OnEnd?.Invoke();
            CurrentBrick = null;
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
        private BrickType brickType;
        [TitleGroup("Settings")] [SerializeField]
        private string sequenceName;
        [TitleGroup("Settings")] [SerializeField]
        private int loop = 1;
        
        [TitleGroup("Gameplay"), ReadOnly, ShowIf("@this.brickType == BrickType.WAIT_INPUT")]
        public bool hasAnInput = false; //Si il y un input l'action est fini.

        [TitleGroup("Sequence")]
        public MMFeedbacks sequence;
        [TitleGroup("Sequence"), ReadOnly]
        public bool isPlaying;

        [TitleGroup("Sequence"), ShowIf("@this.brickType == BrickType.CINEMATIC")] 
        public PlayableDirector PlayableDirector;
        
        public enum BrickType
        {
            SEQUENCE,
            WAIT_INPUT,
            CINEMATIC,
        }
        
        public IEnumerator Execute()
        {
            isPlaying = true;

            for (int i = 0; i < loop; i++)
            {
                hasAnInput = false;

                switch (brickType)
                {
                    case BrickType.SEQUENCE:
                        sequence.PlayFeedbacks();
                        while (sequence.IsPlaying)
                        {
                            yield return null;
                        }
                        break;
                    case BrickType.WAIT_INPUT:
                        sequence.PlayFeedbacks();
                        while (!hasAnInput)
                        {
                            yield return null;
                        }
                        sequence.StopFeedbacks();
                        break;
                    case BrickType.CINEMATIC:
                        PlayableDirector.Play();
                        while (PlayableDirector.state == PlayState.Playing)
                        {
                            yield return null;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            isPlaying = false;
        }

        public void Stop()
        {
            isPlaying = false;
            PlayableDirector.Stop();
        }

        public void SetLoop(int loopCount)
        {
            loop = loopCount;
        }

        [Button]
        public void Input()
        {
            if(brickType == BrickType.WAIT_INPUT)
                hasAnInput = true;
        }
    }
}
