using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class StateIcon : UIElement
    {
        [SerializeField] protected Image icon;

        public Image onImage;

        private Sequence sequence;
        
        public override void Show()
        {
            gameObject.SetActive(true);

            Feedback();
        }

        [Button]
        protected void Feedback()
        {
            onImage.sprite = icon.sprite;
            onImage.transform.localScale = Vector3.zero;
            onImage.DOFade(1f, 0.01f);
            
            sequence?.Kill();
            sequence = DOTween.Sequence();
            sequence.Append(onImage.transform.DOScale(Vector3.one * 1.25f, .5f).SetEase(Ease.OutCirc));
            sequence.Append(icon.DOFade(1f, 0.01f));
            sequence.Append(onImage.DOFade(0f, .25f).SetEase(Ease.OutSine));
            sequence.OnComplete(delegate
            {
                onImage.transform.localScale = Vector3.zero;
            });
            sequence.Play();
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
