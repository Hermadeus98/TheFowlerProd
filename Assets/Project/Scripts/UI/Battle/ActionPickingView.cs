using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TheFowler
{
    public class ActionPickingView : UIView
    {
        [SerializeField] private CanvasGroup backPart;
        public ActionPicker ActionPicker;

        [SerializeField] private PlayerInput Inputs;
        [SerializeField] private ActionPickerElement[] actions;

        public ActionPickerElement furyButton, basicAttack, parry, skill;
        
        private Tween backAnim;

        [SerializeField] private Image flamme;
        
        public override void Show()
        {
            base.Show();
            backAnim?.Kill();
            backAnim = backPart.DOFade(1f, .1f);
        }

        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (BattleManager.CurrentTurnActor != null)
            {
                var currentBattleActor = BattleManager.CurrentTurnActor as BattleActor;
                //ActionPicker.PlugToActor(currentBattleActor);
            }
        }

        public override void Hide()
        {
            base.Hide();
            backAnim?.Kill();
            backAnim = backPart.DOFade(0f, .1f);
        }

        public bool CheckActions(out ActionPickerElement.PlayerActionType playerActionType)
        {
            var check = false;
            
            for (int i = 0; i < actions.Length; i++)
            {
                check = actions[i].CheckInput(Inputs, out var action);
                playerActionType = action;

                if (check)
                    return true;
            }

            playerActionType = ActionPickerElement.PlayerActionType.NONE;
            return false;
        }

        public void AllowFury(bool state)
        {
            furyButton.canInput = state;

            if (state)
            {
                flamme.DOFade(1f, 0.01f);
            }
            else
            {
                flamme.DOFade(0f, 0.01f);
            }
        }

        public void AllowSkill(bool state)
        {
            skill.canInput = state;
        }

        public void AllowBasicAttack(bool state)
        {
            basicAttack.canInput = state;
        }

        public void AllowParry(bool state)
        {
            parry.canInput = state;
        }
    }
}
