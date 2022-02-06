using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class SequencerNode : CompositeNode
    {
        private int current;
        
        protected override void OnStart()
        {
            current = 0;
        }

        protected override State OnUpdate()
        {
            var child = children[current];
            switch (child.Update())
            {
                case State.Running:
                    return State.Running;
                case State.Failure:
                    return State.Failure;
                case State.Success:
                    current++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return current == children.Count ? State.Success : State.Running;
        }

        protected override void OnStop()
        {
        }
    }
}
