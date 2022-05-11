using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DefendEffect : Effect
    {
        public enum DefenseBonusType
        {
            Buff,
            Debuff,
        }

        public DefenseBonusType bonusType;
        public bool isAOE = false;

        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            if (emitter == BattleManager.CurrentBattle.phoebe)
            {
                emitter.punchline.PlayPunchline(PunchlineCallback.PROTECT);
            }
            
            foreach (var receiver in receivers)
            {
                if(emitter.GetBattleComponent<SpellHandler>() != null)
                    emitter.GetBattleComponent<SpellHandler>().LoseCoolDown(1);

                switch (bonusType)
                {
                    case DefenseBonusType.Buff:
                        receiver.GetBattleComponent<Defense>().BuffDefense(isAOE ? SpellData.Instance.buffDefenseAOE : SpellData.Instance.buffDefense);
                        break;
                    case DefenseBonusType.Debuff:
                        receiver.GetBattleComponent<Defense>().DebuffDefense(isAOE ? SpellData.Instance.debuffDefenseAOE : SpellData.Instance.debuffDefense);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
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
