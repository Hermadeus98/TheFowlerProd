using System;
using System.Collections.Generic;
using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class SoundManager : MonoBehaviourSingleton<SoundManager>
    {
        [SerializeField]
        private AudioDatabase _audioDatabase;

        public static AudioDatabase AudioDatabase => Instance._audioDatabase;


        [Button]
        public static void PlaySound(AudioGenericEnum key, GameObject handler)
        {
            if (key == AudioGenericEnum.NULL)
            {
                QRDebug.Log("AUDIO", FrenchPallet.SUN_FLOWER, "Audio Generic Enum is equal to NULL.");
                return;
            }

            if (handler == null)
                handler = Instance.gameObject;
            
            AudioDatabase.GetElement(key).Post(handler);
        }
        [Button]
        public static void PlaySound(AK.Wwise.Event sound, GameObject handler)
        {

            sound.Stop(handler);
            sound.Post(handler);
        }

        public static void StopSound(AK.Wwise.Event sound, GameObject handler)
        {
            if (sound == null) return;
            sound.Stop(handler);
        }

        public static void PlaySoundDamageTaken(BattleActor target, DamageCalculator.ResistanceFaiblesseResult result)
        {
            if (target is AllyActor)
            {
                PlaySound(AudioGenericEnum.TF_SFX_Combat_Generic_Ally_DamageTaken, target.gameObject);
            }
            else if (target is EnemyActor)
            {
                switch (result)
                {
                    case DamageCalculator.ResistanceFaiblesseResult.NEUTRE:
                        PlaySound(AudioGenericEnum.TF_SFX_Combat_Generic_Enemy_DamageTaken_Neutral, target.gameObject);
                        break;
                    case DamageCalculator.ResistanceFaiblesseResult.FAIBLESSE:
                        PlaySound(AudioGenericEnum.TF_SFX_Combat_Generic_Enemy_DamageTaken_Weak, target.gameObject);
                        break;
                    case DamageCalculator.ResistanceFaiblesseResult.RESISTANCE:
                        PlaySound(AudioGenericEnum.TF_SFX_Combat_Generic_Enemy_DamageTaken_Resist, target.gameObject);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(result), result, null);
                }
            }
        }
        [Button]
        public static void Mute()
        {
            QRDebug.Log("AUDIO", FrenchPallet.SUN_FLOWER, "MUTE");
            PlaySound(AudioGenericEnum.TF_Main_SetMuteOn, null);
        }
        [Button]
        public static void UnMute()
        {
            QRDebug.Log("AUDIO", FrenchPallet.SUN_FLOWER, "UNMUTE");
            PlaySound(AudioGenericEnum.TF_Main_SetMuteOff, null);
        }
    }
}
