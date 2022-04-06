using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Utilities;

namespace TheFowler
{
    public class ResurrectEffect : Effect
    {
        [SerializeField] private float resurrectPorcentage = 25;
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            receivers.ForEach(w => w.Health.Resurect(resurrectPorcentage));
            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override void OnSimpleCast(BattleActor emitter, BattleActor[] receivers)
        {
            base.OnSimpleCast(emitter, receivers);
            receivers.ForEach(w => w.Health.Resurect(resurrectPorcentage));
        }

    }
}

