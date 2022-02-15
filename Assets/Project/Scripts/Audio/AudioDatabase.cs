using System.Collections;
using System.Collections.Generic;
using QRCode;
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu(menuName = "Audio/Database")]
    public class AudioDatabase : Database<AudioGenericEnum, AK.Wwise.Event>
    {
        
    }

    public enum AudioGenericEnum
    {
        NULL,
    }
}
