using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DebugLogEffect : Effect
    {
        public override IEnumerator OnBeginCast()
        {
            yield break;
        }

        public override IEnumerator OnCast()
        {
            Debug.Log("CAST " + EffectName);
            yield break;
        }

        public override IEnumerator OnFinishCast()
        {
            yield break;
        }
    }
}
