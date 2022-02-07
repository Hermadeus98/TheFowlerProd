using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public abstract class CompositeNode : Node
    {
        [TitleGroup("Debug")]
        public List<Node> children = new List<Node>();
        
        [TitleGroup("Main Settings"), PropertyOrder(-1), ReadOnly, ShowInInspector]
        public bool isLast => children.Count == 0;
    }
}
