using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class BattleActorDataInitializer : DataInitializer<BattleActorStats>
    {
        public BattleActorDataInitializer(string json) : base(json)
        {
        }
    }

    [Serializable]
    public struct BattleActorStats
    {
        public float health;
        public int mana;
    }
}
