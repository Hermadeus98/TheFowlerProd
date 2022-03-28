using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace TheFowler
{
    public class SequenceHandler : SerializedMonoBehaviour
    {
        public Dictionary<SequenceEnum, PlayableDirector> database = new Dictionary<SequenceEnum, PlayableDirector>();

        public PlayableDirector GetSequence(SequenceEnum key)
        {
            return database[key];
        }
    }

    public enum SequenceEnum
    {
        NULL,
        ROBYN_BASIC_ATTACK,
        ABY_BASIC_ATTACK,
        PHOEBE_BASIC_ATTACK
    }
}
