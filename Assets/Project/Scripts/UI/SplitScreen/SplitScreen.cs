using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class SplitScreen : SerializedMonoBehaviour
    {
        public RectTransform splitScreenRect;
        public Image frame;

        private Tween move, fill;
        
        [Button]
        public void Show()
        {
            splitScreenRect.transform.position = new Vector3(-splitScreenRect.sizeDelta.x, splitScreenRect.position.y, splitScreenRect.position.z);
            move?.Kill();
            move = splitScreenRect.DOAnchorPosX(0, .2f).SetEase(Ease.OutBack);
            frame.fillAmount = 0f;
            fill?.Kill();
            fill = frame.DOFillAmount(1f, .2f).SetEase(Ease.OutBack);
        }

        [Button]
        public void Hide()
        {
            move?.Kill();
            move = splitScreenRect.DOAnchorPosX(-splitScreenRect.sizeDelta.x, .2f).SetEase(Ease.OutBack);
            fill?.Kill();
            fill = frame.DOFillAmount(0f, .2f).SetEase(Ease.OutBack);
        }
    }
}
