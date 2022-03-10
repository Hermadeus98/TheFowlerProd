using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
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

        [FoldoutGroup("Anim")] public Image X, Y, A, B;
        [FoldoutGroup("Anim")] public Image X_box, Y_box, A_box, B_box;
        [FoldoutGroup("Anim")] public Image middle;
        [FoldoutGroup("Anim")] public TextMeshProUGUI X_text, Y_text, A_text, B_text;
        [FoldoutGroup("Anim"), SerializeField] private float fillDuration = .2f;

        private Coroutine opening;
        
        public override void Show()
        {
            base.Show();
            backAnim?.Kill();
            backAnim = backPart.DOFade(1f, .1f);
            ShowAnim();
        }

        [Button]
        public void ShowAnim()
        {
            X.fillAmount = 0;
            Y.fillAmount = 0;
            A.fillAmount = 0;
            B.fillAmount = 0;

            X_box.fillAmount = 0;
            Y_box.fillAmount = 0;
            A_box.fillAmount = 0;
            B_box.fillAmount = 0;

            X_text.gameObject.SetActive(false);
            Y_text.gameObject.SetActive(false);
            B_text.gameObject.SetActive(false);
            A_text.gameObject.SetActive(false);
            
            middle.fillAmount = 0;

            CanvasGroup.alpha = 1f;

            if(opening != null) StopCoroutine(opening);
            opening = StartCoroutine(ShowAnimIE());
        }

        IEnumerator ShowAnimIE()
        {
            middle.DOFillAmount(1f, fillDuration * 4f);
            
            B.DOFillAmount(1f, fillDuration);
            yield return new WaitForSeconds(fillDuration);
            
            Y.DOFillAmount(1f, fillDuration);
            A.DOFillAmount(1f, fillDuration);
            yield return new WaitForSeconds(fillDuration);
            
            X.DOFillAmount(1f, fillDuration);
            yield return new WaitForSeconds(fillDuration);

            Y_box.DOFillAmount(1f, fillDuration);
            A_box.DOFillAmount(1f, fillDuration);
            X_box.DOFillAmount(1f, fillDuration);
            B_box.DOFillAmount(1f, fillDuration);
            yield return new WaitForSeconds(fillDuration);

            X_text.gameObject.SetActive(true);
            Y_text.gameObject.SetActive(true);
            B_text.gameObject.SetActive(true);
            A_text.gameObject.SetActive(true);
        }

        [Button]
        public void HideAnim()
        {
            if(opening != null) StopCoroutine(opening);
            opening = StartCoroutine(HideAnimIE());
        }
        
        IEnumerator HideAnimIE()
        {
            middle.DOFillAmount(0f, fillDuration * 4f);
            
            X_text.gameObject.SetActive(false);
            Y_text.gameObject.SetActive(false);
            B_text.gameObject.SetActive(false);
            A_text.gameObject.SetActive(false);
            yield return new WaitForSeconds(fillDuration);

            Y_box.DOFillAmount(0f, fillDuration);
            A_box.DOFillAmount(0f, fillDuration);
            X_box.DOFillAmount(0f, fillDuration);
            B_box.DOFillAmount(0f, fillDuration);
            yield return new WaitForSeconds(fillDuration);
            
            X.DOFillAmount(0f, fillDuration);
            yield return new WaitForSeconds(fillDuration);
            
            Y.DOFillAmount(0f, fillDuration);
            A.DOFillAmount(0f, fillDuration);
            yield return new WaitForSeconds(fillDuration);
            
            B.DOFillAmount(0f, fillDuration);

            yield return new WaitForSeconds(fillDuration);
            CanvasGroup.alpha = 0;
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
            HideAnim();
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
