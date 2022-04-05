using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class ResurrectEffect : Effect
    {
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            //receivers.ForEach(w => w.Health.Resurrect());
            throw new System.NotImplementedException();
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            throw new System.NotImplementedException();
        }

    }
}

