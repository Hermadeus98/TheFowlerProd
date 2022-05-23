using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class IntroVideoHandler : VideoHandler
    {

        [SerializeField] private GameplayPhase nextPhase;
        [SerializeField] private AK.Wwise.Event onStartWWISE, onEndWWISE, onBreakWallWwise;
        [SerializeField] private float timeCodeBreakWall;

        public override void PlayPhase()
        {
            base.PlayPhase();
            onStartWWISE.Post(gameObject);
            StartCoroutine(WaitEventWall());
        }

        public override void EndPhase()
        {
            base.EndPhase();
            LaunchContextualAction();
            onEndWWISE.Post(gameObject);
            onBreakWallWwise.Post(gameObject);

        }
        public void LaunchContextualAction()
        {
            UI.GetView<ContextualActionView>(UI.Views.ContextualAction).Show(ContextualActionLocation.INTRO, 8, End);
        }

        public void End()
        {
            UI.GetView<ContextualActionView>(UI.Views.ContextualAction).Hide();
            nextPhase.PlayPhase();
        }

        private IEnumerator WaitEventWall()
        {
            yield return new WaitForSeconds(timeCodeBreakWall);

            //Wall Break State
            Debug.Log("Set State Wwise");
            onBreakWallWwise.Post(gameObject);

        }
    }
}

