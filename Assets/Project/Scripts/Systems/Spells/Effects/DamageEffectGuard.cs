using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DamageEffectGuard : DamageEffect
    {
        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            foreach (var r in receivers)
            {
                var attack = GameObject.Instantiate(SpellData.Instance.Guard_PS_BasicAttack_Projectile);

                yield return new WaitForSeconds(SpellData.Instance.Guard_Timer_BasicAttack_ProjectileDuration);

                var impact = GameObject.Instantiate(SpellData.Instance.Guard_PS_BasicAttack_Impact, r.transform);
                
                Damage(damage, emitter, new []{r});
            }

            yield return new WaitForSeconds(SpellData.Instance.Guard_Timer_BasicAttack_ImpactDuration);
        }
    }
}
