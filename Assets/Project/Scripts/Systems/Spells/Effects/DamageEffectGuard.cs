using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DamageEffectGuard : DamageEffect
    {
        public ParticleSystem attackParticle;
        public float attackDuration = 2f;
        public ParticleSystem impactParticle;
        public float impactDuration = 1f;
        
        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            foreach (var r in receivers)
            {
                if (attackParticle != null)
                {
                    var attack = GameObject.Instantiate(attackParticle);
                }

                yield return new WaitForSeconds(attackDuration);

                if (impactParticle != null)
                {
                    var impact = GameObject.Instantiate(impactParticle, r.transform);
                }
            }
            
            Damage(damage, emitter, receivers);
            
            yield return new WaitForSeconds(impactDuration);
        }

        public override void OnSimpleCast(BattleActor emitter, BattleActor[] receivers)
        {
            emitter.StartCoroutine(OnCast(emitter, receivers));
        }
    }
}
