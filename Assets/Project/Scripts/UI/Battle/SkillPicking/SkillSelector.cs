using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using QRCode;
using QRCode.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class SkillSelector : UISelector
    {
        [SerializeField] private RectTransform cursor;

        [SerializeField] private Image up, down;

        public SpellHandler currentSpellHandler;
        
        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            OnSelect += RepositionningCursor;
        }

        protected override void UnregisterEvent()
        {
            base.UnregisterEvent();
            OnSelect -= RepositionningCursor;
        }

        public void Refresh(SpellHandler spellHandler)
        {
            currentSpellHandler = spellHandler;

            ResetElements();
            HideAllElements();
            DeselectedAll();
            elements.ForEach(w => w.Hide());
            elements.Clear();

            for (int i = 0; i < spellHandler.spells.Count; i++)
            {
                elements.Add(transform.GetChild(i).GetComponent<SkillSelectorElement>());
                elements[i].Show();
                elements[i].Refresh(new WrapperArgs<SpellHandler.SpellHandled>(spellHandler.spells[i]));
            }

            StartCoroutine(ShowElements());

            currentIndex = 0;

            SelectElement();
        }



        private IEnumerator ShowElements()
        {
            elements.ForEach(w => w.canvasGroup.alpha = 0);

            for (int i = 0; i < elements.Count(); i++)
            {
                elements.ElementAt(i).canvasGroup.DOFade(1f, .1f);
                yield return new WaitForSeconds(.1f);
            }
        }

        public override void Show()
        {
            base.Show();
            canvasGroup.alpha = 1;
        }

        public override void Hide()
        {
            base.Hide();
            canvasGroup.alpha = 0f;
        }
        
        public bool WaitChoice(out SkillSelectorElement skillSelectorElement)
        {
            var skillElement = elements[currentIndex] as SkillSelectorElement;
            
            if (Inputs.actions["Select"].WasPressedThisFrame() && skillElement != null)
            {
                if (skillElement.isSelectable)
                {
                    Hide();
                    skillSelectorElement = skillElement;
                    return true;
                }
            }

            skillSelectorElement = null;
            return false;
        }

        private void RepositionningCursor(UISelectorElement element)
        {
            cursor.DOMoveY(element.RectTransform.position.y, .1f);
            
            var skillPickingView = UI.GetView<SkillPickingView>(UI.Views.SkillPicking);
            skillPickingView.descriptionText.SetText(((SkillSelectorElement)element).referedSpell.SpellDescription);
            
            //skillPickingView.easyDescriptionText.SetText(((SkillSelectorElement)element).referedSpell.EasySpellDescription);
            //skillPickingView.targetDescription.SetText(((SkillSelectorElement)element).referedSpell.TargetDescription);
        }

        protected override void OnNavigate()
        {
            base.OnNavigate();
            SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_Hover, gameObject);

            if (currentIndex == 0)
            {
                up.rectTransform.DOScale(0f, .2f).SetEase(Ease.InOutSine);
            }
            else
            {
                up.rectTransform.DOScale(1f, .2f).SetEase(Ease.InOutSine);
            }
            
            if (currentIndex == elements.Count - 1)
            {
                down.rectTransform.DOScale(0f, .2f).SetEase(Ease.InOutSine);
            }
            else
            {
                down.rectTransform.DOScale(1f, .2f).SetEase(Ease.InOutSine);
            }
        }
    }
}
