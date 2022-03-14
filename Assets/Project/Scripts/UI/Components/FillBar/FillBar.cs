using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class FillBar : UIElement
    {
        [SerializeField] private float maxValue;
        [SerializeField] private float currentValue;

        public float valueInPercent => currentValue / maxValue;

        [SerializeField] private RectTransform backGround, fill, preview;

        [SerializeField] private float duration;
        [SerializeField] private Ease ease;

        [SerializeField] private Color background, previewMinus, previewAdd;
        [SerializeField] private Gradient fillColor;

        private Image backgroundImage, previewImage, fillImage;
        [SerializeField] private Image cross, hearth;

        protected override void OnStart()
        {
            base.OnStart();
            backgroundImage = backGround.GetComponent<Image>();
            previewImage = preview.GetComponent<Image>();
            fillImage = fill.GetComponent<Image>();
        }

        [Button]
        public void SetFill(float newValue)
        {
            HidePreview();
            newValue = Mathf.Clamp(newValue, 0f, maxValue);

            currentValue = newValue;

            //FORMULA = fill.SetRight((1 - valueInPercent) * RectTransform.rect.width);
            DOTween.To(
                () => fill.GetRight(),
                (x) => fill.SetRight(x),
                (1 - valueInPercent) * RectTransform.rect.width,
                duration
            ).SetEase(ease).OnUpdate(delegate
            {
                fillImage.color = fillColor.Evaluate(valueInPercent);
            });
        }

        public void ShowPreview()
        {
            preview.gameObject.SetActive(true);
        }

        public void HidePreview()
        {
            preview.gameObject.SetActive(false);
            cross.gameObject.SetActive(false);
            hearth.color = Color.white;

        }
        
        [Button]
        public void SetPreview(float newValue)
        {
            ShowPreview();

            newValue = Mathf.Clamp(newValue, 0f, maxValue);
            
            if (newValue < currentValue)
            {
                preview.SetRight(fill.GetRight());
                preview.SetLeft((newValue / maxValue) * RectTransform.rect.width);
                previewImage.color = previewMinus;
            }
            else if (newValue > currentValue)
            {
                preview.SetLeft(RectTransform.rect.width - fill.GetRight());
                preview.SetRight( ( 1 - (newValue / maxValue)) * RectTransform.rect.width);
                previewImage.color = previewAdd;
            }

            if(newValue == 0)
            {
                cross.gameObject.SetActive(true);
                hearth.color = Color.black;
            }
            else
            {
                cross.gameObject.SetActive(false);
                hearth.color = Color.white;
            }
        }

        public void SetMaxValue(float value) => maxValue = value;
    }
}
