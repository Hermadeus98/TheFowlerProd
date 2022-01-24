using System;
using System.Collections;
using System.Collections.Generic;
using QRCode.Utils;
using UnityEngine;

namespace QRCode
{
    public class StateMachine
    {
        //--<Privates>
        private Dictionary<string, Istate> states;
        private Istate currentState;

        //--<Publics>
        public bool Pause = false;
        
        //---<Properties>----------------------------------------------------------------------------------------------<
        private Dictionary<string, Istate> States => states;
        private Istate CurrentState => currentState;
        
        //---<INITIALISATION>------------------------------------------------------------------------------------------<
        public StateMachine (Istate[] states, string firstState, UpdateMode updateMode , EventArgs args)
        {
            this.states = new Dictionary<string, Istate>();
            for (int i = 0; i < states.Length; i++)
                AddState(states[i]);
            SetState(firstState, args);
            UpdateRunner.Instance.Register(updateMode, Execute);
        }

        //---<CORE>----------------------------------------------------------------------------------------------------<
        public void SetState(string key, EventArgs args)
        {
            currentState?.OnStateExit(args);
            currentState = states[key];
            currentState.OnStateEnter(args);
        }

        private void Execute()
        {
            if(Pause)
                return;
            
            currentState?.OnStateExecute();
        }

        public void AddState(Istate state)
        {
            if(!states.ContainsKey(state.StateName))
                states.Add(state.StateName, state);
        }

        public void RemoveState(string key)
        {
            if(states.ContainsKey(key))
                states.Remove(key);
        }
    }

    public enum UpdateMode
    {
        Update,
        FixedUpdate,
        LateUpdate
    }
}
