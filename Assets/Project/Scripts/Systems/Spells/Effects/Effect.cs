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

        /*[SerializeField] protected BattleCameraBatch battleCameraBatch = BattleCameraBatch.NULL;
        [SerializeField, ShowIf("@this.battleCameraBatch == BattleCameraBatch.NULL")] protected cameraPath cameraPath;
        [SerializeField, ShowIf("@this.battleCameraBatch == BattleCameraBatch.CURRENT_ACTOR_PERSONALISE")] private string cameraSpecificPath = "Default";
        [SerializeField, ShowIf("@this.battleCameraBatch == BattleCameraBatch.CURRENT_BATTLE")] private string cameraBattlePath = "Default";*/

        [ReadOnly] public Spell ReferedSpell;

        public bool rage = false;
        public bool berserk = false;
        [ShowIf("@this.berserk == true")]  public float berserkDamage = 100f;

        public virtual void PreviewEffect(BattleActor emitter)
        {
            if (this.GetType() != typeof(DefendEffect))
            {
                //emitter.BattleActorAnimator.AttackPreview();
            }
        }
        
        public abstract IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers);

        public abstract IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers);

        public abstract IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers);
        
        public void SetCamera()
        {
            /*switch (battleCameraBatch)
            {
                case BattleCameraBatch.NULL:
                    CameraManager.Instance.SetCamera(cameraPath);
                    break;
                case BattleCameraBatch.CURRENT_ACTOR:
                    CameraManager.Instance.SetCamera(BattleManager.CurrentBattleActor.CameraBatchBattle, CameraKeys.BattleKeys.SkillExecutionDefault);
                    break;
                case BattleCameraBatch.CURRENT_ACTOR_PERSONALISE:
                    CameraManager.Instance.SetCamera(BattleManager.CurrentBattleActor.CameraBatchBattle, cameraSpecificPath);
                    break;
                case BattleCameraBatch.CURRENT_BATTLE:
                    CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, cameraBattlePath);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }*/
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

            if (berserk)
            {
                ApplyDamage(berserkDamage, emitter, emitter, berserk);
            }

            _damage = dmg;
            return dmg;
        }

        private float ApplyDamage(float damage, BattleActor emitter, BattleActor receiver, bool leaveOneHP = false)
        {
            var _damage = DamageCalculator.CalculateDamage(damage, emitter, receiver, ReferedSpell.SpellType, out var resistanceFaiblesseResult);

            if (rage)
            {
                var healthLosePercent = 1 - emitter.Health.NormalizedHealth;
                _damage += damage * (1 + healthLosePercent);
            }

            if(resistanceFaiblesseResult == DamageCalculator.ResistanceFaiblesseResult.FAIBLESSE)
            {
                //Fury.AddFuryPoint(10);
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
            _heal = heal;
            receivers.ForEach(w => w.Health.Heal(_heal));

            if (berserk)
            {
                ApplyDamage(berserkDamage, emitter, emitter, berserk);
            }
        }
    }
}
