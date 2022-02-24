using System;
using QRCode;
using QRCode.Extensions;
using UnityEngine;
using MoreMountains.Feedbacks;

namespace TheFowler
{
    public class BattleTransitionHandler : GameplayPhase
    {
        [SerializeField] private GameplayPhase nextPhase;
        public override void PlayPhase()
        {
            base.PlayPhase();

        }


        public override void EndPhase()
        {
            base.EndPhase();
            if (nextPhase != null)
            {
                nextPhase.PlayPhase();
            }


        }

    }

}
