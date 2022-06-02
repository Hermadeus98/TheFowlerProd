using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TheFowler
{
    public class ExclamationElement : MonoBehaviour
    {
        private Tween tween;
        [SerializeField] private RectTransform rect;


        private void OnEnable()
        {
            tween = rect.DOAnchorPosY(rect.anchoredPosition.y + 10, .5f).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            tween.Kill();
        }
    }
}

