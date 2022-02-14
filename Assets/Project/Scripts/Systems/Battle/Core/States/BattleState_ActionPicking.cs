using System;
using System.Collections;
using UnityEngine;

namespace TheFowler
{
    public class BattleState_ActionPicking : BattleState
    {
        private ActionPickingView ActionPickingView;
        
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);

            StartCoroutine(OnStateEnterIE());
        }

        IEnumerator OnStateEnterIE()
        {
            Debug.Log(BattleManager.CurrentBattleActor.CameraBatchBattle);
            CameraManager.Instance.SetCamera(BattleManager.CurrentBattleActor.CameraBatchBattle, CameraKeys.BattleKeys.ActionPicking);

            yield return new WaitForSeconds(UI.GetView<TurnTransitionView>(UI.Views.TurnTransition).WaitTime);
            
            if (BattleManager.IsAllyTurn)
            {
                ActionPickingView = UI.OpenView<ActionPickingView>(UI.Views.ActionPicking);
                ActionPickingView.Refresh(EventArgs.Empty);
            }

            isActive = true;
            yield break;
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();
            
            if(!isActive)
                return;

            if (BattleManager.IsAllyTurn)
            {
                if (ActionPickingView.CheckActions(out var actionType))
                {
                    switch (actionType)
                    {
                        case ActionPickerElement.PlayerActionType.NONE:
                            break;
                        case ActionPickerElement.PlayerActionType.SPELL:
                            BattleManager.CurrentBattle.ChangeBattleState<BattleState_SkillPicking>(BattleStateEnum.SKILL_PICKING);
                            break;
                        case ActionPickerElement.PlayerActionType.PARRY:
                            Player.SelectedSpell = BattleManager.CurrentBattleActor.BattleActorData.DefendSpell;
                        {
                            var skillPickingView =
                                BattleManager.CurrentBattle.ChangeBattleState<BattleState_TargetPicking>(BattleStateEnum
                                    .TARGET_PICKING);
                            skillPickingView.ReturnToActionMenu = true;
                        }
                            break;
                        case ActionPickerElement.PlayerActionType.ATTACK:
                            Player.SelectedSpell = BattleManager.CurrentBattleActor.BattleActorData.BasicAttackSpell;
                        {
                            var skillPickingView =
                                BattleManager.CurrentBattle.ChangeBattleState<BattleState_TargetPicking>(BattleStateEnum
                                    .TARGET_PICKING);
                            skillPickingView.ReturnToActionMenu = true;
                        }
                            break;
                        case ActionPickerElement.PlayerActionType.FURY:
                            //
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            else if (BattleManager.IsEnemyTurn)
            {
                BattleManager.CurrentBattle.ChangeBattleState<BattleState_SkillPicking>(BattleStateEnum.SKILL_PICKING);
            }
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            
            UI.CloseView(UI.Views.ActionPicking);

            isActive = false;
        }
    }
}
