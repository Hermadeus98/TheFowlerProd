using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class HealEffect : Effect
    {
        [SerializeField] private float healValue = 25;
        
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            if(emitter is AllyActor)
                EnemySpellBox.Instance.Popup("Soin", "Heal");

            if (emitter == BattleManager.CurrentBattle.abi)
            {
                var s = emitter.SequenceHandler.GetSequence(SequenceEnum.HEAL);
                s.Play();
            }
            
            if (TargetType == TargetTypeEnum.SELF)
            {
                yield return StateEvent(emitter, receivers,
                    delegate(BattleActor emitter, BattleActor receiver)
                    {
                        Heal(healValue, emitter, new[] {emitter});
                    });
            }
            else
            {
                yield return StateEvent(emitter, receivers,
                    delegate(BattleActor emitter, BattleActor receiver)
                    {
                        Heal(healValue, emitter, new[] {receiver});
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
