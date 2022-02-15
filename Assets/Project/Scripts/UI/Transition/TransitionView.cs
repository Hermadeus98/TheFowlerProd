using System;
using System.Linq;
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
        [SerializeField] private MMFeedbacks transitionHarmonisation;
        [TabGroup("References")]
        [SerializeField] private MMFeedbacks transitionStatic;
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
        STATIC,
        MOVEMENT,
        HARMONISATION,
        BATTLE
    }
}

