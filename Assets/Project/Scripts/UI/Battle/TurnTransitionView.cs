using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QRCode;
using Sirenix.OdinInspector;
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

        [SerializeField] private Animator Animator;
        
        public float WaitTime =>
            InOutComponent.in_duration + InOutComponent.between_duration + InOutComponent.out_duration;


        [Button]
        private void Test() => CameraSwipTransition(null);
        
        public void CameraSwipTransition(Action transitionEvent)
        {
            StartCoroutine(CameraSwipTransitionIE(transitionEvent));
        }
        
        private IEnumerator CameraSwipTransitionIE(Action transitionEvent)
        {
            CanvasGroup.alpha = 1;
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

            Animator.SetTrigger("play");

            yield return new WaitForSeconds(transitionDuration);
            CanvasGroup.alpha = 0;

            
            /*mask.DOFillAmount(0, transitionDuration).OnComplete(delegate
            {
                CanvasGroup.alpha = 0;
            });*/
            
            yield break;
        }
        
        /// <summary>
        /// NE PAS UTILISER, utiliser CameraSwipTransition.
        /// </summary>
        public override void Show()
        {
            base.Show();

            Sprite spr = null;
            if (BattleManager.IsAllyTurn)
            {
                spr = sprites.GetElement("ally");
            }
            else
            {
                spr = sprites.GetElement("enemy");
            }
        }
    }
}
