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
            dialogueHandler.currentAnim.SetTrigger(animationTrigger);
        }
    }
}

