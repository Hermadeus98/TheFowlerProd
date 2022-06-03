using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimatedIcon : MonoBehaviour
{
    private RectTransform rt;
    private Tween anim;
    private Sequence beat;

    public Ease easing = Ease.OutBack;
    public float duration = .2f;

    private void OnEnable()
    {
        if (TryGetComponent(out RectTransform rt))
        {
            
        }

    }

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

    private void OnDestroy()
    {
        anim?.Kill();
        beat?.Kill();
    }
}
