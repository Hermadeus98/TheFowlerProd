using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace TheFowler
{
    [Serializable]
    public class MMFeedbacksClip : PlayableAsset, ITimelineClipAsset
    {
        public ExposedReference<MMFeedbacks> feedback;

        [SerializeField] private MMFeedbackTimelineBehaviour template = new MMFeedbackTimelineBehaviour();
        
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return ScriptPlayable<MMFeedbackTimelineBehaviour>.Create(graph, template);
        }
        
        public ClipCaps clipCaps { get => ClipCaps.None; }
    }
}
