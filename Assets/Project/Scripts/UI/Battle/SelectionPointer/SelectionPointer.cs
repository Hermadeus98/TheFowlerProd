using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

namespace TheFowler
{
    public class SelectionPointer : UIElement
    {
        [SerializeField] private CanvasGroup selector;

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
        }
    }
}
