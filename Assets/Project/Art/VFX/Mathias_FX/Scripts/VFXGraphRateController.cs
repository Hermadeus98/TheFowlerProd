using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]
public class VFXGraphRateController : MonoBehaviour
{
    [SerializeField] private VisualEffect VisualEffect;
    [SerializeField, Range(0f, 4f)] private float rate = 100f;

    [OnValueChanged("this.UpdateRate")]
    public float Rate
    {
        get => rate;
        set
        {
            rate = value;
            UpdateRate();
        }
    }
    
    private void UpdateRate()
    {
        VisualEffect.playRate = Rate;
    }
}
