using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class DamageBonus : Effect
    {
        [SerializeField, Range(0,100)] private float buffBonus = 25f;
        [SerializeField] private int turnDuration = 1;
        
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            if (TargetType == TargetTypeEnum.SELF)
            {
                var buff = emitter.GetBattleComponent<Buff>();
                buff.BuffPercent = buffBonus;
                buff.BuffActor(turnDuration);
            }
            else
            {
                foreach (var receiver in receivers)
                {
                    var buff = receiver.GetBattleComponent<Buff>();
                    buff.BuffPercent = buffBonus;
                    buff.BuffActor(turnDuration);
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
            if (TargetType == TargetTypeEnum.SELF)
            {
                var buff = emitter.GetBattleComponent<Buff>();
                buff.BuffPercent = buffBonus;
                buff.BuffActor(turnDuration);
            }
            else
            {
                foreach (var receiver in receivers)
                {
                    var buff = receiver.GetBattleComponent<Buff>();
                    buff.BuffPercent = buffBonus;
                    buff.BuffActor(turnDuration);
                }
            }
        }
    }
}