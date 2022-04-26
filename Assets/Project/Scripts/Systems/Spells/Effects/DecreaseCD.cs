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
            if (emitter.GetBattleComponent<SpellHandler>() != null)
                emitter.GetBattleComponent<SpellHandler>().LoseCoolDown(1);
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

