using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using TheFowler;
using UnityEngine;
using UnityEngine.UI;

public class VolumeUpdater : MonoBehaviour, IUpdater
{
    public VolumeSettings setting;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.1f);
        if (TryGetComponent<TextMenu>(out var t))
        {
            t.SliderSimple.value = GetValue();
            t.textSlider.text = t.SliderSimple.value + "/" + t.SliderSimple.maxValue;
        }
    }

    public float GetValue()
    {
        return setting switch
        {
            VolumeSettings.MASTER => SoundManager.masterVolume,
            VolumeSettings.EFFECTS => SoundManager.EffectsVolume,
            VolumeSettings.VOICES => SoundManager.VoicesVolume,
            VolumeSettings.MUSIC => SoundManager.musicVolume,
            VolumeSettings.AMBIANT => SoundManager.AmbiantVolume,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void Refresh()
    {
        var up = GameObject.FindObjectsOfType<VolumeUpdater>();
        up.ForEach(w => w.Apply());
    }

    public void Apply()
    {
        if (TryGetComponent<TextMenu>(out var t))
        {
            t.SliderSimple.value = GetValue();
            t.textSlider.text = t.SliderSimple.value + "/" + t.SliderSimple.maxValue;
        }
    }
}

public enum VolumeSettings
{
    MASTER,
    EFFECTS,
    VOICES,
    MUSIC,
    AMBIANT,
}
