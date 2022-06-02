using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace TheFowler
{
    public class PointLightController : SerializedMonoBehaviour
    {
        [SerializeField] private Light light;
        [SerializeField] private float strenght = 600f;
        [SerializeField] private float duration = 2f;
        [SerializeField] private AnimationCurve controller;

        private float s;
        
        private void OnEnable()
        {
            s = 0f;

            DOTween.To(
                () => s,
                (x) => s = x,
                strenght,
                duration
            ).SetEase(controller).OnComplete(delegate { Destroy(gameObject); });
        }

        private void Update()
        {
            light.intensity = s;
        }
    }
}
