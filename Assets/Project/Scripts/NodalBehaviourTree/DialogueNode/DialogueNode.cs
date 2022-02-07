using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class DialogueNode : CompositeNode
    {
        [TitleGroup("Main Settings"), PropertyOrder(-1)]
        public Dialogue dialogue;

        [TitleGroup("Main Settings"), PropertyOrder(-1), ReadOnly, ShowInInspector]
        public bool hasMultipleChoices => children.Count > 1;

        

        protected override void OnStart()
        {
            
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }

        protected override void OnStop()
        {
            
        }
    }
}
