using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class BattleState_SkillExecution : BattleState
    {
        public bool fury;
        
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            
            SetCamera(CameraKeys.BattleKeys.SkillExecutionDefault);

            StartCoroutine(Cast());
        }

        IEnumerator Cast()
        {
            if (fury)
            {
                BattleManager.CurrentBattle.ChangeBattleState<BattleState_Fury>(BattleStateEnum.FURY);
            }
            else
            {
                if (BattleManager.IsAllyTurn)
                {
                    if (Player.SelectedSpell.IsNotNull())
                    {
                        BattleManager.CurrentBattleActor.Mana.RemoveMana(Player.SelectedSpell.ManaCost);
                        yield return Player.SelectedSpell.Cast(BattleManager.CurrentBattleActor, TargetSelector.SelectedTargets.ToArray());
                    }    
                }

                yield return new WaitForSeconds(2f);
                BattleManager.CurrentBattle.TurnSystem.NextTurn();
            }
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            fury = false;
            TargetSelector.ResetSelectedTargets();
        }
    }
}
