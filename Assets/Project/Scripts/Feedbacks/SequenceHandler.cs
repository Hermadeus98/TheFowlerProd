using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace TheFowler
{
    public class SequenceHandler : SerializedMonoBehaviour
    {
        [ReadOnly] public List<SequenceKeys> sequences;

        
        private void Start()
        {
            var binding = GetComponentsInChildren<SequenceBinding>();

            sequences = new List<SequenceKeys>();

            for (int i = 0; i < binding.Length; i++)
            {
                var s = new SequenceKeys()
                {
                    key = binding[i].SequenceEnum,
                    value = binding[i].GetPlayable,
                };

                sequences.Add(s);
            }
        }


        public PlayableDirector GetSequence(SequenceEnum key)
        {
            for (int i = 0; i < sequences.Count; i++)
            {
                if (sequences[i].key == key)
                    return sequences[i].value;
            }

            return null;
        }
    }

    public class SequenceKeys
    {
        public SequenceEnum key;
        public PlayableDirector value;
    }

    public enum SequenceEnum
    {
        NULL,
        DAMAGE,
        HEAL,
        DAMAGE_GROUP
    }
}
