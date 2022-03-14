using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
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
            emitter.BattleActorAnimator.AttackPreview();
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
    }
}
