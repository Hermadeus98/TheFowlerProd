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
                UI.OpenView("FuryView");
                
                //CameraManager.Instance.SetCamera(BattleManager.CurrentBattleActor.CameraBatchBattle, CameraKeys.BattleKeys.ActionPicking);
            }

            if (BattleManager.IsEnemyTurn)
            {
                UI.CloseView("FuryView");
            }

            BattleManager.CurrentBattleActor.punchline.PlayPunchline(PunchlineEnum.ACTIONPICKING);

            
            InfoBoxButtons[] infoButtons = new InfoBoxButtons[1];
            infoButtons[0] = InfoBoxButtons.HIDE;

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);
        }

        IEnumerator OnStateEnterIE()
        {
            if (BattleManager.IsAllyTurn)
            {
                SetCamera(CameraKeys.BattleKeys.ActionPicking);
            }
            else if (BattleManager.IsEnemyTurn)
            {
                SetCamera(CameraKeys.BattleKeys.TargetPickingGuard);
            }

            //yield return new WaitForSeconds(UI.GetView<TurnTransitionView>(UI.Views.TurnTransition).WaitTime);
            yield return new WaitForSeconds(.2f);


            if (BattleManager.IsAllyTurn)
            {
                if(UI.GetView<ActionPickingView>(UI.Views.ActionPicking) != null)
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

            if (!Tutoriel.isTutoriel)
            {
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
                                //Fury.BatonPass();

                                if (BattleManager.CurrentBattleActor.BattleActorData.FurySpell == null)
                                {
                                    Debug.LogError($"Il manque le spell de fury dans : {BattleManager.CurrentBattleActor.BattleActorData}", BattleManager.CurrentBattleActor.BattleActorData);
                                    break;
                                }
                                
                                Fury.PlayFury(BattleManager.CurrentBattleActor.BattleActorData.FurySpell);
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
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);

            if(openning != null) StopCoroutine(openning);
            if(closing != null) StopCoroutine(closing);

            closing = StartCoroutine(CloseView());

            isActive = false;

            SoundManager.PlaySound( AudioGenericEnum.TF_SFX_Combat_UI_SwitchCamera_Light, gameObject);
        }

        private IEnumerator CloseView()
        {
            //yield return new WaitForSeconds(.5f);
            UI.CloseView(UI.Views.ActionPicking);
            yield break;
        }
    }
}
