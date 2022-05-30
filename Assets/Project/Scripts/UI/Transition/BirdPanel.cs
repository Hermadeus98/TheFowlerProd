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
    public class BirdPanel : MonoBehaviourSingleton<BirdPanel>
    {

        [SerializeField] private Image icon;
        [SerializeField] private StringSpriteDatabase sprites;
        [SerializeField] private InOutComponent InOutComponent;

        [SerializeField] private RawImage image;
        [SerializeField] private Image mask;
        [SerializeField] private float transitionDuration = .2f;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Canvas canvas;

        [SerializeField] private Animator Animator;

        public void Play()
        {
            StartCoroutine(CameraSwipTransitionIE());
        }

        private IEnumerator CameraSwipTransitionIE()
        {
            canvasGroup.alpha = 1;
            mask.fillAmount = 1f;

            var cam = CameraManager.Camera;
            Vector2 dim = canvas.GetComponent<RectTransform>().sizeDelta;
            RenderTexture targettex = new RenderTexture((int)dim.x, (int)dim.y, 8);
            cam.targetTexture = targettex;
            cam.Render();
            cam.targetTexture = null;
            image.texture = targettex;
            image.rectTransform.sizeDelta = new Vector2((int)dim.x, (int)dim.y);



            Animator.ResetTrigger("play");
            Animator.SetTrigger("play");

            yield return new WaitForSeconds(transitionDuration);
            canvasGroup.alpha = 0;

            yield break;
        }
    }
}

