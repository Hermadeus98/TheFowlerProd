using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class CastNode : CompositeNode
    {
        public Spell spellToCast;
        
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
