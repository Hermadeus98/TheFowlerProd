using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace TheFowler
{
    [Serializable]
    public class DialogueControlClip : PlayableAsset, ITimelineClipAsset
    {
        public  DialogueControlBehaviour template = new DialogueControlBehaviour();

        public float _duration;
        public ClipCaps clipCaps
        {
            get
            {
                return ClipCaps.All;
            }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<DialogueControlBehaviour>.Create(graph);

            DialogueControlBehaviour dialogueControlBehaviour = playable.GetBehaviour();
            //Dialogue tree = dialogueControlBehaviour.dialogue;

            


            return playable;
        }

    }
}

