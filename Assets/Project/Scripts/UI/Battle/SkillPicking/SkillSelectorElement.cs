using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using UnityEngine;

namespace TheFowler
{
    public class SkillSelectorElement : UISelectorElement
    {
        [SerializeField] private Spell referedSpell;
        
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (args is WrapperArgs<Spell> cast)
            {
                Debug.Log(1);
                referedSpell = cast.Arg;
                text.SetText(cast.Arg.SpellName);
            }
        }
    }
}
