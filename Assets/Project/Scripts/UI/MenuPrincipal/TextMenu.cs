using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace TheFowler
{
    public class TextMenu : UISelectorElement
    {
        [SerializeField] private Color normalColor;
        [SerializeField] private Color selectedColor;

        [SerializeField] private UnityEvent OnSelect;

        private Sequence pulse;
        
        public override void Select()
        {
            text.color = selectedColor;

            pulse?.Kill();
            pulse = DOTween.Sequence();
            pulse.Append(
                text.rectTransform.DOScale(1.1f, .6f)
                );
            pulse.Append(
                text.rectTransform.DOScale(1f, .6f)
                );
            pulse.SetLoops(-1);
            pulse.Play();
        }

        public override void DeSelect()
        {
            text.color = normalColor;
            pulse?.Kill();
            text.rectTransform.DOScale(1f, .2f);
        }

        public override void OnClick()
        {
            base.OnClick();
            OnSelect?.Invoke();
        }
    }
}
