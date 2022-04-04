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
        public string EffectName = "NO NAME EFFECT";

        public TargetTypeEnum TargetType;

        [SerializeField] protected BattleCameraBatch battleCameraBatch = BattleCameraBatch.NULL;
        [SerializeField, ShowIf("@this.battleCameraBatch == BattleCameraBatch.NULL")] protected cameraPath cameraPath;
        [SerializeField, ShowIf("@this.battleCameraBatch == BattleCameraBatch.CURRENT_ACTOR_PERSONALISE")] private string cameraSpecificPath = "Default";
        [SerializeField, ShowIf("@this.battleCameraBatch == BattleCameraBatch.CURRENT_BATTLE")] private string cameraBattlePath = "Default";

        public bool ImPreview = false;
        
        public AudioGenericEnum audioEvent;
        public string eventName;
        
        [ReadOnly] public Spell ReferedSpell;

        public virtual void PreviewEffect(BattleActor emitter)
        {
            if (this.GetType() != typeof(DefendEffect))
            {
                emitter.BattleActorAnimator.AttackPreview();
            }
        }
        
        public abstract IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers);

        public abstract IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers);

        public abstract IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers);
        
        public void SetCamera()
        {
            switch (battleCameraBatch)
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
            }
        }

        public virtual void OnSimpleCast(BattleActor emitter, BattleActor[] receivers)
        {
            Debug.Log("CAST");
        }
        
        
        protected void Damage(float damage, BattleActor emitter, BattleActor[] receivers)
        {
            foreach (var receiver in receivers)
            {
                var _damage = DamageCalculator.CalculateDamage(damage, emitter, receiver, ReferedSpell.SpellType, out var resistanceFaiblesseResult);

                if(resistanceFaiblesseResult == DamageCalculator.ResistanceFaiblesseResult.FAIBLESSE)
                {
                    Fury.AddFuryPoint(15);
                }

                SoundManager.PlaySoundDamageTaken(receiver, resistanceFaiblesseResult);
                
                receiver.Health.TakeDamage(
                    _damage
                );
            }
        }

        protected void Heal(float heal, BattleActor emitter, BattleActor[] receivers)
        {
            receivers.ForEach(w => w.Health.Heal(heal));
        }
    }
}
