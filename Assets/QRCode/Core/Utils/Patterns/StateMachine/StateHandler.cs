using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace QRCode
{
    public class StateHandler : SerializedMonoBehaviour, Istate
    {
        [SerializeField]
        private string stateName;
        
        [FoldoutGroup("Callbacks")]
        public UnityEvent OnEnter, OnExecute, OnExit;
        public string StateName { get => stateName; set => stateName = value; }
        
        public void OnStateEnter(EventArgs arg)
        {
            OnEnter?.Invoke();
        }

        public void OnStateExecute()
        {
            OnExecute?.Invoke();
        }

        public void OnStateExit(EventArgs arg)
        {
            OnExit?.Invoke();
        }
    }
}
