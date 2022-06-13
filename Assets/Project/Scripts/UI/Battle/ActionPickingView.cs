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

        public PlayerInput Inputs;
        [SerializeField] public ActionPickerElement[] actions;

        public ActionPickerElement furyButton, basicAttack, parry, skill;
        
        private Tween backAnim;

        [SerializeField] private Image flamme;
        [SerializeField] private GameObject fury_desc;

        private ActionPickerElement.PlayerActionType selectedAction;
        
        [Serializable]
        public class ActionButton
        {
            public ActionPickerElement.PlayerActionType action;
            public Image box, ornament, background;
            public TextMeshProUGUI desc;
            public GameObject text, input;
            public float duration = .2f;

            private Tween boxTween, ornamentTween, backgroundTween, descTween;
            
            public void InitShow()
            {
                boxTween?.Kill();
                ornamentTween?.Kill();
                backgroundTween?.Kill();
                descTween?.Kill();
                
                box.fillAmount = 0;
                ornament.fillAmount = 0;
                background.fillAmount = 0;
                text.SetActive(false);
                input.SetActive(false);
                desc.DOFade(0f, 0f);
            }
            
            public IEnumerator Show()
            {
                yield return new WaitForEndOfFrame();
                backgroundTween = background.DOFillAmount(1f, duration);
                boxTween = box.DOFillAmount(1f, duration);
                ornamentTween = ornament.DOFillAmount(1f, duration);
                descTween = desc.DOFade(1f, duration);
                yield return new WaitForSeconds(duration);
                text.SetActive(true);
                input.SetActive(true);
            }

            public void InitHide()
            {
                boxTween?.Kill();
                ornamentTween?.Kill();
                backgroundTween?.Kill();
                descTween?.Kill();
            }

            public IEnumerator Hide()
            {
                yield return new WaitForEndOfFrame();
                backgroundTween = background.DOFillAmount(0f, duration);
                boxTween = box.DOFillAmount(0f, duration);
                ornamentTween = ornament.DOFillAmount(0f, duration);
                descTween = desc.DOFade(0f, duration);
                yield return new WaitForSeconds(duration);
                text.SetActive(false);
                input.SetActive(false);
            }
        }

        [FoldoutGroup("Anim")] public ActionButton[] ActionButtonsAnims;
        [FoldoutGroup("Anim")] public Image middle;
        
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
            middle.fillAmount = 0;
            CanvasGroup.alpha = 1f;

            if(opening != null) StopCoroutine(opening);
            opening = StartCoroutine(ShowAnimIE());

            ActionButtonsAnims.ForEach(w => w.InitShow());
        }

        IEnumerator ShowAnimIE()
        {
            middle.DOFillAmount(1f, .2f * 2f);
            for (int i = 0; i < ActionButtonsAnims.Length; i++)
            {
                yield return ActionButtonsAnims[i].Show();
            }
            yield break;
        }

        [Button]
        public void HideAnim()
        {
            if(opening != null) StopCoroutine(opening);
            opening = StartCoroutine(HideAnimIE());
            
            ActionButtonsAnims.ForEach(w => w.InitHide());
        }
        
        IEnumerator HideAnimIE()
        {
            middle.DOFillAmount(0f, .2f * 2f);
            for (int i = 0; i < ActionButtonsAnims.Length; i++)
            {
                if (ActionButtonsAnims[i].action != selectedAction)
                {
                    yield return ActionButtonsAnims[i].Hide();
                }
            }

            yield return new WaitForSeconds(.2f);
            CanvasGroup.alpha = 0f;
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
                selectedAction = action;

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
