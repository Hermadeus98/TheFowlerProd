using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class ActorReplacer : SerializedMonoBehaviour
    {
        public Dictionary<string, ActorPositions[]> repositionners = new Dictionary<string, ActorPositions[]>();

        [Button]
        public void ApplyRepositionning(string key)
        {
            repositionners[key].ForEach(w => w.Repositionning());
        }
    }

    public class ActorPositions
    {
        public Transform actor;
        public Transform targetPosition;

        public void Repositionning()
        {
            if (actor == null)
                return;
            
            actor.transform.position = targetPosition.position;
            actor.transform.rotation = targetPosition.rotation;
        }
    }
}
