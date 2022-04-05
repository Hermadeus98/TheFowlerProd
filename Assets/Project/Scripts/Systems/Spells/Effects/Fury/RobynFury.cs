using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class RobynFury : FuryEffect
    {
        public float damage = 5f;
        
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

            
            
            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield return base.OnFinishCast(emitter, receivers);
            yield break;
        }
    }
}
