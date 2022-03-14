using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler{
    public class ShockWave : SerializedMonoBehaviour
    {
        [SerializeField] private Material mat;
        [SerializeField] private float duration;
        [SerializeField] private float maxSize;
        [SerializeField] private AnimationCurve speedCurve;
        [SerializeField] private AnimationCurve sizeCurve;

        [SerializeField, ReadOnly] private float elapsedTime;
        [SerializeField, ReadOnly] private float size;
        private Tween timeTween, sizeTween;
        
        private void OnEnable()
        {
            Play();
        }

        private void OnDisable()
        {
            timeTween?.Kill();
            sizeTween?.Kill();
        }

        [Button]
        private void Play()
        {
            elapsedTime = 0f;
            size = 0f;
            
            timeTween = DOTween.To(
                () => elapsedTime,
                (x) => elapsedTime = x,
                1f,
                duration
                ).SetEase(speedCurve);
            
            sizeTween = DOTween.To(
                () => size,
                (x) => size = x,
                maxSize,
                duration
            ).SetEase(sizeCurve).OnComplete(delegate { gameObject.SetActive(false); });
        }

        private void Update()
        {
            mat.SetFloat("_ElapsedTime", elapsedTime);
            mat.SetFloat("_Size", size);
        }
    }
}
