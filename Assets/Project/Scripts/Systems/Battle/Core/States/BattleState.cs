using System;
using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class BattleState : SerializedMonoBehaviour, Istate
    {
        [SerializeField] private string stateName;
        public string StateName { get => stateName; set => stateName = value; }
        
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
