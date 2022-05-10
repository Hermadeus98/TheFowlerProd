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

        public float delay = 0f;
        public AttackBonusType attackBonusType;
        public bool isAOE = false;
        
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield return new WaitForSeconds(SpellData.Instance.StateEffect_WaitTime);
            
            if (TargetType == TargetTypeEnum.SELF)
            {
                if (emitter is AllyActor)
                {
                    CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "Allies");
                }
                else if(emitter is EnemyActor)
                {
                    CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "Enemies");
                }
                
                yield return new WaitForSeconds(SpellData.Instance.StateEffect_WaitTime);
                
                var buff = emitter.GetBattleComponent<Buff>();
                Apply(buff);
            }
            else
            {
                foreach (var receiver in receivers)
                {
                    if (receiver is AllyActor)
                    {
                        CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "Allies");
                    }
                    else if(receiver is EnemyActor)
                    {
                        CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "Enemies");
                    }
                
                    yield return new WaitForSeconds(SpellData.Instance.StateEffect_WaitTime);
                    
                    var buff = receiver.GetBattleComponent<Buff>();
                    Apply(buff);
                }
            }
            
            yield return new WaitForSeconds(1f);

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