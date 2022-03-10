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
        }
        public override void EndPhase()
        {
            base.EndPhase();

            view.Hide();
            

            StopAllCoroutines();
        }

        private IEnumerator WaitEndVideo()
        {
            yield return new WaitForSeconds((float)video.length);
            EndPhase();
            yield return null;
        }
    }
}

