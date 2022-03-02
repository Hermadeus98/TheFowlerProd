using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QRCode;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class TurnTransitionView : UIView
    {
        [SerializeField] private Image icon;
        [SerializeField] private StringSpriteDatabase sprites;
        [SerializeField] private InOutComponent InOutComponent;

        [SerializeField] private RawImage image;
        [SerializeField] private Image mask;
        [SerializeField] private float transitionDuration = .2f;
        
        public float WaitTime =>
            InOutComponent.in_duration + InOutComponent.between_duration + InOutComponent.out_duration;


        public void CameraSwipTransition(Action transitionEvent)
        {
            StartCoroutine(CameraSwipTransitionIE(transitionEvent));
        }
        
        private IEnumerator CameraSwipTransitionIE(Action transitionEvent)
        {
            CanvasGroup.alpha = 1;
            Debug.Log("SWIPE");
            
            mask.fillAmount = 1f;
            
            var cam = CameraManager.Camera;
            Vector2 dim = Canvas.GetComponent<RectTransform>().sizeDelta;
            RenderTexture targettex = new RenderTexture((int)dim.x, (int)dim.y, 8);
            cam.targetTexture = targettex;
            cam.Render();
            cam.targetTexture = null;
            image.texture = targettex;
            image.rectTransform.sizeDelta = new Vector2((int)dim.x, (int)dim.y);

            transitionEvent?.Invoke();
            mask.DOFillAmount(0, transitionDuration).OnComplete(delegate
            {
                CanvasGroup.alpha = 0;
            });
            
            yield break;
        }
        
        public override void Show()
        {
            base.Show();
            
            CanvasGroup.alpha = 0;

            if (BattleManager.IsAllyTurn)
            {
                icon.sprite = sprites.GetElement("ally");
            }
            else
            {
                icon.sprite = sprites.GetElement("enemy");
            }

            var sequence = DOTween.Sequence();
            sequence.Append(CanvasGroup.DOFade(1f, InOutComponent.in_duration).SetEase(InOutComponent.in_ease));
            sequence.Append(CanvasGroup.DOFade(0f, InOutComponent.out_duration).SetEase(InOutComponent.out_ease).SetDelay(InOutComponent.between_duration));
            sequence.OnComplete(Hide);
            sequence.Play();
        }
    }
}
