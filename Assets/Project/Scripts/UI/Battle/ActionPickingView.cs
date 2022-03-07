using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class ActionPickingView : UIView
    {
        [SerializeField] private CanvasGroup backPart;
        public ActionPicker ActionPicker;

        [SerializeField] private PlayerInput Inputs;
        [SerializeField] private ActionPickerElement[] actions;
        
        private Tween backAnim;
        
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
    }
}
