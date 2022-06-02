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
            
            if (TargetType == TargetTypeEnum.SELF)
            {
                yield return StateEvent(emitter, receivers, (actor, BattleActor) =>
                {
                    var def = emitter.GetBattleComponent<Defense>();
                    Apply(def);
                });
            }
            else
            {
                yield return StateEvent(emitter, receivers, (actor, BattleActor) =>
                {
                    var def = BattleActor.GetBattleComponent<Defense>();
                    Apply(def);
                });
            }

            yield break;
        }

        private void Apply(Defense def)
        {
            switch (bonusType)
            {
                case DefenseBonusType.Buff:
                    def.BuffDefense(isAOE ? SpellData.Instance.buffDefenseAOE : SpellData.Instance.buffDefense);
                    break;
                case DefenseBonusType.Debuff:
                    def.DebuffDefense(isAOE ? SpellData.Instance.debuffDefenseAOE : SpellData.Instance.debuffDefense);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }
    }
}
