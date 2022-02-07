using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class BattleState_TargetPicking : BattleState
    {
        public override void OnStateExecute()
        {
            base.OnStateExecute();

            if (BattleManager.CurrentTurnActor is AllyActor)
            {
                if (Gamepad.current.aButton.wasPressedThisFrame)
                {
                    BattleManager.CurrentBattle.ChangeBattleState<BattleState_SkillExecution>(BattleStateEnum.SKILL_EXECUTION);
                }
                if (Gamepad.current.bButton.wasPressedThisFrame)
                {
                    BattleManager.CurrentBattle.ChangeBattleState(BattleStateEnum.SKILL_PICKING);
                }
            }
            else if (BattleManager.CurrentTurnActor is EnemyActor)
            {
                BattleManager.CurrentBattle.ChangeBattleState<BattleState_SkillExecution>(BattleStateEnum.SKILL_EXECUTION);
            }
        }
    }
}
