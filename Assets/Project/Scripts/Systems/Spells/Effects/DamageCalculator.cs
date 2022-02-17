using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public static class DamageCalculator
    {
        public static float CalculateDamage(float initialDamage, BattleActor emitter, BattleActor receiver)
        {
            var bonusesCoef = emitter.BattleActorInfo.buffBonus / 100;
            var malusesCoef = emitter.BattleActorInfo.debuffMalus / 100;
            var bonusesCalculate = initialDamage * (bonusesCoef - malusesCoef);

            return initialDamage + bonusesCalculate;
        }
    }
}
