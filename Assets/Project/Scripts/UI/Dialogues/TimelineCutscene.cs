using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace TheFowler
{
    public class TimelineCutscene : MonoBehaviour
    {
        [SerializeField] private PlayableDirector Timeline;

        public void ShowTwoDCutscene(Sprite overrideSprite)
        {
            UI.GetView<TwoDCutsceneView>(UI.Views.CutsceneView).Show(overrideSprite);
        }

        public void HideTwoDCutscene()
        {
            UI.GetView<TwoDCutsceneView>(UI.Views.CutsceneView).Hide();
        }
    }
}

