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

        private Coroutine openning;
        
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            
            if (BattleManager.IsAllyTurn)
            {
                skillPickingView = UI.GetView<SkillPickingView>(UI.Views.SkillPicking);
                skillPickingView.skillSelector.Refresh(BattleManager.CurrentBattleActor.BattleActorData);
                
                openning = StartCoroutine(OpenView());
                BattleManager.CurrentBattleActor.BattleActorAnimator.Idle();
            }

            SetCamera(CameraKeys.BattleKeys.SkillPicking);
        }

        private IEnumerator OpenView()
        {
            //yield return new WaitForSeconds(.5f);
            skillPickingView = UI.OpenView<SkillPickingView>(UI.Views.SkillPicking);
            yield break;
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();

            if (BattleManager.IsAllyTurn)
            {
                if (skillPickingView != null)
                {
                    if (skillPickingView.skillSelector.WaitChoice(out var skillSelectorElement))
                    {
                        SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_SkillSelection, gameObject);
                        Player.SelectedSpell = skillSelectorElement.referedSpell;
                        BattleManager.CurrentBattle.ChangeBattleState(BattleStateEnum.TARGET_PICKING);
                    }

                    if (inputs.actions["Return"].WasPressedThisFrame())
                    {
                        SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_Cancel, gameObject);
                        BattleManager.CurrentBattle.ChangeBattleState(BattleStateEnum.ACTION_PICKING);
                    }
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
            
            if(openning != null)
                StopCoroutine(openning);
        }
    }
}
