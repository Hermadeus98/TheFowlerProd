using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public static class DamageCalculator
    {
        public static float CalculateDamage(float initialDamage, BattleActor emitter, BattleActor receiver, Spell.SpellTypeEnum spellType)
        {
            var bonusesCoef = emitter.BattleActorInfo.buffBonus / 100;
            var malusesCoef = emitter.BattleActorInfo.debuffMalus / 100;
            var defenseCoef = receiver.BattleActorInfo.defenseBonus == 0 ? 1f : receiver.BattleActorInfo.defenseBonus / 100;
          
            var bonusesCalculate = initialDamage * (bonusesCoef - malusesCoef);
            
            var result = (initialDamage + bonusesCalculate) / defenseCoef;
            
            var resistance = CalculateSpellTypeBonus(spellType, receiver.BattleActorData.actorType);
            switch (resistance)
            {
                case ResistanceFaiblesseResult.NEUTRE:
                    break;
                case ResistanceFaiblesseResult.FAIBLESSE:
                    result *= 1.5f;
                    break;
                case ResistanceFaiblesseResult.RESISTANCE:
                    result *= 0.5f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        public enum ResistanceFaiblesseResult
        {
            NEUTRE,
            FAIBLESSE,
            RESISTANCE,
        }
        public static ResistanceFaiblesseResult CalculateSpellTypeBonus(Spell.SpellTypeEnum spellTypeSpell, Spell.SpellTypeEnum spellTypeReceiver)
        {
            switch (spellTypeSpell)
            {
                case Spell.SpellTypeEnum.NULL:
                    return ResistanceFaiblesseResult.NEUTRE;
                case Spell.SpellTypeEnum.CLAW:
                    return spellTypeReceiver switch
                    {
                        Spell.SpellTypeEnum.NULL => ResistanceFaiblesseResult.NEUTRE,
                        Spell.SpellTypeEnum.CLAW => ResistanceFaiblesseResult.NEUTRE,
                        Spell.SpellTypeEnum.BEAK => ResistanceFaiblesseResult.FAIBLESSE,
                        Spell.SpellTypeEnum.FEATHER => ResistanceFaiblesseResult.RESISTANCE,
                        _ => throw new ArgumentOutOfRangeException(nameof(spellTypeReceiver), spellTypeReceiver, null)
                    };
                case Spell.SpellTypeEnum.BEAK:
                    return spellTypeReceiver switch
                    {
                        Spell.SpellTypeEnum.NULL => ResistanceFaiblesseResult.NEUTRE,
                        Spell.SpellTypeEnum.CLAW => ResistanceFaiblesseResult.RESISTANCE,
                        Spell.SpellTypeEnum.BEAK => ResistanceFaiblesseResult.NEUTRE,
                        Spell.SpellTypeEnum.FEATHER => ResistanceFaiblesseResult.FAIBLESSE,
                        _ => throw new ArgumentOutOfRangeException(nameof(spellTypeReceiver), spellTypeReceiver, null)
                    };
                case Spell.SpellTypeEnum.FEATHER:
                    return spellTypeReceiver switch
                    {
                        Spell.SpellTypeEnum.NULL => ResistanceFaiblesseResult.NEUTRE,
                        Spell.SpellTypeEnum.CLAW => ResistanceFaiblesseResult.FAIBLESSE,
                        Spell.SpellTypeEnum.BEAK => ResistanceFaiblesseResult.RESISTANCE,
                        Spell.SpellTypeEnum.FEATHER => ResistanceFaiblesseResult.NEUTRE,
                        _ => throw new ArgumentOutOfRangeException(nameof(spellTypeReceiver), spellTypeReceiver, null)
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(spellTypeSpell), spellTypeSpell, null);
            }

            return ResistanceFaiblesseResult.NEUTRE;
        }
    }
}
