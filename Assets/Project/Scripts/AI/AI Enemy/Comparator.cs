using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public static class Comparator
    {
        public static bool HaveMoreHealth(BattleActor[] a, BattleActor[] b)
        {
            return GetSumOfHealth(a) > GetSumOfHealth(b);
        }
        
        public static bool HaveLessHealth(BattleActor[] a, BattleActor[] b)
        {
            return GetSumOfHealth(a) < GetSumOfHealth(b);
        }

        private static float GetSumOfHealth(BattleActor[] actors)
        {
            if (actors.IsNullOrEmpty())
                return 0f;
            
            var h = 0f;
            for (int i = 0; i < actors.Length; i++)
            {
                h += actors[i].Health.CurrentHealth;
            }

            return h;
        }
    }
}
