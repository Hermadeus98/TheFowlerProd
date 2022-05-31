using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.Rendering;
using DG.Tweening;

namespace TheFowler
{
    [AddComponentMenu("")]
    [FeedbackPath("TheFowler/Post Process")]
    public class MMFowlerPP : MMFeedback
    {

        public float durationIn, waitDuration, durationOut;
        public Volume volume;
        private Tween tween;
        private Coroutine cor;

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            if(cor != null)
                StopCoroutine(cor);
            cor = StartCoroutine(WaitPP());
        }

        private IEnumerator WaitPP()
        {
            tween?.Kill();
            tween = DOTween.To(() => volume.weight, x => volume.weight = x, 1, durationIn);
            yield return new WaitForSeconds(waitDuration);
            tween = DOTween.To(() => volume.weight, x => volume.weight = x, 0, durationOut);

            yield break;
        }
    }
}
