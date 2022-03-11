using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

        [SerializeField] private CanvasGroup CanvasGroup;

        [SerializeField] private Image soulignage;
        
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

        private Tween s;
        public override void Select()
        {
            //canvasGroup.alpha = 1f;
            s?.Kill();
            s = soulignage.DOFillAmount(1f, .5f);
        }

        public override void DeSelect()
        {
            //canvasGroup.alpha = .5f;
            s.Kill();
            s = soulignage.DOFillAmount(0f, .5f);
        }
    }
}
