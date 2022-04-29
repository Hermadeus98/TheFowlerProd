using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class VampirismeEffect : Effect
    {
        public float damage;
        
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            var dmg = Damage(damage, emitter, receivers);
            Heal(dmg, emitter, new []{emitter});

            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }
    }
}
