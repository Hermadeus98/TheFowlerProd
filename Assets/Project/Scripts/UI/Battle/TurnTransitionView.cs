using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QRCode;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class TurnTransitionView : UIView
    {
        [SerializeField] private Image icon;
        [SerializeField] private StringSpriteDatabase sprites;
        [SerializeField] private InOutComponent InOutComponent;

        public float WaitTime =>
            InOutComponent.in_duration + InOutComponent.between_duration + InOutComponent.out_duration;
        
        public override void Show()
        {
            base.Show();
            
            CanvasGroup.alpha = 0;

            if (BattleManager.IsAllyTurn)
            {
                icon.sprite = sprites.GetElement("ally");
            }
            else
            {
                icon.sprite = sprites.GetElement("enemy");
            }

            var sequence = DOTween.Sequence();
            sequence.Append(CanvasGroup.DOFade(1f, InOutComponent.in_duration).SetEase(InOutComponent.in_ease));
            sequence.Append(CanvasGroup.DOFade(0f, InOutComponent.out_duration).SetEase(InOutComponent.out_ease).SetDelay(InOutComponent.between_duration));
            sequence.OnComplete(Hide);
            sequence.Play();
        }
    }
}
