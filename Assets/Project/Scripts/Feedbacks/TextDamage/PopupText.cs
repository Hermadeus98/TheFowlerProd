using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheFowler
{
    public class PopupText : SerializedMonoBehaviour
    {
        [SerializeField] private TextMesh text;

        public float duration = 1.2f;
        public Vector3 offset;
        public Color Color = Color.red;

        public float randomOffsetX = 0, randomOffsetY = 0, randomOffsetZ = 0;
        
        [Button]
        public void Play(string message)
        {
            text.text = message;
            text.color = Color;

            var _offset = new Vector3(offset.x + Random.Range(-randomOffsetX, randomOffsetX), offset.y + Random.Range(-randomOffsetY, randomOffsetY), offset.z + Random.Range(-randomOffsetZ, randomOffsetZ));
            
            transform.DOMove(transform.position + _offset, duration).SetEase(Ease.OutCubic).OnComplete(delegate
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
