using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TheFowler;
using UnityEngine;

public class UIAdaptativeScale : MonoBehaviour
{
    public float coef = 0.01f;
    public float maxSize = 1.2f;
    public float minSize = .8f;
    
    private Transform camTransform;

    [SerializeField] private bool animate = false;
    public float animDuration = .5f;
    private float animation;
    public Ease Ease = Ease.OutBounce;
    
    private void Start()
    {
        camTransform = CameraManager.Camera.transform;

        DOTween.To(
            () => animation,
            (x) => animation = x,
            1f,
            animDuration
        ).SetEase(Ease);
    }

    private void Update()
    {
        var dist = Vector3.Distance(transform.position, camTransform.position);

        dist = Mathf.Clamp(dist, minSize, maxSize);
        
        transform.localScale = Vector3.one * dist * coef * animation;
    }
}
