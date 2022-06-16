using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace TheFowler
{
    public class IntroVideoHandler : VideoHandler
    {

        [SerializeField] private GameplayPhase nextPhase;
        [SerializeField] private AK.Wwise.Event onStartWWISE, onEndWWISE, onBreakWallWwise;
        [SerializeField] private float timeCodeBreakWall;
        [SerializeField] private Animator animSubtitles;

        public override void PlayPhase()
        {
            base.PlayPhase();
           
            animSubtitles.SetTrigger("Play");
            onStartWWISE.Post(gameObject);
            StartCoroutine(WaitUnfadePanel());

        }

        public override void EndPhase()
        {
            base.EndPhase();
            End();
            //LaunchContextualAction();
            onEndWWISE.Post(gameObject);
            onBreakWallWwise.Post(gameObject);

            AkSoundEngine.SetState("Scene", "Scene1_TheGarden");
        }

        public void LaunchContextualAction()
        {
           // UI.GetView<ContextualActionView>(UI.Views.ContextualAction).Show(ContextualActionLocation.INTRO, 8, End);
        }

        public void End()
        {
            //UI.GetView<ContextualActionView>(UI.Views.ContextualAction).Hide();
            nextPhase.PlayPhase();
        }

        private IEnumerator WaitUnfadePanel()
        {
            yield return new WaitForSeconds(.3f);
            BlackPanel.instance.HidePanelIntro();
        }

        private IEnumerator WaitEventWall()
        {
            yield return new WaitForSeconds(timeCodeBreakWall);
            
            //onBreakWallWwise.Post(gameObject);

        }
    }
}

