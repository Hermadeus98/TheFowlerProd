using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using QRCode.Extensions;
using UnityEngine;

namespace TheFowler
{
    public static class DifficultyManager
    {
        public static Action<DifficultyEnum> OnDifficultyChange;
        
        public static DifficultyEnum currentDifficulty { get; private set; }

        public static void ChangeDifficulty(DifficultyEnum difficulty)
        {
            if (difficulty != currentDifficulty)
            {
                QRDebug.Log("DIFFICULTY", FrenchPallet.ALIZARIN, $"Set on {difficulty}");
                
                currentDifficulty = difficulty;
                OnDifficultyChange?.Invoke(currentDifficulty);
            }
        }
    }
}