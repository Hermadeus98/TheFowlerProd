using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public static class Comparator
    {
        //---<HEALTH>--------------------------------------------------------------------------------------------------<
        //--<PLUS>--
        public static bool HaveMoreHealth(BattleActor[] a, BattleActor[] b) => GetSumOfHealth(a) > GetSumOfHealth(b);

        public static bool HaveMoreHealth(BattleActor[] a, float value) => GetSumOfHealth(a) > GetSumOfMaxHealth(a) * value / 100;

        //--<PLUSEQUAL>--
        public static bool HaveMoreEqualHealth(BattleActor[] a, BattleActor[] b) =>  GetSumOfHealth(a) >= GetSumOfHealth(b);
        public static bool HaveMoreEqualHealth(BattleActor[] a, float value) => GetSumOfHealth(a) >= GetSumOfMaxHealth(a) * value / 100;
        
        //--<LESS>--
        public static bool HaveLessHealth(BattleActor[] a, BattleActor[] b) => GetSumOfHealth(a) < GetSumOfHealth(b);
        public static bool HaveLessHealth(BattleActor[] a, float value) => GetSumOfHealth(a) < GetSumOfMaxHealth(a) * value / 100;
        
        //--<LESS EQUAL>--
        public static bool HaveLessEqualHealth(BattleActor[] a, BattleActor[] b) => GetSumOfHealth(a) <= GetSumOfHealth(b);
        
        public static bool HaveLessEqualHealth(BattleActor[] a, float value) => GetSumOfHealth(a) <= GetSumOfMaxHealth(a) * value / 100;
        
        //--<EQUALITY>--
        public static bool HaveEqualHealth(BattleActor[] a, BattleActor[] b) => GetSumOfHealth(a) == GetSumOfHealth(b);
        
        public static bool HaveEqualHealth(BattleActor[] a, float value) => GetSumOfHealth(a) == GetSumOfMaxHealth(a) * value / 100;

        public static bool AtLeastOneHaveMinusHealthThan(BattleActor[] actors, float value)
        {
            for (int i = 0; i < actors.Length; i++)
            {
                if (HaveMoreHealth(new BattleActor[] {actors[i]}, value))
                {
                    return true;
                }
            }

            return false;
        }
        
        //---<HELPERS>-------------------------------------------------------------------------------------------------<
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

        private static float GetSumOfMaxHealth(BattleActor[] actors)
        {
            if (actors.IsNullOrEmpty())
                return 0f;

            var h = 0f;
            for (int i = 0; i < actors.Length; i++)
            {
                h += actors[i].Health.MaxHealth;
            }

            return h;
        }
    }
}
