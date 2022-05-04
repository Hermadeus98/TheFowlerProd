using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TheFowler
{
    public class PopupText : SerializedMonoBehaviour
    {
        [SerializeField] private TextMesh text, back;
        [SerializeField] private SpriteRenderer image;

        public float duration = 1.2f;
        public Vector3 offset;
        public Color Color = Color.red;

        public float randomOffsetX = 0, randomOffsetY = 0, randomOffsetZ = 0;

        public Sprite popupSprite = null;
        public SpriteRenderer extraImage;

        public float minFontSize, maxFontSize;
        
        [Button]
        public void Play(string message)
        {
            text.text = message;
            back.text = message;
            
            text.color = Color;

            if (popupSprite != null)
            {
                text.color = Color.clear;
                image.gameObject.SetActive(true);
                image.sprite = popupSprite;
            }
            else
            {
                image.gameObject.SetActive(false);
            }

            var _offset = new Vector3(offset.x + Random.Range(-randomOffsetX, randomOffsetX), offset.y + Random.Range(-randomOffsetY, randomOffsetY), offset.z + Random.Range(-randomOffsetZ, randomOffsetZ));
            
            transform.DOMove(transform.position + _offset, duration).SetEase(Ease.OutCubic).OnComplete(delegate
            {
                if (popupSprite == null)
                {
                    var sequence = AnimateEnd(text);
                    AnimateEnd(back);
                    sequence.OnComplete(delegate { Destroy(gameObject); });
                    sequence.Play();
                    
                    var sequenceExtraImage = DOTween.Sequence();
                    sequenceExtraImage.Append(extraImage.DOFade(1f, .1f).SetEase(Ease.InOutSine));
                    sequenceExtraImage.Append(extraImage.DOFade(0f, .1f).SetEase(Ease.InOutSine));
                    sequenceExtraImage.SetLoops(3);
                    sequenceExtraImage.Play();
                }
                else
                {
                    var sequence = DOTween.Sequence();
                    sequence.Append(image.DOFade(1f, .1f).SetEase(Ease.InOutSine));
                    sequence.Append(image.DOFade(0f, .1f).SetEase(Ease.InOutSine));
                    sequence.SetLoops(3);
                    sequence.OnComplete(delegate { Destroy(gameObject); });
                    sequence.Play();
                }
            });
        }

        private Sequence AnimateEnd(TextMesh t)
        {
            var sequence = DOTween.Sequence();
            var d = .1f;
            sequence.Append(ChangeTextColor(t, new Color(t.color.r, t.color.g, t.color.b, 1f), d));
            sequence.Append(ChangeTextColor(t, new Color(t.color.r, t.color.g, t.color.b, 0f), d));
            sequence.SetLoops(3);
            return sequence;
        }
        
        private Tween ChangeTextColor(TextMesh t,  Color c, float duration)
        {
            return DOTween.To(
                    () => t.color,
                    (x) => t.color = x,
                    c,
                    duration
                )
                .SetEase(Ease.InOutSine);
        }

        public void SetSizePercent(float percent)
        {
            var size = Mathf.Lerp(minFontSize, maxFontSize, percent);
            text.characterSize = size;
            back.characterSize = size;
        }

        public void Destroy()
        {
            DestroyImmediate(gameObject, true);
        }
    }
}
