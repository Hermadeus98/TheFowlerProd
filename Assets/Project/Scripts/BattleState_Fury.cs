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

        public static float punchlineDuration;
        
        public override void OnStateEnter(EventArgs arg)
        {
            BattleManager.CurrentBattleActor.AllyData?.Fury(false);
            
            BattleManager.CurrentBattleActor.punchline.PlayPunchline(PunchlineCallback.GIVING_BREAKDOWN, out var data);
            if (data != null)
            {
                punchlineDuration = data.soundDuration;
            }
            else
            {
                punchlineDuration = 2f;
            }

            if (selectedActorForFury is AllyActor actor)
            {
                actor.AllyData?.Fury(true);

                if (actor.BattleActorInfo.isDeath)
                {
                    actor.resurector = BattleManager.CurrentBattleActor;
                    actor.mustResurect = true;
                }
            }

            BattleManager.CurrentRound.OverrideTurn(selectedActorForFury);

             InfoBoxButtons[] infoButtons = new InfoBoxButtons[2];
            infoButtons[0] = InfoBoxButtons.CONFIRM;
            infoButtons[1] = InfoBoxButtons.BACK;

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);
            
            var skillExecutionState = BattleManager.CurrentBattle.BattleState.GetState("SkillExecution") as BattleState_SkillExecution;
            skillExecutionState.fury = false;
        }
    }
}
