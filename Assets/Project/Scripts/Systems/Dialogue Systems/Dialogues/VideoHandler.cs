using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.InputSystem;
namespace TheFowler
{
    public class VideoHandler : GameplayPhase
    {
        [SerializeField] private VideoClip video;
        [SerializeField] private ActorActivator activator;
        [SerializeField] private PlayerInput Inputs;

        private VideoView view;
        public override void PlayPhase()
        {
            base.PlayPhase();

            view = UI.GetView<VideoView>(UI.Views.Video);

            view.Show(video);

            StartCoroutine(WaitEndVideo());

            activator.DesactivateActor(false);

            BlackPanel.Instance.Hide();

            AkSoundEngine.SetState("GameplayPhase", "Intro");
            AkSoundEngine.SetState("Scene", "Intro");
        }
        public override void EndPhase()
        {
            base.EndPhase();

            view.Hide();


            AkSoundEngine.SetState("Scene", "Scene1_TheGarden");
            AkSoundEngine.SetState("GameplayPhase", "Explo");
            

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

        private IEnumerator WaitEndVideoInput()
        {
            BlackPanel.Instance.Show();
            yield return new WaitForSeconds(1);
            EndPhase();
            BlackPanel.Instance.Hide(1);
            yield return null;
        }

        protected override void Update()
        {
            base.Update();
            if (isActive)
            {
                if (Inputs.actions["Start"].WasPressedThisFrame())
                {
                    StartCoroutine(WaitEndVideoInput());
                    isActive = false;
                }
            }
        }
    }
}

