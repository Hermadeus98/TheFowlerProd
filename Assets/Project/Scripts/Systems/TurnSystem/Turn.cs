using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public abstract class Turn
    {
        public abstract void OnTurnStart();

        public abstract void OnTurnEnd();
    }
}
