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
        public static TurnTransitionView Instance;
        
        [SerializeField] private Image icon;
        [SerializeField] private StringSpriteDatabase sprites;
        [SerializeField] private InOutComponent InOutComponent;

        [SerializeField] private RawImage image;
        [SerializeField] private Image mask;
        [SerializeField] private float transitionDuration = .2f;

        [SerializeField] private Animator Animator;

        private Coroutine c;
        
        public float WaitTime => transitionDuration;
            //InOutComponent.in_duration + InOutComponent.between_duration + InOutComponent.out_duration;

        public static bool isLock = false;

        protected override void OnStart()
        {
            base.OnStart();
            Instance = this;
        }
        
        public void CameraSwipTransition(Action transitionEvent)
        {
            if (c != null)
            {
                StopCoroutine(c);
                ForceHide();
                return;
            }

            c = StartCoroutine(CameraSwipTransitionIE(transitionEvent));
        }
        
        private IEnumerator CameraSwipTransitionIE(Action transitionEvent)
        {
            if(!isLock) CanvasGroup.alpha = 1;
            mask.fillAmount = 1f;
            
            var cam = CameraManager.Camera;
            Vector2 dim = Canvas.GetComponent<RectTransform>().sizeDelta;
            RenderTexture targettex = new RenderTexture((int)dim.x, (int)dim.y, 8);
            cam.targetTexture = targettex;
            cam.Render();
            cam.targetTexture = null;
            image.texture = targettex;
            image.rectTransform.sizeDelta = new Vector2((int)dim.x, (int)dim.y);

            if(transitionEvent != null)
            {
                transitionEvent?.Invoke();
            }


            Animator.ResetTrigger("play");
            Animator.SetTrigger("play");

            yield return new WaitForSeconds(transitionDuration);
            CanvasGroup.alpha = 0;
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

        public void ForceHide()
        {
            if (c == null) return;
            StopCoroutine(c);
            CanvasGroup.alpha = 0;
        }
    }
}
