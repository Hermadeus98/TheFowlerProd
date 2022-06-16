using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRCode;
using UnityEngine.Rendering;

namespace TheFowler
{
    public class ColorBlindHandler : MonoBehaviourSingleton<ColorBlindHandler>
    {
        public Volume colorBlindVolume;
        public ColorBlindMode currentMode;

        [SerializeField] private VolumeProfile[] volumeProfiles;

        public void ChangeProfile(int mode)
        {
            colorBlindVolume.profile = volumeProfiles[mode];
            currentMode = (ColorBlindMode)mode;
        }


    }

    public enum ColorBlindMode
    {
        NORMAL = 0,
        PROTANOPIA = 1,
        PROTANOMALY = 2,
        DEUTERANOPIA = 3,
        TRITANOPIA = 4,
        TRITANOMALY = 5,
        ACHROMATOPSIA = 6,
        ACHROMATOMALY = 7
    }
}


