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

            if(transitionType != TransitionType.TURNTRANSITION)
            {
                feedbacks?.PlayFeedbacks();

            }
            else
            {
                UI.GetView<TurnTransitionView>(UI.Views.TurnTransition).CameraSwipTransition(EndPhase);
            }

        }


        public override void EndPhase()
        {
            base.EndPhase();
            if(nextPhase != null)
            {
                nextPhase.PlayPhase();
            }


        }

        public void Transition()
        {
            UI.GetView<TransitionView>(UI.Views.TransitionView).Show(transitionType, null);
        }

        public void ReplaceActor()
        {

            Player.Robyn?.gameObject.SetActive(true);
            Player.Abigael?.gameObject.SetActive(true);
            Player.Pheobe?.gameObject.SetActive(true);
        }

        public void ShowGainSkill(int value)
        {
            GainSkillView view = UI.GetView<GainSkillView>(UI.Views.GainSkill);
            view.Show(value);
        }


    }

}
