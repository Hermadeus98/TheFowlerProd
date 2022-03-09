using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
namespace TheFowler
{
    public class VideoHandler : GameplayPhase
    {
        [SerializeField] private VideoClip video;

        private VideoView view;
        public override void PlayPhase()
        {
            base.PlayPhase();

            view = UI.GetView<VideoView>(UI.Views.Video);

            view.Show(video);

            StartCoroutine(WaitEndVideo());
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

