using System.Collections;
using System.Collections.Generic;
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

        public List<SpellHandled> spells;

        public override void Initialize()
        {
            base.Initialize();
            spells = new List<SpellHandled>();

            if (BattleManager.CurrentBattle.enableProgression)
            {
                for (int i = 0; i < ReferedActor.BattleActorData.Spells.Length; i++)
                {
                    spells.Add(new SpellHandled()
                    {
                        Spell = ReferedActor.BattleActorData.Spells[i],
                        cooldown = 0,
                    });
                }
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
    }
}


