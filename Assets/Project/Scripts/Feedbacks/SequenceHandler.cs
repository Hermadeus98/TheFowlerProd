using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace TheFowler
{
    public class SequenceHandler : SerializedMonoBehaviour
    {
        public SequenceKeys[] sequences;

        public PlayableDirector GetSequence(SequenceEnum key)
        {
            for (int i = 0; i < sequences.Length; i++)
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
        ROBYN_BASIC_ATTACK,
        ABY_BASIC_ATTACK,
        PHOEBE_BASIC_ATTACK,
        ENEMY_BASIC_ATTACK,
        
        ROBYN_ATTACK_FINISHER,
    }
}
