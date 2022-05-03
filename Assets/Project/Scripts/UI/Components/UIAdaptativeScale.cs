using System;
using System.Collections;
using System.Collections.Generic;
using TheFowler;
using UnityEngine;

public class UIAdaptativeScale : MonoBehaviour
{
    public float coef = 0.01f;
    public float maxSize = 1.2f;
    public float minSize = .8f;
    
    private Transform camTransform;

    private void Start()
    {
        camTransform = CameraManager.Camera.transform;
    }

    private void Update()
    {
        var dist = Vector3.Distance(transform.position, camTransform.position);

        dist = Mathf.Clamp(dist, minSize, maxSize);
        
        transform.localScale = Vector3.one * dist * coef;
    }
}
