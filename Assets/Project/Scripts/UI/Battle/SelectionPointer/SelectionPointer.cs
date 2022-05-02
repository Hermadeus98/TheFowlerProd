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
        public Color targetColor, emitterColor;

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
            SetTargetColor();
        }

        public void SetTargetColor()=> selectorImage.color = targetColor;
        public void SetEmitterColor()=> selectorImage.color = emitterColor;
    }
}
