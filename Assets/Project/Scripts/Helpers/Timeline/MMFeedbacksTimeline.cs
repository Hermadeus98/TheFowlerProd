using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace TheFowler
{
    [TrackColor(241f/255f, 249f/255f, 90f/255f)]
    [TrackBindingType(typeof(MMFeedbacks))]
    [TrackClipType(typeof(MMFeedbacksClip))]
    public class MmFeedbacksTimeline : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<MMFeedbackTimelineBehaviour>.Create(graph, inputCount);
        }
    }
}
