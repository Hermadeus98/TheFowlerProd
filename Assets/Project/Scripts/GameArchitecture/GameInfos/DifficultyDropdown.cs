using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class DifficultyDropdown : SerializedMonoBehaviour
    {
        public void ChangeDifficulty(int value)
        {
            if (value == 0)
            {
                DifficultyManager.ChangeDifficulty(DifficultyEnum.TEST);
            }
            
            if (value == 1)
            {
                DifficultyManager.ChangeDifficulty(DifficultyEnum.EASY);
            }
            
            if (value == 2)
            {
                DifficultyManager.ChangeDifficulty(DifficultyEnum.MEDIUM);
            }
            
            if (value == 3)
            {
                DifficultyManager.ChangeDifficulty(DifficultyEnum.HARD);
            }
        }
    }
}
