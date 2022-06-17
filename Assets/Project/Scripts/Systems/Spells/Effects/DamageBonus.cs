using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class DamageBonus : Effect
    {
        public enum AttackBonusType
        {
            BONUS,
            MALUS,
            NEUTRE
        }

        public AttackBonusType attackBonusType;
        public bool isAOE = false;
        
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            if (emitter is AllyActor)
            {
                switch (attackBonusType)
                {
                    case AttackBonusType.BONUS:
                        EnemySpellBox.Instance.Popup("Bonus de dégâts", "Damage bonus");
                        break;
                    case AttackBonusType.MALUS:
                        EnemySpellBox.Instance.Popup("Malus de dégâts", "Damage malus");
                        break;
                    case AttackBonusType.NEUTRE:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            if (TargetType == TargetTypeEnum.SELF)
            {
                yield return StateEvent(emitter, receivers, (actor, battleActor) =>
                {
                    var buff = emitter.GetBattleComponent<Buff>();
                    Apply(buff);
                });
            }
            else
            {
                yield return StateEvent(emitter, receivers, (actor, battleActor) =>
                {
                    var buff = battleActor.GetBattleComponent<Buff>();
                    Apply(buff);
                });
            }

            yield break;
        }

        private void Apply(Buff buff)
        {
            switch (attackBonusType)
            {
                case AttackBonusType.BONUS:
                    buff.BuffAttack(isAOE ? SpellData.Instance.buffAttackAOE : SpellData.Instance.buffAttack);
                    break;
                case AttackBonusType.MALUS:
                    buff.DebuffAttack(isAOE ? SpellData.Instance.debuffAttackAOE : SpellData.Instance.debuffAttack);
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