using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DefendEffect : Effect
    {
        public enum DefenseBonusType
        {
            Buff,
            Debuff,
        }

        public DefenseBonusType bonusType;
        public bool isAOE = false;

        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            foreach (var receiver in receivers)
            {
                if(emitter.GetBattleComponent<SpellHandler>() != null)
                    emitter.GetBattleComponent<SpellHandler>().LoseCoolDown(1);

                switch (bonusType)
                {
                    case DefenseBonusType.Buff:
                        receiver.GetBattleComponent<Defense>().BuffDefense(isAOE ? DamageCalculator.buffDefenseAOE : DamageCalculator.buffDefense);
                        break;
                    case DefenseBonusType.Debuff:
                        receiver.GetBattleComponent<Defense>().DebuffDefense(isAOE ? DamageCalculator.debuffDefenseAOE : DamageCalculator.debuffDefense);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override void OnSimpleCast(BattleActor emitter, BattleActor[] receivers)
        {
            base.OnSimpleCast(emitter, receivers);
            emitter.StartCoroutine(OnCast(emitter, receivers));
        }
    }
}
