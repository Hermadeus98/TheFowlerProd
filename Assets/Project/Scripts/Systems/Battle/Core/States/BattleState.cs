using System;
using Nrjwolf.Tools.AttachAttributes;
using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class BattleState : SerializedMonoBehaviour, Istate
    {
        [SerializeField] private string stateName;
        public string StateName { get => stateName; set => stateName = value; }

        [SerializeField, GetComponentInParent] protected PlayerInput inputs;

        [SerializeField] protected BattleCameraBatch battleCameraBatch = BattleCameraBatch.NULL;
        [SerializeField, HideIf("@this.battleCameraBatch == BattleCameraBatch.CURRENT_ACTOR")] protected cameraPath cameraPath;

        protected bool isActive = false;

        public virtual void OnStateEnter(EventArgs arg)
        {
            QRDebug.Log("BATTLE ", FrenchPallet.JALAPENOS_RED, $"{stateName}");
        }

        public virtual void OnStateExecute()
        {
        }

        public virtual void OnStateExit(EventArgs arg)
        {
        }

        protected void SetCamera(string defaultCam)
        {
            switch (battleCameraBatch)
            {
                case BattleCameraBatch.NULL:
                    CameraManager.Instance.SetCamera(cameraPath);
                    break;
                case BattleCameraBatch.CURRENT_ACTOR:
                    CameraManager.Instance.SetCamera(BattleManager.CurrentBattleActor.CameraBatchBattle, defaultCam);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    
    public enum BattleCameraBatch
    {
        NULL,
        CURRENT_ACTOR,
        CURRENT_ACTOR_PERSONALISE,
        CURRENT_BATTLE,
    }
}
