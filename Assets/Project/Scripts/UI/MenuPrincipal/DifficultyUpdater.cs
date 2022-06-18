using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class DifficultyUpdater : MonoBehaviour, IUpdater
    {
        public TextChoice tc;
        
        private void OnEnable()
        {
            Refresh();
        }

        public void Refresh()
        {

            switch (DifficultyManager.currentDifficulty)
            {
                case DifficultyEnum.EASY:
                    tc.SetTo(1);
                    break;
                case DifficultyEnum.MEDIUM:
                    tc.SetTo(0);
                    break;
                case DifficultyEnum.HARD:
                    tc.SetTo(2);
                    break;
            }

            


            //int index = tc.current;

            //var difficultyTexts = FindObjectsOfType<DifficultyUpdater>();
            //difficultyTexts.ForEach(w => w.Apply(index));
        }

        private void Update()
        {
            Refresh();
        }

        public void Apply(int current)
        {
            tc.SetTo(current);
        }
    }

    public interface IUpdater
    {
        public void Refresh();
    }
}
