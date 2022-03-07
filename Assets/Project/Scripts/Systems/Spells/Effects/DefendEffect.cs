using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DefendEffect : Effect
    {
        [SerializeField] private int effectDuration = 1;
        [SerializeField, Range(0, 100)] private float effect = 25;
        [SerializeField] private int manaToRestaure = 2;
        
        public enum RestaureManaAt
        {
            Cast,
            Start_Turn
        }

        [SerializeField] private RestaureManaAt restaureManaAt = RestaureManaAt.Cast;
        
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            
            switch (restaureManaAt)
            {
                case RestaureManaAt.Cast:
                    emitter.Mana.AddMana(manaToRestaure);
                    break;
                case RestaureManaAt.Start_Turn:
                    emitter.GetBattleComponent<Defense>().RestaureMana.AddListener(() => emitter.Mana.AddMana(manaToRestaure));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            emitter.GetBattleComponent<Defense>().DefendActor(effectDuration, effect);
            emitter.BattleActorAnimator.Parry();
            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }
    }
}
