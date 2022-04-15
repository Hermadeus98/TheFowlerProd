using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class PhoebeFury : FuryEffect
    {
        public float damage = 5f;
        public float buffBonus = 25f;
        public int turnDuration = 2;
        
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield return base.OnBeginCast(emitter, receivers);
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield return base.OnCast(emitter, receivers);

            var allies = TargetSelector.GetAllAllies();
            foreach (var ally in allies)
            {
                var buff = ally.GetBattleComponent<Buff>();
                /*buff.BuffPercent = buffBonus;
                buff.BuffActor(turnDuration);*/
            }

            yield return new WaitForSeconds(1f);

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
