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
        private float t;
        
        public bool fury;
        
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);

            StartCoroutine(Cast());
            
            CameraManager.Instance.SetCamera(BattleManager.CurrentBattleActor.CameraBatchBattle, CameraKeys.BattleKeys.SkillExecutionDefault);
        }

        IEnumerator Cast()
        {
            yield return new WaitForSeconds(.5f);

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
                        yield return Player.SelectedSpell.Cast();
                    }    
                }
                
                BattleManager.CurrentBattle.TurnSystem.NextTurn();
            }
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            fury = false;
        }
    }
}
