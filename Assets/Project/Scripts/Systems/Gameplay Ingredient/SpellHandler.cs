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

        public void InitializeWithData()
        {
            base.Initialize();
            spells = new List<SpellHandled>();

            for (int i = 0; i < ReferedActor.BattleActorData.Spells.Length; i++)
            {
                spells.Add(new SpellHandled()
                {
                    Spell = ReferedActor.BattleActorData.Spells[i],
                    //cooldown = ReferedActor.BattleActorData.Spells[i].CurrentCooldown,
                });
            }
        }


        public override void OnTurnStart()
        {
            base.OnTurnStart();

            if (Fury.IsInFury && !BattleManager.CurrentBattleActor == ReferedActor)
                return;

            if (spells == null)
                return;

            foreach (var s in spells)
            {
                s.cooldown--;
                
                if (s.cooldown < 0)
                {
                    s.cooldown = 0;
                    s.Spell.isRechargingCooldown = false;
                }


                s.Spell.CurrentCooldown = s.cooldown;
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
                s.CurrentCooldown = s.Cooldown - ReferedActor.BattleActorInfo.cooldownBonus;

                s.isRechargingCooldown = true;
            }
        }

        public void LoseCoolDown(int cooldownToRemove)
        {
            for (int i = 0; i < spells.Count; i++)
            {
                spells[i].cooldown -= cooldownToRemove;
                spells[i].cooldown = Mathf.Clamp(spells[i].cooldown, 0, int.MaxValue);
                spells[i].Spell.CurrentCooldown = spells[i].cooldown;

            }
        }
    }
}


