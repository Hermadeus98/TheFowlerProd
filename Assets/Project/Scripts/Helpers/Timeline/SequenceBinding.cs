using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace TheFowler
{
    public class SequenceBinding : SerializedMonoBehaviour
    {
        public SequenceEnum SequenceEnum;

        public PlayableDirector GetPlayable => GetComponent<PlayableDirector>();
    }
}
