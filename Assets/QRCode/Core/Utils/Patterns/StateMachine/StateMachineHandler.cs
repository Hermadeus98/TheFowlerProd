using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode
{
    public class StateMachineHandler : SerializedMonoBehaviour
    {
        [SerializeField] private UpdateMode updateMode;
        
        [SerializeField] private StateHandler[] states;

        public StateMachine StateMachine { get; private set; }

        [Button]
        public void Initialize()
        {
            StateMachine = new StateMachine(states, states[0].StateName, updateMode, EventArgs.Empty);
        }
    }
}
