using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheFowler
{
    public class CastNode : CompositeNode
    {
        [Serializable]
        public class SpellPercentList
        {
            public Spell Spell;
            [Range(0,100)] public int Chance;
        }

        [TitleGroup("Spells")]
        [SerializeField] private SpellPercentList[] spellList;

        public Spell GetRandomSpell()
        {
            if (spellList.IsNullOrEmpty())
                return null;

            if (spellList.Length == 1)
                return spellList[0].Spell;

            var list = new List<Spell>();
            for (int i = 0; i < spellList.Length; i++)
            {
                for (int y = 0; y < spellList[i].Chance; y++)
                {
                    list.Add(spellList[i].Spell);
                }
            }

            return list[Random.Range(0, list.Count)];
        }
        
        protected override void OnStart()
        {
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }

        protected override void OnStop()
        {
        }
    }
}
