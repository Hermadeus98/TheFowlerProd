using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DecreaseCD : Effect
    {
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {

            
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            EnemySpellBox.Instance.Popup("Bonus de recharge", "Cooldown bonus");
            
            if (TargetType == TargetTypeEnum.SELF)
            {
                yield return StateEvent(emitter, receivers, delegate(BattleActor emitter, BattleActor receiver)
                {
                    emitter.GetBattleComponent<CooldownComponent>().BuffCD(1);
                });
            }
            else
            {
                yield return StateEvent(emitter, receivers, (actor, BattleActor) =>
                {
                    BattleActor.GetBattleComponent<CooldownComponent>().BuffCD(1);
                });
            }

            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }
    }
}

