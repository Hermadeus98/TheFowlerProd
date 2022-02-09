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
    }
}
