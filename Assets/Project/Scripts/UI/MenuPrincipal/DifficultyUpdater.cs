using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DifficultyUpdater : MonoBehaviour
    {
        public TextChoice tc;
        
        private void Start()
        {
            Refresh();
        }

        private void Refresh()
        {
            switch (DifficultyManager.currentDifficulty )
            {
                case DifficultyEnum.TEST:
                    break;
                case DifficultyEnum.EASY:
                    break;
                case DifficultyEnum.MEDIUM:
                    break;
                case DifficultyEnum.HARD:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
