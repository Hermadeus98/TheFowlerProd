using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class BattleState_TargetPicking : BattleState
    {
        public bool ReturnToActionMenu { get; set; }

        private TargetPickingView targetPickingView;

        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);

            if (BattleManager.IsAllyTurn)
            {
                targetPickingView =  UI.OpenView<TargetPickingView>(UI.Views.TargetPicking);
                targetPickingView.Refresh(Player.SelectedSpell);
                
                TargetSelector.OnTargetChanged += PreviewManager.SetPreviews;
                TargetSelector.Initialize(Player.SelectedSpell.TargetType);
                
                SetCamera();
                
                if(Player.SelectedSpell.TargetType == TargetTypeEnum.SELF)
                {
                    InfoBoxButtons[] infoButtons = new InfoBoxButtons[2];
                    infoButtons[0] = InfoBoxButtons.CONFIRM;
                    infoButtons[1] = InfoBoxButtons.BACK;
                    UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);

                }
                else
                {
                    InfoBoxButtons[] infoButtons = new InfoBoxButtons[3];
                    infoButtons[0] = InfoBoxButtons.CONFIRM;
                    infoButtons[1] = InfoBoxButtons.BACK;
                    infoButtons[2] = InfoBoxButtons.SELECTTARGET;
                    UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);
                }
            }
            
            PlaySpellPreview();

            BattleManager.CurrentBattleActor.punchline.PlayPunchline(PunchlineEnum.TARGETPICKING);
        }


        private void PlaySpellPreview()
        {
            if (BattleManager.IsAllyTurn)
            {
                var spell = Player.SelectedSpell;
                if (spell.IsNotNull())
                {
                    spell.Effects.ForEach(w => w.PreviewEffect(BattleManager.CurrentBattleActor));
                }
            }
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();
            if (!Tutoriel.isTutoriel)
            {
                if (BattleManager.IsAllyTurn)
                {
                    if (!Tutoriel.LockTarget)
                    {
                        TargetSelector.Navigate(inputs.actions["NavigateLeft"].WasPressedThisFrame(), inputs.actions["NavigateRight"].WasPressedThisFrame());
                    }

                    if (TargetSelector.Select(inputs.actions["Select"].WasPressedThisFrame(), out var targets))
                    {
                        BattleManager.CurrentBattle.ChangeBattleState<BattleState_SkillExecution>(BattleStateEnum.SKILL_EXECUTION);
                    }
                    if (!Tutoriel.LockTarget)
                    {
                        if (inputs.actions["Return"].WasPressedThisFrame())
                        {
                            SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_Cancel, gameObject);

                            if (Fury.IsInFury)
                            {
                                var skillEx = BattleManager.CurrentBattle.BattleState.GetState("SkillExecution") as BattleState_SkillExecution;
                                skillEx.fury = false;
                            }

                            if (ReturnToActionMenu)
                            {
                                BattleManager.CurrentBattle.ChangeBattleState(BattleStateEnum.ACTION_PICKING);
                                SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_SwitchCamera_Light, gameObject);
                            }
                            else
                            {
                                BattleManager.CurrentBattle.ChangeBattleState(BattleStateEnum.SKILL_PICKING);
                                SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_SwitchCamera_Light, gameObject);
                            }
                        }
                    }
                      
                }
                else if (BattleManager.IsEnemyTurn)
                {
                    BattleManager.CurrentBattle.ChangeBattleState<BattleState_SkillExecution>(BattleStateEnum.SKILL_EXECUTION);
                }
            }
            
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);

            if (BattleManager.IsAllyTurn)
            {
                TargetSelector.OnTargetChanged -= PreviewManager.SetPreviews;
                PreviewManager.EndPreviews();
            }
            
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
                    SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_SwitchCamera_Heavy, gameObject);
                    //SetCamera(CameraKeys.BattleKeys.TargetPickingAllies);
                    break;
                case TargetTypeEnum.ALL_ALLIES:
                    CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "Allies");
                    SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_SwitchCamera_Heavy, gameObject);
                    //SetCamera(CameraKeys.BattleKeys.TargetPickingAllies);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
