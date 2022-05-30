using System;
using System.Collections;
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

            if (!AudioDatabase.Get.ContainsKey(key))
            {
                QRDebug.Log("AUDIO", FrenchPallet.SUN_FLOWER, "Key is not set in the database", AudioDatabase);
                return;
            }
            
            if (handler == null)
                handler = Instance.gameObject;
            
            if(!handler.gameObject.activeInHierarchy)
                return;
            
            AudioDatabase.GetElement(key)?.Post(handler);
        }
        
        [Button]
        public static AK.Wwise.Event PlaySound(AK.Wwise.Event sound, GameObject handler)
        {
            if (handler == null)
                handler = Instance.gameObject;
            
            if(!handler.gameObject.activeInHierarchy)
                return null;
            
            
            sound.Stop(handler);
            sound.Post(handler);

            return sound;
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
                        AkSoundEngine.PostTrigger("CombatHit", null);
                        break;
                    case DamageCalculator.ResistanceFaiblesseResult.FAIBLESSE:
                        PlaySound(AudioGenericEnum.TF_SFX_Combat_Generic_Enemy_DamageTaken_Weak, target.gameObject);
                        AkSoundEngine.PostTrigger("CombatHit", null);
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

        public static float masterVolume = 75f, musicVolume = 75f, VoicesVolume = 75f, EffectsVolume = 75f;
        
        public static void SetMasterVolume(float v)
        {
            masterVolume = v;
            AkSoundEngine.SetRTPCValue("Game_Volume_Master", v);

            QRDebug.Log("AUDIO", FrenchPallet.SUN_FLOWER, $"SetMasterVolume -> {v}");
        }

        public static void SetMusicVolume(float v)
        {
            musicVolume = v;
            AkSoundEngine.SetRTPCValue("Game_Volume_Music", v);

            QRDebug.Log("AUDIO", FrenchPallet.SUN_FLOWER, $"SetMusicVolume -> {v}");
        }

        public static void SetVoicesVolume(float v)
        {
            VoicesVolume = v;
            AkSoundEngine.SetRTPCValue("Game_Volume_Voices", v);

            QRDebug.Log("AUDIO", FrenchPallet.SUN_FLOWER, $"SetVoicesVolume -> {v}");
        }

        public static void SetEffectsVolume(float v)
        {
            EffectsVolume = v;
            AkSoundEngine.SetRTPCValue("Game_Volume_SFX", v);

            QRDebug.Log("AUDIO", FrenchPallet.SUN_FLOWER, $"SetEffectsVolume -> {v}");
        }

        public static void SetAmbiantVolume(float v)
        {
            EffectsVolume = v;
            AkSoundEngine.SetRTPCValue("Game_Volume_Ambiant", v);

            QRDebug.Log("AUDIO", FrenchPallet.SUN_FLOWER, $"SetAmbiantVolume -> {v}");
        }
    }
}
