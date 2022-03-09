using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QRCode;
using QRCode.Extensions;
using UnityEngine;

namespace TheFowler
{
    public class BattleState_Fury : BattleState
    {
        public ITurnActor selectedActorForFury;

        public override void OnStateEnter(EventArgs arg)
        {
            BattleManager.CurrentBattleActor.AllyData?.Fury(false);
            if (selectedActorForFury is AllyActor actor)
            {
                actor.AllyData?.Fury(true);   
            }
            
            BattleManager.CurrentRound.OverrideTurn(selectedActorForFury);
        }
    }
}
