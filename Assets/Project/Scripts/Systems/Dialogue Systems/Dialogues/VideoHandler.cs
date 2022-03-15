using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
namespace TheFowler
{
    public class VideoHandler : GameplayPhase
    {
        [SerializeField] private VideoClip video;
        [SerializeField] private ActorActivator activator;

        private VideoView view;
        public override void PlayPhase()
        {
            base.PlayPhase();

            view = UI.GetView<VideoView>(UI.Views.Video);

            view.Show(video);

            StartCoroutine(WaitEndVideo());

            activator.DesactivateActor(false);

            BlackPanel.Instance.Hide();
        }
        public override void EndPhase()
        {
            base.EndPhase();

            view.Hide();
            

            StopAllCoroutines();
        }

        private IEnumerator WaitEndVideo()
        {
            yield return new WaitForSeconds((float)video.length - 1);
            BlackPanel.Instance.Show();
            yield return new WaitForSeconds(1);
            EndPhase();
            BlackPanel.Instance.Hide(1);
            yield return null;
        }
    }
}

