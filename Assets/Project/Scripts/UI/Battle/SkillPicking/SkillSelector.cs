using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QRCode;
using QRCode.Extensions;
using UnityEngine;

namespace TheFowler
{
    public class SkillSelector : UISelector
    {
        [SerializeField] private RectTransform cursor;
        
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

        public void Refresh(BattleActorData battleActorData)
        {
            ResetElements();
            HideAllElements();
            DeselectedAll();
            elements.ForEach(w => w.Hide());
            elements.Clear();

            for (int i = 0; i < battleActorData.Spells.Length; i++)
            {
                elements.Add(transform.GetChild(i).GetComponent<SkillSelectorElement>());
                elements[i].Show();
                elements[i].Refresh(new WrapperArgs<Spell>(battleActorData.Spells[i]));
            }

            currentIndex = 0;
            SelectElement();
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
        }

        protected override void OnNavigate()
        {
            base.OnNavigate();
            SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_Hover, gameObject);
        }
    }
}
