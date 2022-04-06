using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class FuryEffect : Effect
    {
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            Fury.ResetFury();
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            
            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }
    }
}
