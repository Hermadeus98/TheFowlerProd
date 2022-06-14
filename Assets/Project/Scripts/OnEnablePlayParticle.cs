using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OnEnablePlayParticle : MonoBehaviour
{
    public ParticleSystem ps;

    private void OnEnable()
    {
        ps?.Play();
    }

    private void OnDisable()
    {
        ps?.Stop();
    }
}
