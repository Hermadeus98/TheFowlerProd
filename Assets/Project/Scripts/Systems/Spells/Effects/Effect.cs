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
        [SerializeField, HideIf("@this.battleCameraBatch == BattleCameraBatch.CURRENT_ACTOR")] protected cameraPath cameraPath;
        
        public abstract IEnumerator OnBeginCast();

        public abstract IEnumerator OnCast();

        public abstract IEnumerator OnFinishCast();
        
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
