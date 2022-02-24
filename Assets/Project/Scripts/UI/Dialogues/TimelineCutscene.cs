using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using AK.Wwise;
using System;

namespace TheFowler
{
    public class TimelineCutscene : MonoBehaviour
    {
        [SerializeField] private PlayableDirector Timeline;
        [SerializeField] private DialogueHandler dialogueHandler;
        [SerializeField] Animator robynAnim, phoebeAnim, abiAnim;
        [SerializeField] UnityEngine.Events.UnityEvent[] evs;
        private Animator currentAnim;
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
    }
}

