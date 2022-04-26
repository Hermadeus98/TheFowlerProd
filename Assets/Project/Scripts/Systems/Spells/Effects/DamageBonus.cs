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
        }

        public AttackBonusType attackBonusType;
        public bool isAOE = false;
        
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            if (TargetType == TargetTypeEnum.SELF)
            {
                var buff = emitter.GetBattleComponent<Buff>();
                Apply(buff);
            }
            else
            {
                foreach (var receiver in receivers)
                {
                    var buff = receiver.GetBattleComponent<Buff>();
                    Apply(buff);
                }
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

        public override void OnSimpleCast(BattleActor emitter, BattleActor[] receivers)
        {
            emitter.StartCoroutine(OnCast(emitter, receivers));
        }
    }
}