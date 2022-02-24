using System.Collections.Generic;
using QRCode;
using UnityEngine;

namespace TheFowler
{
    public class SoundManager : MonoBehaviourSingleton<SoundManager>
    {
        [SerializeField]
        private AudioDatabase _audioDatabase;

        public static AudioDatabase AudioDatabase => Instance._audioDatabase;

        public static void PlaySound(AudioGenericEnum key, GameObject handler)
        {
            AudioDatabase.GetElement(key).Post(handler);
        }

        public static void PlaySound(AK.Wwise.Event sound, GameObject handler)
        {
            sound.Stop(handler);
            sound.Post(handler);
        }

        public static void StopSound(AK.Wwise.Event sound, GameObject handler)
        {
            sound.Stop(handler);
        }

    }
}
