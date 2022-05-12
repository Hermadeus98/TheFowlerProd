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
            
            if(selectedActorForFury is AllyActor ally)
            {
                ally.Health.Resurect(25f);
            }

            BattleManager.CurrentRound.OverrideTurn(selectedActorForFury);

            //BattleManager.CurrentBattleActor.punchline.PlayPunchline(PunchlineEnum.FURY);

             InfoBoxButtons[] infoButtons = new InfoBoxButtons[2];
            infoButtons[0] = InfoBoxButtons.CONFIRM;
            infoButtons[1] = InfoBoxButtons.BACK;

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);
            
            var skillExecutionState = BattleManager.CurrentBattle.BattleState.GetState("SkillExecution") as BattleState_SkillExecution;
            skillExecutionState.fury = false;

            
        }
    }
}
