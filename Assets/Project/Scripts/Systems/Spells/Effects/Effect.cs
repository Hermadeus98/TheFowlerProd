using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    [Serializable]
    public abstract class Effect
    {
        //public string EffectName = "NO NAME EFFECT";

        public TargetTypeEnum TargetType;
        [HideInInspector]
        public BattleActor Emitter;
        [HideInInspector]
        public BattleActor[] Receivers;
        [HideInInspector]
        public float _damage, _heal;

        public Sprite sprite;

        [ReadOnly] public Spell ReferedSpell;

        public bool rage = false; //Extra damage in function of health percentage of emitter.
        public bool berserk = false; //Emitter is damaged whel he inflicts damages.
        public bool vampirisme = false; //Emitter is healed when he inflicts damages.
        
        [ShowIf("@this.berserk == true")]  public float berserkDamage = 100f;

        public virtual void PreviewEffect(BattleActor emitter)
        {
            
        }
        
        public abstract IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers);

        public abstract IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers);

        public abstract IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers);
        
        public void SetCamera()
        {
            
        }

        protected float Damage(float damage, BattleActor emitter, BattleActor[] receivers)
        {
            var dmg = 0f;
            
            if (TargetType == TargetTypeEnum.SELF)
            {
                dmg = ApplyDamage(damage, emitter, emitter, berserk);
            }
            else
            {
                foreach (var receiver in receivers)
                {
                    dmg += ApplyDamage(damage, emitter, receiver);
                }
            }

            _damage = dmg;
            return dmg;
        }

        protected float ApplyDamage(float damage, BattleActor emitter, BattleActor receiver, bool leaveOneHP = false)
        {
            var _damage = DamageCalculator.CalculateDamage(damage, emitter, receiver, ReferedSpell.SpellType, out var resistanceFaiblesseResult);

            if (rage)
            {
                var healthLosePercent = 1 - emitter.Health.NormalizedHealth;
                _damage += damage * (1 + healthLosePercent);
            }

            SoundManager.PlaySoundDamageTaken(receiver, resistanceFaiblesseResult);
                
            receiver.Health.TakeDamage(
                _damage,
                resistanceFaiblesseResult,
                leaveOneHP
            );

            return _damage;
        }

        protected void Heal(float heal, BattleActor emitter, BattleActor[] receivers)
        {
            if (emitter == BattleManager.CurrentBattle.abi)
            {
                emitter.punchline.PlayPunchline(PunchlineCallback.HEALING);
            }
            
            _heal = heal;
            receivers.ForEach(w => w.Health.Heal(_heal));
        }

        protected IEnumerator StateEvent(BattleActor emitter, BattleActor[] receivers, Action<BattleActor, BattleActor> stateEvent, bool playAnimation = true, bool forceSelf = false)
        {
            if (TargetType == TargetTypeEnum.SELF || forceSelf)
                receivers = new[] {emitter};
            
            if (emitter is AllyActor)
            {
                CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "Allies");
            }
            else if(emitter is EnemyActor)
            {
                CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "Enemies");
            }
            
            if(playAnimation)
                emitter.BattleActorAnimator.SpellExecution1();

            yield return new WaitForSeconds(SpellData.Instance.StateEffect_WaitTime);

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
                
                stateEvent.Invoke(emitter, receiver);
                
                yield return new WaitForSeconds(1f);
            }
            
            //yield return new WaitForSeconds(emitter.BattleActorAnimator.GetCurrentClipDuration() / 2f);
            yield return new WaitForSeconds(2f);
        }
    }
}
