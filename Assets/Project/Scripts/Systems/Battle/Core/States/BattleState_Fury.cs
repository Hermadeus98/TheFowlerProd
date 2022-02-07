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
            BattleManager.CurrentRound.OverrideTurn(selectedActorForFury);
        }
    }
}
