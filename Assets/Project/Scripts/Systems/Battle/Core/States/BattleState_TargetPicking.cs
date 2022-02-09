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
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);

            if (BattleManager.IsAllyTurn)
            {
                UI.OpenView(UI.Views.TargetPicking);
            }
            
            CameraManager.Instance.SetCamera(BattleManager.CurrentBattleActor.cameraBatchBattle, CameraKeys.BattleKeys.TargetPicking);
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();

            if (BattleManager.IsAllyTurn)
            {
                if (inputs.actions["Select"].WasPressedThisFrame())
                {
                    BattleManager.CurrentBattle.ChangeBattleState<BattleState_SkillExecution>(BattleStateEnum.SKILL_EXECUTION);
                }
                if (Gamepad.current.bButton.wasPressedThisFrame)
                {
                    BattleManager.CurrentBattle.ChangeBattleState(BattleStateEnum.SKILL_PICKING);
                }
            }
            else if (BattleManager.IsEnemyTurn)
            {
                BattleManager.CurrentBattle.ChangeBattleState<BattleState_SkillExecution>(BattleStateEnum.SKILL_EXECUTION);
            }
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            UI.CloseView(UI.Views.TargetPicking);
        }
    }
}
