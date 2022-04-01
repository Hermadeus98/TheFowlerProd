using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class SpellHandler : BattleActorComponent
    {
        public class SpellHandled
        {
            public Spell Spell;
            public int cooldown;
        }

        [ReadOnly] public List<SpellHandled> spells;

        public override void Initialize()
        {
            base.Initialize();
            spells = new List<SpellHandled>();

            for (int i = 0; i < ReferedActor.BattleActorData.Spells.Length; i++)
            {
                spells.Add(new SpellHandled()
                {
                    Spell = ReferedActor.BattleActorData.Spells[i],
                    cooldown = 0,
                });
            }
        }

        public override void OnTurnStart()
        {
            base.OnTurnStart();

            foreach (var s in spells)
            {
                s.cooldown--;
                if (s.cooldown < 0)
                    s.cooldown = 0;
            }
        }

        public SpellHandled GetSpellHandled(Spell s)
        {
            SpellHandled spellHandled = null;

            for (int i = 0; i < spells.Count; i++)
            {
                if (spells[i].Spell == s)
                    return spells[i];
            }

            return null;
        }

        public void ApplyCooldown(Spell s)
        {
            if (GetSpellHandled(s) != null)
            {
                GetSpellHandled(s).cooldown = s.Cooldown;
            }
        }
    }
}


