using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using AK.Wwise;
using System;
using Sirenix.OdinInspector;
using UnityEngine.Timeline;
using System.Linq;
using UnityEngine.Rendering;
using DG.Tweening;
using Cinemachine;
namespace TheFowler
{
    public class TimelineCutscene : MonoBehaviour
    {
        [SerializeField] private PlayableDirector Timeline;
        [SerializeField] private DialogueHandler dialogueHandler;
        [SerializeField] Animator robynAnim, phoebeAnim, abiAnim;
        [SerializeField] UnityEngine.Events.UnityEvent[] evs;
        [SerializeField] AK.Wwise.Event[] sounds;
        private Animator currentAnim;

        public TrackAsset track;
        public object trackObject;
        private float id;
        private DialogueNode node;
        public void ShowTwoDCutscene(Sprite overrideSprite)
        {
            UI.GetView<TwoDCutsceneView>(UI.Views.CutsceneView).Show(overrideSprite);
        }

        public void HideTwoDCutscene()
        {
            UI.GetView<TwoDCutsceneView>(UI.Views.CutsceneView).Hide();
        }

        public void PlaySound(AK.Wwise.Event sound)
        {
            sound.Post(this.gameObject);
        }

        public void PlaySound(int id)
        {
            sounds[id].Post(this.gameObject);
        }

        public void SetAnim(string animationTrigger)
        {
            if(currentAnim != null)
            {
                currentAnim.SetTrigger(animationTrigger);
            }
            else
            {
                dialogueHandler.currentAnim.SetTrigger(animationTrigger);
            }

        }

        public void PlayUnityEvent(int ID)
        {
            evs[ID].Invoke();
        }

        public void ChangePPWeight(Volume volume)
        {

            DOTween.To(() => volume.weight, x => volume.weight = x, 1, 4.5f);
            
        }



        public void PlaysSentence(DialogueNode sentence)
        {

            DialogueStaticView view = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs) ;
            view.Show();
            view.DisplaySentence(sentence);
            PlaySound(sentence.dialogue.voice);

            switch (sentence.dialogue.ActorEnum)
            {
                case ActorEnum.ROBYN:
                    currentAnim = robynAnim;
                    break;
                case ActorEnum.PHEOBE:
                    currentAnim = phoebeAnim;
                    break;
                case ActorEnum.ABIGAEL:
                    currentAnim = abiAnim;
                    break;
            }
            

        }

        public void HideSentence()
        {

            DialogueStaticView view = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs);
            view.Hide();

        }

        [Button]
        public void EndTimeline()
        {
            Timeline.time = Timeline.duration;
        }

        [Button]
        public  void CreateClips()
        {
            id = 0;

            for (int i = 0; i < 100; i++)
            {

                TimelineAsset timeline = Timeline.playableAsset as TimelineAsset;
                track = timeline.GetOutputTrack(i);

                trackObject = Timeline.GetGenericBinding(track);

                if(trackObject != null)
                {
                    if (trackObject.GetType() == typeof(BehaviourTree))
                    {
                        BehaviourTree tree = trackObject as BehaviourTree;

                        foreach (TimelineClip item in track.GetClips().ToArray())
                        {
                            track.DeleteClip(item);
                        }


                        for (int j = 0; j < tree.nodes.Count; j++)
                        {

                            TimelineClip clip =  track.CreateDefaultClip();

                            DialogueControlClip asset = clip.asset as DialogueControlClip;

                            DialogueNode node = tree.nodes[j] as DialogueNode;

                            asset.template.dialogue = node;

                            clip.start = id;
                            clip.duration = node.dialogue.displayDuration;

                            id += (float)clip.duration;
                        }


                        break;
                    }
                }

            }


        }

        [Button]
        public void DeleteClips()
        {
            id = 0;
            for (int h = 0; h < 10; h++)
            {
                for (int i = 0; i < 100; i++)
                {

                    TimelineAsset timeline = Timeline.playableAsset as TimelineAsset;
                    track = timeline.GetOutputTrack(i);

                    trackObject = Timeline.GetGenericBinding(track);

                    if (trackObject != null)
                    {
                        if (trackObject.GetType() == typeof(BehaviourTree))
                        {

                            for (int j = 0; j < track.GetClips().ToArray().Length; j++)
                            {
                                track.DeleteClip(track.GetClips().ToArray()[j]);
                            }


                            break;
                        }
                    }

                }
            }

            

        }



    }
}

