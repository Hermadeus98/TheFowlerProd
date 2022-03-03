using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class PopupText : SerializedMonoBehaviour
    {
        [SerializeField] private TextMesh text;

        public float duration = 1.2f;
        public Vector3 offset;

        
        [Button]
        public void Play(string message)
        {
            text.text = message;

            transform.DOMove(transform.position + offset, duration).SetEase(Ease.OutCubic).OnComplete(delegate
            {
                DOTween.To(
                    () => text.color,
                    (x) => text.color = x,
                    new Color(text.color.r, text.color.g, text.color.b, 0f),
                    .2f
                )
                    .SetEase(Ease.InOutSine)
                    .OnComplete(delegate
                    {
                        Destroy();
                    });
            });
        }

        public void Destroy()
        {
            DestroyImmediate(gameObject, true);
        }
    }
}
