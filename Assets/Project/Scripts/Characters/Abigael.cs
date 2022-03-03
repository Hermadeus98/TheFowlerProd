using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class Abigael : Character
    {
        protected override void OnStart()
        {
            base.OnStart();
            Player.Abigael = this;
        }

    }
}
