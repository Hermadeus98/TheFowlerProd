using System;
using System.Collections;
using UnityEngine;

namespace TheFowler
{
    public class BattleState_ActionPicking : BattleState
    {
        private ActionPickingView ActionPickingView;

        private Coroutine openning, closing;
        
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);

            if(closing != null) StopCoroutine(closing);
            if(openning != null) StopCoroutine(openning);
            openning = StartCoroutine(OnStateEnterIE());

            if (BattleManager.IsAllyTurn)
            {
                ActionPickingView = UI.GetView<ActionPickingView>(UI.Views.ActionPicking);
                BattleManager.CurrentBattleActor.BattleActorAnimator.Idle();
            }
        }

        IEnumerator OnStateEnterIE()
        {
            SetCamera(CameraKeys.BattleKeys.ActionPicking);

            //yield return new WaitForSeconds(UI.GetView<TurnTransitionView>(UI.Views.TurnTransition).WaitTime);
            yield return new WaitForSeconds(.2f);
            if (Tutoriel.isTutoriel)
            {
                if (BattleManager.IsAllyTurn)
                {
                    if (Tutoriel.BasicAttack)
                    {
                        ActionPickingView = UI.OpenView<ActionPickingView>(UI.Views.ActionPicking);
                        ActionPickingView.Refresh(EventArgs.Empty);
                        ActionPickingView.AllowBasicAttack(true);
                        ActionPickingView.AllowParry(false);
                        ActionPickingView.AllowSkill(false);
                    }
                }
            }
            else
            {
                if (BattleManager.IsAllyTurn)
                {
                    ActionPickingView = UI.OpenView<ActionPickingView>(UI.Views.ActionPicking);
                    ActionPickingView.Refresh(EventArgs.Empty);
                }
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
                            Player.SelectedSpell = BattleManager.CurrentBattleActor.BattleActorData.BatonPass;
                            var skillExecutionState = BattleManager.CurrentBattle.BattleState.GetState("SkillExecution") as BattleState_SkillExecution;
                            skillExecutionState.fury = true;
                        {
                            var skillPickingView =
                                BattleManager.CurrentBattle.ChangeBattleState<BattleState_TargetPicking>(BattleStateEnum
                                    .TARGET_PICKING);
                            skillPickingView.ReturnToActionMenu = true;
                        }
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

            if(openning != null) StopCoroutine(openning);
            if(closing != null) StopCoroutine(closing);

            closing = StartCoroutine(CloseView());

            isActive = false;
        }

        private IEnumerator CloseView()
        {
            //yield return new WaitForSeconds(.5f);
            UI.CloseView(UI.Views.ActionPicking);
            yield break;
        }
    }
}
