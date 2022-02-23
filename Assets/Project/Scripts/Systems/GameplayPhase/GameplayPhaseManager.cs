using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public static class GameplayPhaseManager
    {
        private static Dictionary<GameplayPhaseEnum, GameplayPhase> GameplayPhases = new Dictionary<GameplayPhaseEnum, GameplayPhase>();

        public static void RegisterGameplayPhase(GameplayPhase dialoguePhase)
        {
            if (dialoguePhase.GameplayPhaseID == GameplayPhaseEnum.NULL)
                return;

            if (!GameplayPhases.ContainsKey(dialoguePhase.GameplayPhaseID))
            {
                GameplayPhases.Add(dialoguePhase.GameplayPhaseID, dialoguePhase);
            }
        }
        
        public static void UnregisterGameplayPhase(GameplayPhase dialoguePhase)
        {
            if (GameplayPhases.ContainsKey(dialoguePhase.GameplayPhaseID))
            {
                GameplayPhases.Remove(dialoguePhase.GameplayPhaseID);
            }
        }

        public static void PlayGameplayPhase(GameplayPhaseEnum key)
        {
            if(key == GameplayPhaseEnum.NULL)
                return;
            
            if (!GameplayPhases.ContainsKey(key))
            {
                Debug.LogError($"GameplayPhases don't contain {key}");
                return;
            }
            GameplayPhases[key].PlayPhase();
        }
    }
}
