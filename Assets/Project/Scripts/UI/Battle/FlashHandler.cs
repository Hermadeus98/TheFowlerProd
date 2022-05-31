using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRCode;
using DG.Tweening;
using UnityEngine.UI;
namespace TheFowler
{
    public class FlashHandler : MonoBehaviourSingleton<FlashHandler>
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image image;
        private Tween tween;
        private Coroutine cor;
        public void Flash(float durationIn, float wait, float durationOut, Color color, Sprite sprite)
        {
            if(cor != null)
            {
                StopCoroutine(cor);
            }

            cor = StartCoroutine(WaitFlash(durationIn, wait, durationOut, color, sprite));

        }

        private IEnumerator WaitFlash(float durationIn, float wait, float durationOut, Color color, Sprite sprite)
        {

            if (sprite != null) image.overrideSprite = sprite;
            else image.overrideSprite = null;

            image.color = color;
            tween = DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, durationIn);
            yield return new WaitForSeconds(wait);
            tween = DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, durationOut);
        }
    }

}
