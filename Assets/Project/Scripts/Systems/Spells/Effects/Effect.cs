using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    [Serializable]
    public abstract class Effect
    {
        public abstract IEnumerator OnBeginCast();

        public abstract IEnumerator OnCast();

        public abstract IEnumerator OnFinishCast();
    }
}
