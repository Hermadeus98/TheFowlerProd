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
        
        private void Start()
        {
            Refresh();
        }

        public void Refresh()
        {
            int index = tc.current;

            var difficultyTexts = FindObjectsOfType<DifficultyUpdater>();
            difficultyTexts.ForEach(w => w.Apply(index));
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
