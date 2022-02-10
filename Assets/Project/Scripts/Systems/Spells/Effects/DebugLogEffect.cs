using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DebugLogEffect : Effect
    {
        public override IEnumerator OnBeginCast()
        {
            Debug.Log("ON BEGIN CAST");
            yield break;
        }

        public override IEnumerator OnCast()
        {
            Debug.Log("CAST");
            yield break;
        }

        public override IEnumerator OnFinishCast()
        {
            Debug.Log("ON FINISH CAST");
            yield break;
        }
    }
}
