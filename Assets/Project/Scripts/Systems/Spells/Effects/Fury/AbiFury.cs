using System.Collections;
using System.Collections.Generic;
using TheFowler;
using UnityEngine;

namespace TheFowler
{
    public class AbiFury : FuryEffect
    {
        public float damage = 5;
        public float heal = 5;
        
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield return base.OnBeginCast(emitter, receivers);
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield return base.OnCast(emitter, receivers);

            Damage(damage, emitter, receivers);

            yield return new WaitForSeconds(1f);
            
            Heal(heal, emitter, TargetSelector.GetAllAllies());
            
            yield return new WaitForSeconds(1f);

            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield return base.OnFinishCast(emitter, receivers);
            yield break;
        }
    }
}
