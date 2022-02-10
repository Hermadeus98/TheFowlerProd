using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using UnityEngine;

namespace TheFowler
{
    public class SkillSelector : UISelector
    {
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
            
            if (Inputs.actions["Select"].WasPressedThisFrame())
            {
                Hide();
                skillSelectorElement = skillElement;
                return true;
            }

            skillSelectorElement = null;
            return false;
        }
    }
}
