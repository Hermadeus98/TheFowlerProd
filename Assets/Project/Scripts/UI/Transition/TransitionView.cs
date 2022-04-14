using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using QRCode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using MoreMountains.Feedbacks;

namespace TheFowler
{
    public class TransitionView : UIView
    {
        [TabGroup("References")]
        [SerializeField]
        private MMFeedbacks transitionHarmonisation, transitionStatic, transitionBattleIn,
            transitionChapter_One, transitionChapter_Two_PartOne, transitionChapter_Two_PartTwo, transitionChapter_Three, transitionCutscene;
        [ReadOnly]
        public ChapterEnum chapterType;

        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        public void Show(TransitionType transitionType, UnityEngine.Events.UnityAction action )
        {
            switch (transitionType)
            {
                case TransitionType.HARMONISATION:
                    Transition(transitionHarmonisation, action);
                    break;
                case TransitionType.STATIC:
                    Transition(transitionStatic, action);
                    break;
                case TransitionType.CHAPTER:
                    switch (chapterType)
                    {
                        case ChapterEnum.CHAPTER_TWO_PART1:
                            Transition(transitionChapter_Two_PartOne, action);
                            AkSoundEngine.SetState("Scene", "Scene2_TheAviary");
                            break;
                        case ChapterEnum.CHAPTER_TWO_PART2:
                            Transition(transitionChapter_Two_PartTwo, action);
                            AkSoundEngine.SetState("Scene", "Scene3_TheBallRoom");
                            break;
                        case ChapterEnum.CHAPTER_THREE:
                            Transition(transitionChapter_Three, action);
                            AkSoundEngine.SetState("Scene", "Scene4_TheTribunal");
                            break;
                    }
                    
                    
                    break;
                case TransitionType.BATTLE:
                    Transition(transitionBattleIn, action);
                    break;
                case TransitionType.CUTSCENE:
                    Transition(transitionCutscene, action);
                    return;
            }

            Show();
        }


        private void Transition(MMFeedbacks transition,UnityEngine.Events.UnityAction action)
        {
            for (int i = 0; i < transition.Feedbacks.Count; i++)
            {
                if (transition.Feedbacks[i].GetType() == typeof(MMUnityEvent))
                {

                    MMUnityEvent unityEvent = transition.Feedbacks[i] as MMUnityEvent;
                    unityEvent.InstantEvent.RemoveAllListeners();
                    if(action != null)
                    {
                        unityEvent.InstantEvent.AddListener(action);
                    }

                }
            }

            transition.PlayFeedbacks();

        }


    }

    public enum TransitionType
    {
        NULL,
        STATIC,
        MOVEMENT,
        HARMONISATION,
        BATTLE,
        CHAPTER,
        TURNTRANSITION,
        CUTSCENE
    }

}

