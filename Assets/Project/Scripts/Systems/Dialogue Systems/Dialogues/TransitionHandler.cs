using System;
using QRCode;
using QRCode.Extensions;
using UnityEngine;
using MoreMountains.Feedbacks;


namespace TheFowler
{
    public class TransitionHandler : GameplayPhase
    {
        [SerializeField] private GameplayPhase nextPhase;
        [SerializeField] private TransitionType transitionType;
        [SerializeField] private MMFeedbacks feedbacks;


        public override void PlayPhase()
        {
            base.PlayPhase();
            feedbacks?.PlayFeedbacks();
        }

        public override void EndPhase()
        {
            base.EndPhase();
            if(nextPhase != null)
            {
                UI.GetView<TransitionView>(UI.Views.TransitionView).Show(transitionType, nextPhase.PlayPhase);
            }
            else
            {
                UI.GetView<TransitionView>(UI.Views.TransitionView).Show(transitionType, null);
            }

        }
    }

}
