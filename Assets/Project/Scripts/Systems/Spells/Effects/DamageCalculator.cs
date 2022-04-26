using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public static class DamageCalculator
    {
        public static float CalculateDamage(float initialDamage, BattleActor emitter, BattleActor receiver, Spell.SpellTypeEnum spellType, out ResistanceFaiblesseResult ResistanceFaiblesseResult)
        {
            var attackBonus = emitter.BattleActorInfo.attackBonus;
            var defenseBonus = receiver.BattleActorInfo.defenseBonus;

            var bonus = (attackBonus - defenseBonus) / 100;

            var result = initialDamage + initialDamage * bonus;
            
            var resistance = CalculateSpellTypeBonus(spellType, receiver.BattleActorData.actorType);
            switch (resistance)
            {
                case ResistanceFaiblesseResult.NEUTRE:
                    break;
                case ResistanceFaiblesseResult.FAIBLESSE:
                    result *= 1.3f;

                    break;
                case ResistanceFaiblesseResult.RESISTANCE:
                    result *= 0.7f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ResistanceFaiblesseResult = resistance;

            result = Mathf.Ceil(result);
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
