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
            foreach (var receiver in receivers)
            {
                switch (restaureManaAt)
                {
                    case RestaureManaAt.Cast:
                        //receiver.Mana.AddMana(manaToRestaure);
                        if(emitter.GetBattleComponent<SpellHandler>() != null)
                            emitter.GetBattleComponent<SpellHandler>().LoseCoolDown(1);
                        break;
                    case RestaureManaAt.Start_Turn:
                        //receiver.GetBattleComponent<Defense>().RestaureMana.AddListener(() => receiver.Mana.AddMana(manaToRestaure));
                        //emitter.GetBattleComponent<SpellHandler>().re
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                receiver.GetBattleComponent<Defense>().DefendActor(effectDuration, effect);
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
            emitter.StartCoroutine(OnCast(emitter, receivers));
        }
    }
}
