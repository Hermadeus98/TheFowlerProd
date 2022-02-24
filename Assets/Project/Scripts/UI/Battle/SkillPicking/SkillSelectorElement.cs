using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class SkillSelectorElement : UISelectorElement
    {
        [SerializeField, ReadOnly] public Spell referedSpell;
        [SerializeField] private TextMeshProUGUI manaCostText;
        [SerializeField] private Image spellTypeIcon;
        [SerializeField] private SpellTypeDatabase SpellTypeDatabase;

        [SerializeField] private Image selectableFeedback;
        
        public bool isSelectable { get; set; } = false;
        
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (args is WrapperArgs<Spell> cast)
            {
                referedSpell = cast.Arg;
                text.SetText(referedSpell.SpellName);
                manaCostText.SetText(referedSpell.ManaCost.ToString());
                spellTypeIcon.sprite = SpellTypeDatabase.GetElement(referedSpell.SpellType);
                
                if (BattleManager.CurrentBattleActor.Mana.HaveEnoughMana(referedSpell.ManaCost))
                {
                    isSelectable = true;
                    selectableFeedback.enabled = false;
                }
                else
                {
                    isSelectable = false;
                    selectableFeedback.enabled = true;
                }
            }
        }
    }
}
