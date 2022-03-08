using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class ImagePopup : SerializedMonoBehaviour
    {
        public static ImagePopup Instance;
        
        [SerializeField] private InOutComponent InOutComponent;
        [SerializeField] private Image icon;
        [SerializeField] private Sprite fury;

        public void PopupFury() => Popup(fury);

        private void Awake()
        {
            Instance = this;
        }

        [Button]
        public void Popup(Sprite sprite)
        {
            icon.sprite = sprite;

            var sequence = DOTween.Sequence();
            sequence.Append(icon.DOFade(1f, InOutComponent.in_duration).SetEase(InOutComponent.in_ease));
            sequence.Append(icon.DOFade(0f, InOutComponent.out_duration).SetEase(InOutComponent.out_ease).SetDelay(InOutComponent.between_duration));
            sequence.Play();
        }
    }
}
