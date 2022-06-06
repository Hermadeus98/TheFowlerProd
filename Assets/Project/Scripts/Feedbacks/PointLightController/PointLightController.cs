using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace TheFowler
{
    [ExecuteInEditMode]
    public class PointLightController : SerializedMonoBehaviour
    {
        [SerializeField] private Light light;
        [SerializeField] private float strenght = 600f;
        [SerializeField] private float duration = 2f;
        [SerializeField] private AnimationCurve controller;

        private float s;

        public bool destroyAtEnd = true;
        
        private void OnEnable()
        {
            Play();
        }

        [Button]
        public void Play()
        {
            s = 0f;

            DOTween.To(
                () => s,
                (x) => s = x,
                strenght,
                duration
            ).SetEase(controller).OnComplete(delegate
            {
                if (destroyAtEnd) Destroy(gameObject);
                else
                {
                    s = 0f;
                    enabled = false;
                }
            });
        }
        
        private void Update()
        {
            light.intensity = s;
        }
    }
}
