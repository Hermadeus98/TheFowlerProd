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
                BattleManager.lastTurnWasEnemiesTurn = false;

                //CameraManager.Instance.SetCamera(BattleManager.CurrentBattleActor.CameraBatchBattle, CameraKeys.BattleKeys.ActionPicking);
                
                InfoBoxButtons[] infoButtons = new InfoBoxButtons[1];
                            infoButtons[0] = InfoBoxButtons.HIDE;
                
                            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);
            }

            if (BattleManager.IsEnemyTurn)
            {
                UI.CloseView("FuryView");
            }
            

            BattleManager.CurrentBattle.BattleGameLogComponent.ShowGameLogView();
        }

        IEnumerator OnStateEnterIE()
        {
            if (BattleManager.CurrentBattleActor.mustResurect)
            {
                if (BattleManager.CurrentBattleActor is AllyActor allyActor)
                {
                    if (allyActor.mustResurect)
                    {
                        yield return allyActor.ResurectionCoroutine();
                    }
                }
            }
            
            if (BattleManager.IsAllyTurn)
            {
                SetCamera(CameraKeys.BattleKeys.ActionPicking);
            }
            else if (BattleManager.IsEnemyTurn)
            {
                if (!BattleManager.lastTurnWasEnemiesTurn)
                {
                    yield return new WaitForSeconds(.2f);
                }
            }

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

                                if (!Tutoriel.hasSpell)
                                {
                                    Tutoriel.hasSpell = true;
                                    UI.GetView<TutorielView>(UI.Views.Tuto).Show(TutorielEnum.SPELL);
                                }
                                break;
                            case ActionPickerElement.PlayerActionType.PARRY:
                                Player.SelectedSpell = BattleManager.CurrentBattleActor.BattleActorData.DefendSpell;
                                {
                                    
                                    var skillPickingView =
                                        BattleManager.CurrentBattle.ChangeBattleState<BattleState_TargetPicking>(BattleStateEnum
                                            .TARGET_PICKING);
                                    skillPickingView.ReturnToActionMenu = true;

                                }

                                if (!Tutoriel.hasQuickAttack)
                                {
                                    Tutoriel.hasQuickAttack = true;
                                    UI.GetView<TutorielView>(UI.Views.Tuto).Show(TutorielEnum.QUICKATTACK);
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


                                if (!Tutoriel.hasBasicAttack)
                                {
                                    Tutoriel.hasBasicAttack = true;
                                    UI.GetView<TutorielView>(UI.Views.Tuto).Show(TutorielEnum.BASICATTACK);
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

            if (BattleManager.IsEnemyTurn)
            {
                BattleManager.lastTurnWasEnemiesTurn = true;
            }

            SoundManager.PlaySound( AudioGenericEnum.TF_SFX_Combat_UI_SwitchCamera_Light, gameObject);

            BattleManager.CurrentBattle.BattleGameLogComponent.HideGameLogView();
        }

        private IEnumerator CloseView()
        {
            //yield return new WaitForSeconds(.5f);
            UI.CloseView(UI.Views.ActionPicking);
            yield break;
        }
    }
}
