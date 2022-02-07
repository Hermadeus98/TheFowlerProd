using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class BattleState_ActionPicking : BattleState
    {

        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();

            if (BattleManager.CurrentTurnActor is AllyActor)
            {
                if (Gamepad.current.aButton.wasPressedThisFrame)
                {
                    BattleManager.CurrentBattle.ChangeBattleState<BattleState_SkillPicking>(BattleStateEnum.SKILL_PICKING);
                }
            }
            else if (BattleManager.CurrentTurnActor is EnemyActor)
            {
                BattleManager.CurrentBattle.ChangeBattleState<BattleState_SkillPicking>(BattleStateEnum.SKILL_PICKING);
            }
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
        }
    }
}
