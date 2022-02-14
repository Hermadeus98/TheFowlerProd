using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class BattleState_TargetPicking : BattleState
    {
        public bool ReturnToActionMenu { get; set; }

        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);

            if (BattleManager.IsAllyTurn)
            {
                UI.OpenView(UI.Views.TargetPicking);
                TargetSelector.Initialize(Player.SelectedSpell.TargetType);
                SetCamera();
            }
            else if(BattleManager.IsEnemyTurn)
            {
                SetCamera(CameraKeys.BattleKeys.TargetPickingDefault);
            }
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();

            if (BattleManager.IsAllyTurn)
            {
                TargetSelector.Navigate(inputs.actions["NavigateLeft"].WasPressedThisFrame(), inputs.actions["NavigateRight"].WasPressedThisFrame());
                
                if (TargetSelector.Select(inputs.actions["Select"].WasPressedThisFrame(), out var targets))
                {
                    for (int i = 0; i < targets.Count(); i++)
                    {
                        Debug.Log(targets.ElementAt(i).BattleActorData.actorName);
                    }
                    
                    BattleManager.CurrentBattle.ChangeBattleState<BattleState_SkillExecution>(BattleStateEnum.SKILL_EXECUTION);
                }
                if (inputs.actions["Return"].WasPressedThisFrame())
                {
                    if (ReturnToActionMenu)
                    {
                        BattleManager.CurrentBattle.ChangeBattleState(BattleStateEnum.ACTION_PICKING);
                    }
                    else
                    {
                        BattleManager.CurrentBattle.ChangeBattleState(BattleStateEnum.SKILL_PICKING);
                    }
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
            TargetSelector.Quit();
            ReturnToActionMenu = false;
        }

        private void SetCamera()
        {
            switch (Player.SelectedSpell.TargetType)
            {
                case TargetTypeEnum.SELF:
                    SetCamera(CameraKeys.BattleKeys.TargetPickingDefault);
                    break;
                case TargetTypeEnum.SOLO_ENEMY:
                    SetCamera(CameraKeys.BattleKeys.TargetPickingEnemies);
                    break;
                case TargetTypeEnum.ALL_ENEMIES:
                    SetCamera(CameraKeys.BattleKeys.TargetPickingEnemies);
                    break;
                case TargetTypeEnum.SOLO_ALLY:
                    CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "Allies");
                    //SetCamera(CameraKeys.BattleKeys.TargetPickingAllies);
                    break;
                case TargetTypeEnum.ALL_ALLIES:
                    CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "Allies");
                    //SetCamera(CameraKeys.BattleKeys.TargetPickingAllies);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
