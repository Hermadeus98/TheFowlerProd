using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class BattleState_Fury : BattleState
    {
        public ITurnActor selectedActorForFury;

        public override void OnStateEnter(EventArgs arg)
        {
            Debug.Log("FURYYYYYYYYYYYYYYYY");
            BattleManager.CurrentRound.OverrideTurn(selectedActorForFury);
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            TargetSelector.ResetSelectedTargets();
        }
    }
}
