using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public abstract class Node : ScriptableObject
    {
        public enum State
        {
            Running,
            Failure,
            Success
        }

        [TitleGroup("Main Settings"), PropertyOrder(-1)] [SerializeField] [OnValueChanged("ApplyName")]
        public string NodeName;

        [TitleGroup("Main Settings"), PropertyOrder(-1)] [Range(0, 100)]
        public int chance = 1;

        protected virtual void ApplyName() => name = NodeName;
        
        [TitleGroup("Debug"), ReadOnly]
        public State state = State.Running;
        [TitleGroup("Debug"), ReadOnly]
        public bool started = false;
        [TitleGroup("Debug"), ReadOnly]
        public string guid;
        [TitleGroup("Debug"), ReadOnly]
        public Vector2 position;

        public State Update()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if (state == State.Failure || state == State.Success)
            {
                OnStop();
                started = false;
            }

            return state;
        }
        
        protected abstract void OnStart();
        protected abstract State OnUpdate();
        protected abstract void OnStop();
    }
}

