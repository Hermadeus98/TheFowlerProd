using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]
public class VFXGraphPlayOnEnable : MonoBehaviour
{
    [SerializeField] private VisualEffect VisualEffect;

    private void OnEnable()
    {
        VisualEffect?.Play();
    }
}
