using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DynamicBinding : MonoBehaviour
{
    public TimelineAsset TimlineAsset;
    public string TrackName = "Cinemachine Track";
    public CinemachineBrain cineCam;
 
 
    private void Start()
    {
        cineCam = GameObject.FindObjectOfType<CinemachineBrain>();
 
        var director = this.GetComponent<PlayableDirector>();
        director.playableAsset = TimlineAsset;
 
        foreach (var playableAssetOutput in director.playableAsset.outputs)
        {
            if (playableAssetOutput.streamName == TrackName)
            {
                director.SetGenericBinding(playableAssetOutput.sourceObject, cineCam);
            }
 
        }
        //director.Play();
 
    }
}
