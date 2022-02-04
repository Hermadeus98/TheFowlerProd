using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public enum GameplayPhaseEnum
    {
        NULL,
        
        //TESTS
        BATTLE_TEST_01,
        BATTLE_TEST_02,
        
        DS_TEST_01,
        DS_TEST_02,
        
        DM_TEST_01,
        
        PLAYABLE_TEST_01,
        
        //INTRO
        CINEMATIQUE_INTRO,
        
        //CHAPTER_ONE
        DS_ENTREE_EXPLOSION,
        DS_RENCONTRE_PHOEBE,
        BATTLE_GUARDES_PHOEBE,
        DS_HARMONISATION_ONE,
        DM_COULOIR_INSTRUMENT_BRISE,
        DM_FIN_COULOIR,

        //CHAPTER_TWO_P1
        DS_V_DECOUVERTE_VOLIERE,
        BATTLE_V_GUARDES_VOLIERE,
        DS_V_ENTREE_PHOEBE,
        BATTLE_V_COMBAT_PHOEBE,
        DS_V_HARMONISATION_2_1,
        DS_V_HARMONISATION_2_2,
        DM_SORTIE_VOLIERE,

        //CHAPTER_TWO_P2
        DS_B_CONFRONTATION_GUARDES,
        BATTLE_B_GUARDES_SALLE_DE_BAL,
        DM_B_PHOEBE_PASSAGE_SECRET,
        
        //CHAPTER_THREE
        DS_H_HARMONISATION_3_1,
        DS_H_HARMONISATION_3_2,
        DM_H_HUB_TRIBUNAL,
        DS_RENCONTRE_LIEUTEMENT,
        BATTLE_GUARDES_TRIBUNAL,
        DS_COMBAT_LIEUTENANT,
        BATTLE_BOSS_LIEUTENANT,
        DS_FIN_BATTLE_BOSS_LIEUTENANT,
        
        //OUTRO
        CINEMATIQUE_OUTRO,
    }
}
