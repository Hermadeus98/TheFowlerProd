using System;
using QRCode;
using QRCode.Extensions;
using UnityEngine;
using MoreMountains.Feedbacks;
using System.Collections;

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
            if(!IsActive) return;
            base.EndPhase();
            if(nextPhase != null)
            {
                nextPhase.PlayPhase();
            }


        }

        public void Transition()
        {
            if (transitionType == TransitionType.CUTSCENE)
            {
                UI.GetView<TransitionView>(UI.Views.TransitionView).Show(transitionType, EndPhase);
                return;
            }
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
