using System;
using System.Collections;
using System.Collections.Generic;
using TheFowler;
using UnityEngine;
using UnityEngine.UI;

public class VolumeUpdater : MonoBehaviour
{
    public VolumeSettings setting;

    public float GetValue()
    {
        return setting switch
        {
            VolumeSettings.MASTER => SoundManager.masterVolume,
            VolumeSettings.EFFECTS => SoundManager.EffectsVolume,
            VolumeSettings.VOICES => SoundManager.VoicesVolume,
            VolumeSettings.MUSIC => SoundManager.musicVolume,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public enum VolumeSettings
{
    MASTER,
    EFFECTS,
    VOICES,
    MUSIC
}
