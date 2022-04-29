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
            Debug.Log(receivers);

            if (TargetType == TargetTypeEnum.SELF)
            {
                emitter.GetBattleComponent<CooldownComponent>().BuffCD(1);
            }
            else
            {
                foreach (var receiver in receivers)
                {
                    receiver.GetBattleComponent<CooldownComponent>().BuffCD(1);
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

