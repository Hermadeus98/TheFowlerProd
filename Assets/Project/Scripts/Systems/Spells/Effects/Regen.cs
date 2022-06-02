using System.Collections;
using System.Collections.Generic;
using TheFowler;
using UnityEngine;

namespace TheFowler
{
    public class Regen : Effect
    {
        public int reduceCooldown = 2;
        
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            foreach (var r in receivers)
            {
                foreach (var s in r.BattleActorData.Spells)
                {
                    s.CurrentCooldown -= reduceCooldown;
                    if (s.CurrentCooldown < 0)
                        s.CurrentCooldown = 0;
                }
                
            }
            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }
    }
}
