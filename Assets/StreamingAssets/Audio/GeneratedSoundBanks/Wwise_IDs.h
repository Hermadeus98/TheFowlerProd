/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID TEST_TICK = 564845035U;
        static const AkUniqueID TF_COREINIT = 3460945957U;
        static const AkUniqueID TF_MAIN_SETMUTEOFF = 2594094354U;
        static const AkUniqueID TF_MAIN_SETMUTEON = 1059030324U;
        static const AkUniqueID TF_MAIN_SETTUTOOFF = 4166158089U;
        static const AkUniqueID TF_MAIN_SETTUTOON = 3365239869U;
        static const AkUniqueID TF_MUSIC_SETBATTLE = 1242661300U;
        static const AkUniqueID TF_MUSIC_SETEXPLO = 117344976U;
        static const AkUniqueID TF_SFX_COMBAT_BASEATTACK_ABIGAIL_CAST = 1391113456U;
        static const AkUniqueID TF_SFX_COMBAT_BASEATTACK_ABIGAIL_HIT = 3899759664U;
        static const AkUniqueID TF_SFX_COMBAT_BASEATTACK_ROBYN_CAST = 3612941195U;
        static const AkUniqueID TF_SFX_COMBAT_BASEATTACK_ROBYN_HIT = 4175269601U;
        static const AkUniqueID TF_SFX_COMBAT_GENERIC_ALLY_DAMAGETAKEN = 3100206904U;
        static const AkUniqueID TF_SFX_COMBAT_GENERIC_ENEMY_DAMAGETAKEN_NEUTRAL = 3091443500U;
        static const AkUniqueID TF_SFX_COMBAT_GENERIC_ENEMY_DAMAGETAKEN_RESIST = 2730268465U;
        static const AkUniqueID TF_SFX_COMBAT_GENERIC_ENEMY_DAMAGETAKEN_WEAK = 3474944871U;
        static const AkUniqueID TF_SFX_COMBAT_GENERIC_SKILL_BUFF = 777422854U;
        static const AkUniqueID TF_SFX_COMBAT_GENERIC_SKILL_DAMAGE = 3306024426U;
        static const AkUniqueID TF_SFX_COMBAT_GENERIC_SKILL_DEBUFF = 2661308679U;
        static const AkUniqueID TF_SFX_COMBAT_GENERIC_SKILL_HEAL = 2608294815U;
        static const AkUniqueID TF_SFX_COMBAT_GENERIC_SKILL_STUN = 2654934017U;
        static const AkUniqueID TF_SFX_COMBAT_GENERIC_SKILL_TAUNT = 2034208103U;
        static const AkUniqueID TF_SFX_COMBAT_UI_ACTIONWHEEL_OPEN = 936410632U;
        static const AkUniqueID TF_SFX_COMBAT_UI_CANCEL = 1612741184U;
        static const AkUniqueID TF_SFX_COMBAT_UI_CONFIRM = 1091842654U;
        static const AkUniqueID TF_SFX_COMBAT_UI_HOVER = 2681612572U;
        static const AkUniqueID TF_SFX_COMBAT_UI_SKILLSELECTION = 2399625037U;
        static const AkUniqueID TF_SFX_COMBAT_UI_SPELLSELECTIONPANEL_OPEN = 1260578859U;
        static const AkUniqueID TF_SFX_COMBAT_UI_SWITCHTARGET = 3123946201U;
        static const AkUniqueID TF_SFX_COMBAT_UI_TURN_ALLYTURN = 595504765U;
        static const AkUniqueID TF_SFX_COMBAT_UI_TURN_ENEMYTURN = 1139569567U;
        static const AkUniqueID TF_SFX_COMBAT_UI_WEAKDISPLAY = 3449597560U;
        static const AkUniqueID TF_SFX_EXPLO_GENERIC_FOOTSTEP = 2232847779U;
        static const AkUniqueID TF_VOICE_TEST_NARRA01 = 3755670027U;
        static const AkUniqueID TF_VOICE_TEST_NARRA02 = 3755670024U;
        static const AkUniqueID TF_VOICE_TEST_NARRA03 = 3755670025U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GAMEPLAYPHASE
        {
            static const AkUniqueID GROUP = 990169578U;

            namespace STATE
            {
                static const AkUniqueID BATTLE = 2937832959U;
                static const AkUniqueID EXPLO = 3814499265U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace GAMEPLAYPHASE

        namespace MUTE
        {
            static const AkUniqueID GROUP = 2974103762U;

            namespace STATE
            {
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID OFF = 930712164U;
                static const AkUniqueID ON = 1651971902U;
            } // namespace STATE
        } // namespace MUTE

        namespace TUTORIAL
        {
            static const AkUniqueID GROUP = 3762955427U;

            namespace STATE
            {
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID OFF = 930712164U;
                static const AkUniqueID ON = 1651971902U;
            } // namespace STATE
        } // namespace TUTORIAL

    } // namespace STATES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID GAME_VOLUME_AMBIANT = 1021071045U;
        static const AkUniqueID GAME_VOLUME_MUSIC = 1039087204U;
        static const AkUniqueID GAME_VOLUME_SFX = 1011990060U;
        static const AkUniqueID GAME_VOLUME_VOICES = 2065406626U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID CORE = 3787826988U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMBIANT = 78669895U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID SFX = 393239870U;
        static const AkUniqueID VOICES = 3313685232U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
