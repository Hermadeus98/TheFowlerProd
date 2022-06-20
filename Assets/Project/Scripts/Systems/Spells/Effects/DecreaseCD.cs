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
                    var ps = GameObject.Instantiate(SpellData.Instance.cooldownPS, receiver.transform.position,
                        Quaternion.identity);
                    ps.Play();
                    emitter.GetBattleComponent<CooldownComponent>().BuffCD(1);
                });
            }
            else
            {
                yield return StateEvent(emitter, receivers, (actor, BattleActor) =>
                {
                    var ps = GameObject.Instantiate(SpellData.Instance.cooldownPS, BattleActor.transform.position,
                        Quaternion.identity);
                    ps.Play();
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

