using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class BattleState_SkillPicking : BattleState
    {
        private SkillPickingView skillPickingView;
        
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            
            if (BattleManager.IsAllyTurn)
            {
                skillPickingView = UI.OpenView<SkillPickingView>(UI.Views.SkillPicking);
            }
            
            CameraManager.Instance.SetCamera(BattleManager.CurrentBattleActor.cameraBatchBattle, CameraKeys.BattleKeys.SkillPicking);

        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();

            if (BattleManager.IsAllyTurn)
            {
                if (skillPickingView.skillSelector.WaitChoice(out var skillSelectorElement))
                {
                    var spell = skillSelectorElement.referedSpell;
                    
                    BattleManager.CurrentBattle.ChangeBattleState(BattleStateEnum.TARGET_PICKING);
                }
                if (Gamepad.current.bButton.wasPressedThisFrame)
                {
                    BattleManager.CurrentBattle.ChangeBattleState(BattleStateEnum.ACTION_PICKING);
                }
            }
            else if (BattleManager.IsEnemyTurn)
            {
                BattleManager.CurrentBattle.ChangeBattleState(BattleStateEnum.TARGET_PICKING);
            }
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            UI.CloseView(UI.Views.SkillPicking);
        }
    }
}
