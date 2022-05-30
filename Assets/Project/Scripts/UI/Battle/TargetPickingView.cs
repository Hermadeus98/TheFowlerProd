using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using QRCode;


namespace TheFowler
{
    public class TargetPickingView : UIView
    {
        public TextMeshProUGUI spellName;

        private Tween opening, move;

        [SerializeField] private RectTransform box;

        [SerializeField] private SkillSelectorElement SkillSelectorElement;

        public override void Show()
        {
            base.Show();
            opening?.Kill();
            opening = CanvasGroup.DOFade(1f, .2f);

            box.position = new Vector3(-box.sizeDelta.x, box.position.y, 0);
            move?.Kill();
            move = box.DOMoveX(0f, .5f);
        }

        public override void Hide()
        {
            base.Hide();
            
            opening?.Kill();
            opening = CanvasGroup.DOFade(0f, .2f);
            
            move?.Kill();
            move = box.DOMoveX(-box.sizeDelta.x, .5f);

            SkillSelectorElement.manaCostText.gameObject.SetActive(true);
            
            UI.GetView<FuryView>("FuryView").Hide();
        }

        public void Refresh(Spell spell)
        {
            SkillSelectorElement.referedSpell = spell;
            SkillSelectorElement.DeSelect();
            SkillSelectorElement.Select();
            //SkillSelectorElement.Refresh(new WrapperArgs<SpellHandler.SpellHandled>(BattleManager.CurrentBattleActor.GetBattleComponent<SpellHandler>().GetSpellHandled(spell)));
            if (LocalisationManager.language == Language.ENGLISH)
            {
                spellName.SetText(spell.SpellName);
            }
            else
            {
                spellName.SetText(spell.SpellNameFrench);
            }


            if(spell.Cooldown <= 0)
            {

                SkillSelectorElement.manaCostText.gameObject.SetActive(false);
            }
        }
    }
}