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
        [SerializeField] private Image cross;

        [SerializeField] private CanvasGroup CanvasGroup;

        [SerializeField] private Image soulignage;

        [SerializeField] private Color manaColorNormal, manaColorNotEnoughMana;

        [SerializeField] private Image manaLogo;

        
        public bool isSelectable { get; set; } = false;

        private Tween crossAnim;
        
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
                    canvasGroup.alpha = 1f;
                    manaCostText.color = manaColorNormal;

                    crossAnim?.Kill();
                    cross.fillAmount = 0;
                    manaLogo.DOFade(1f, 0.05f);
                    //selectableFeedback.enabled = false;
                }
                else
                {
                    isSelectable = false;
                    canvasGroup.alpha = .5f;
                    manaCostText.color = manaColorNotEnoughMana;
                    
                    crossAnim?.Kill();
                    crossAnim = cross.DOFillAmount(1f, .25f);
                    manaLogo.DOFade(.5f, 0.05f);

                    
                    //selectableFeedback.enabled = true;
                }
            }
        }

        public void Refresh(Spell spell)
        {
            referedSpell = spell;
            text.SetText(referedSpell.SpellName);
            manaCostText.SetText(referedSpell.ManaCost.ToString());
            spellTypeIcon.sprite = SpellTypeDatabase.GetElement(referedSpell.SpellType);
        }

        private Tween s;
        public override void Select()
        {
            //canvasGroup.alpha = 1f;
            s?.Kill();
            s = soulignage.DOFillAmount(1f, .35f);
        }

        public override void DeSelect()
        {
            //canvasGroup.alpha = .5f;
            s.Kill();
            s = soulignage.DOFillAmount(0f, .1f);
        }
    }
}
