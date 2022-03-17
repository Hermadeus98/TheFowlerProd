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

        private float elapsedTimePassCutscene;
        private bool videoPassed = false;

        private VideoView view;
        public override void PlayPhase()
        {
            base.PlayPhase();


            videoPassed = false;
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

        private IEnumerator WaitEndVideoInput()
        {
            videoPassed = true;
            BlackPanel.Instance.Show();
            yield return new WaitForSeconds(1);
            EndPhase();
            BlackPanel.Instance.Hide(1);
            yield return null;
        }

        protected override void Update()
        {
            base.Update();
            if (isActive && !videoPassed)
            {
                CallRappelInput();
                
            }
        }

        private void CallRappelInput()
        {

            if (Inputs.actions["Select"].IsPressed())
            {
                elapsedTimePassCutscene += Time.deltaTime;
                view.rappelInput.RappelInputFeedback(elapsedTimePassCutscene);

                if (elapsedTimePassCutscene >= 1)
                {
                    elapsedTimePassCutscene = 0;
                    view.rappelInput.RappelInputFeedback(elapsedTimePassCutscene);
                    StartCoroutine(WaitEndVideoInput());
                }
            }
            else if (Inputs.actions["Select"].WasReleasedThisFrame())
            {
                elapsedTimePassCutscene = 0;
                view.rappelInput.RappelInputFeedback(elapsedTimePassCutscene);
            }

        }
    }
}

