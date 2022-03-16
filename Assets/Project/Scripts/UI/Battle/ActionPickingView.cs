using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
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
        [SerializeField] private GameObject fury_desc;

        [FoldoutGroup("Anim")] public Image X, Y, A, B;
        [FoldoutGroup("Anim")] public Image X_box, Y_box, A_box, B_box;
        [FoldoutGroup("Anim")] public Image middle;
        [FoldoutGroup("Anim")] public TextMeshProUGUI X_text, Y_text, A_text, B_text;
        [FoldoutGroup("Anim"), SerializeField] private float fillDurationHide = .2f, fillDurationShow = .2f;

        private Coroutine opening;

        public GameObject[] decriptions;
        
        public override void Show()
        {
            base.Show();
            backAnim?.Kill();
            backAnim = backPart.DOFade(1f, .1f);
            ShowAnim();
        }

        public void HideTuto()
        {
            CanvasGroup.alpha = 0;

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
            middle.DOFillAmount(1f, fillDurationShow * 4f);
            
            B.DOFillAmount(1f, fillDurationShow);
            yield return new WaitForSeconds(fillDurationShow);
            
            Y.DOFillAmount(1f, fillDurationShow);
            A.DOFillAmount(1f, fillDurationShow);
            yield return new WaitForSeconds(fillDurationShow);
            
            X.DOFillAmount(1f, fillDurationShow);
            yield return new WaitForSeconds(fillDurationShow);

            Y_box.DOFillAmount(1f, fillDurationShow);
            A_box.DOFillAmount(1f, fillDurationShow);
            X_box.DOFillAmount(1f, fillDurationShow);
            B_box.DOFillAmount(1f, fillDurationShow);
            yield return new WaitForSeconds(fillDurationShow);

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
            middle.DOFillAmount(0f, fillDurationHide * 4f);
            
            X_text.gameObject.SetActive(false);
            Y_text.gameObject.SetActive(false);
            B_text.gameObject.SetActive(false);
            A_text.gameObject.SetActive(false);
            yield return new WaitForSeconds(fillDurationHide);

            Y_box.DOFillAmount(0f, fillDurationHide);
            A_box.DOFillAmount(0f, fillDurationHide);
            X_box.DOFillAmount(0f, fillDurationHide);
            B_box.DOFillAmount(0f, fillDurationHide);
            yield return new WaitForSeconds(fillDurationHide);
            
            X.DOFillAmount(0f, fillDurationHide);
            yield return new WaitForSeconds(fillDurationHide);
            
            Y.DOFillAmount(0f, fillDurationHide);
            A.DOFillAmount(0f, fillDurationHide);
            yield return new WaitForSeconds(fillDurationHide);
            
            B.DOFillAmount(0f, fillDurationHide);

            yield return new WaitForSeconds(fillDurationHide);
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
                //fury_desc.SetActive(true);
            }
            else
            {
                flamme.DOFade(0f, 0.01f);
                //fury_desc.SetActive(false);
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

        public void ShowDescription(bool state) => decriptions.ForEach(w => w.SetActive(state));
    }
}
