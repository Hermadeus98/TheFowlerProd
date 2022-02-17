using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class StunEffect : Effect
    {
        [SerializeField] private int stunTurnDuration;
        
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            foreach (var receiver in receivers)
            {
                receiver.GetBattleComponent<Stun>().StunActor(stunTurnDuration);
            }
            
            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }
    }

}