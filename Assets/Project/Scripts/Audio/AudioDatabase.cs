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
        //Debug
        NULL,

        //Main
        TF_CoreInit,


        //Content
        TF_SFX_Explo_Generic_Footstep,

        //Combat
        TF_SFX_Combat_Generic_Ally_DamageTaken,
        TF_SFX_Combat_Generic_Enemy_DamageTaken_Resist,
        TF_SFX_Combat_Generic_Enemy_DamageTaken_Neutral,
        TF_SFX_Combat_Generic_Enemy_DamageTaken_Weak,
        TF_SFX_Combat_Generic_Skill_Buff,
        TF_SFX_Combat_Generic_Skill_Damage,
        TF_SFX_Combat_Generic_Skill_Debuff,
        TF_SFX_Combat_Generic_Skill_Heal,
        TF_SFX_Combat_Generic_Skill_Stun,
        TF_SFX_Combat_Generic_Skill_Taunt,

        //UI
        TF_SFX_Combat_UI_ActionWheel_Open,
        TF_SFX_Combat_UI_Cancel,
        TF_SFX_Combat_UI_Confirm,
        TF_SFX_Combat_UI_Hover,
        TF_SFX_Combat_UI_SkillSelection,
        TF_SFX_Combat_UI_SpellSelectionPanel_Open,
        TF_SFX_Combat_UI_SwitchTarget,
        TF_SFX_Combat_UI_Turn_AllyTurn,
        TF_SFX_Combat_UI_Turn_EnemyTurn,
        TF_SFX_Combat_UI_WeakDisplay,

        //Utilities
        TF_Main_SetMuteOff,
        TF_Main_SetMuteOn,
        TF_Main_SetBattle,
        TF_Main_SetExplo,

        //Voices
            //Robyn

            //Abigail

            //Phoebe

            //Guard

            //Lieutenant

            //Narrator
        TF_Voice_Test_Narra01,
        TF_Voice_Test_Narra02,
        TF_Voice_Test_Narra03,

    }
}
