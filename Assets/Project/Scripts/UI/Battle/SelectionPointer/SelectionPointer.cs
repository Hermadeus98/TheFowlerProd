using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Tools;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class SelectionPointer : UIElement
    {
        [SerializeField] private CanvasGroup selector;
        //[SerializeField] private Image selectorImage;
        //public Color targetEnemyColor, emitterColor, targetAllyColor;

        private Tween animTween;

        private Sequence anim;

        private float y;

        public override void Show()
        {
            base.Show();
            animTween?.Kill();
            selector.alpha = 1;

            y = transform.position.y;
            anim?.Kill();

            /*anim = DOTween.Sequence();
            anim.Append(transform.DOMoveY(y + .1f, .5f)).SetEase(Ease.InOutSine);
            anim.Append(transform.DOMoveY(y, .5f)).SetEase(Ease.InOutSine);
            anim.SetLoops(-1);
            anim.Play();*/
        }

        public override void Hide()
        {
            base.Hide();
            animTween?.Kill();
            selector.alpha = 0;
            
            anim?.Kill();
            transform.position.MMSetY(y);
            //SetTargetEnemyColor();
        }

        //public void SetTargetEnemyColor()=> selectorImage.color = targetEnemyColor;
        //public void SetTargetAllyColor()=> selectorImage.color = targetAllyColor;
        //public void SetEmitterColor()=> selectorImage.color = emitterColor;
    }
}
