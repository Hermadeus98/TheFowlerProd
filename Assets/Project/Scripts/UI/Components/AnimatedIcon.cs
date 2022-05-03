using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimatedIcon : MonoBehaviour
{
    private RectTransform rt => GetComponent<RectTransform>();
    private Tween anim;
    private Sequence beat;

    public Ease easing = Ease.OutBack;
    public float duration = .2f;

    public void Show()
    {
        anim?.Kill();
        anim = rt.DOScale(Vector3.one, duration).SetEase(easing).OnComplete(Beat);
    }

    public void Hide()
    {
        beat?.Kill();

        anim?.Kill();
        anim = rt.DOScale(Vector3.zero, duration).SetEase(easing);
    }

    private void Beat()
    {
        beat = DOTween.Sequence();
        beat.Append(rt.DOScale(1.1f, .2f).SetEase(Ease.InSine));
        beat.Append(rt.DOScale(1f, .2f).SetEase(Ease.InSine));
        beat.SetLoops(-1);
        beat.Play();
    }
}
