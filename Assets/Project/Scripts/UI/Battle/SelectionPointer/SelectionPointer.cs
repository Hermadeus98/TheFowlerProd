using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class SelectionPointer : UIElement
    {
        [SerializeField] private CanvasGroup selector;
        [SerializeField] private Image selectorImage;
        public Color targetEnemyColor, emitterColor, targetAllyColor;

        private Tween animTween;

        public override void Show()
        {
            base.Show();
            animTween?.Kill();
            selector.alpha = 1;
        }

        public override void Hide()
        {
            base.Hide();
            animTween?.Kill();
            selector.alpha = 0;
            SetTargetEnemyColor();
        }

        public void SetTargetEnemyColor()=> selectorImage.color = targetEnemyColor;
        public void SetTargetAllyColor()=> selectorImage.color = targetAllyColor;
        public void SetEmitterColor()=> selectorImage.color = emitterColor;
    }
}
