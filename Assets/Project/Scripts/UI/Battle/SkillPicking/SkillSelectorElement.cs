using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace TheFowler
{
    public class SkillSelectorElement : UISelectorElement
    {
        [SerializeField, ReadOnly] private Spell referedSpell;
        [SerializeField] private TextMeshProUGUI manaCostText;
        
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (args is WrapperArgs<Spell> cast)
            {
                referedSpell = cast.Arg;
                text.SetText(referedSpell.SpellName);
                manaCostText.SetText(referedSpell.ManaCost.ToString());
            }
        }
    }
}
