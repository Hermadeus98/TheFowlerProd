using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class Pheobe : Character
    {
        protected override void OnStart()
        {
            base.OnStart();
            
            Player.Pheobe = this;
        }
    }
}
