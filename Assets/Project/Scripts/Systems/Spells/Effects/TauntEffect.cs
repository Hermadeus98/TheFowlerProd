using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class TauntEffect : Effect
    {
        public int turnDuration = 1;
        
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            //emitter.BattleActorAnimator.AttackCast();
            EnemySpellBox.Instance.Popup("Taunt", "Taunt");

            yield return StateEvent(emitter, receivers, delegate(BattleActor actor, BattleActor battleActor)
            {
                if (emitter == BattleManager.CurrentBattle.phoebe)
                {
                    emitter.punchline.PlayPunchline(PunchlineCallback.TAUNT);
                }

                battleActor.GetBattleComponent<Taunt>().TauntActor(turnDuration, emitter);
            }, false, false);
            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }
    }
}
