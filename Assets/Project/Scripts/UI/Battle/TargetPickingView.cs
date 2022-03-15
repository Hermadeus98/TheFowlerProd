using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace TheFowler
{
    public class TargetPickingView : UIView
    {
        public TextMeshProUGUI spellName, targetText, easyDesc;

        private Tween opening, move;

        [SerializeField] private RectTransform box;
        
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
        }

        public void Refresh(Spell spell)
        {
            spellName.SetText(spell.SpellName);
            targetText.SetText(spell.TargetDescription);
            easyDesc.SetText(spell.EasySpellDescription);
        }
    }
}