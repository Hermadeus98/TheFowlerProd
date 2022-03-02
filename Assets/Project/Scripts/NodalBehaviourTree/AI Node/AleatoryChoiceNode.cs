using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class AleatoryChoiceNode : CompositeNode
    {
        public Node GetRandomNode()
        {
            var list = new List<Node>();

            for (int i = 0; i < children.Count; i++)
            {
                for (int j = 0; j < children[i].chance; j++)
                {
                    list.Add(children[i]);
                }
            }
            
            return list[Random.Range(0, list.Count)];
        }
        
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
