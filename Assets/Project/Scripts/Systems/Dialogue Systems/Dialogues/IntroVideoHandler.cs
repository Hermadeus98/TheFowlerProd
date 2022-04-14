using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class IntroVideoHandler : VideoHandler
    {

        [SerializeField] private GameplayPhase nextPhase;

        public override void PlayPhase()
        {
            base.PlayPhase();
        }

        public override void EndPhase()
        {
            base.EndPhase();
            LaunchContextualAction();


        }
        public void LaunchContextualAction()
        {
            UI.GetView<ContextualActionView>(UI.Views.ContextualAction).Show(ContextualActionLocation.INTRO, 8, End);
        }

        public void End()
        {
            UI.GetView<ContextualActionView>(UI.Views.ContextualAction).Hide();
            nextPhase.PlayPhase();
        }
    }
}

