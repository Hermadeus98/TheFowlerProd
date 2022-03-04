using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DamageEffect : Effect
    {
        public float damage;

        public override void PreviewEffect(BattleActor emitter)
        {
            base.PreviewEffect(emitter);
            
            
        }

        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            emitter.BattleActorAnimator.AttackCast();
            yield return new WaitForSeconds(emitter.BattleActorAnimator.AttackCastDuration());

            foreach (var receiver in receivers)
            {
                var _damage = DamageCalculator.CalculateDamage(damage, emitter, receiver, ReferedSpell.SpellType);

                receiver.Health.TakeDamage(
                    _damage
                    );
            }
            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }
    }
}
