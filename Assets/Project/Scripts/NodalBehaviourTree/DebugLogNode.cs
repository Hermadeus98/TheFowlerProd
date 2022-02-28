using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class DebugLogNode : ActionNode
    {
        [TitleGroup("Main Settings")]
        [SerializeField] private string message;

        public void DebugLog() => Debug.Log($" {message}");
        
        protected override void OnStart()
        {
            Debug.Log($"OnStart {message}");
        }

        protected override void OnStop()
        {
            Debug.Log($"OnStop {message}");
        }

        protected override Node.State OnUpdate()
        {
            Debug.Log($"OnUpdate {message}");
            return State.Success;
        }
    }
}
