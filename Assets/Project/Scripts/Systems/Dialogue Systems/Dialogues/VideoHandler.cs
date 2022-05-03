using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class VideoHandler : GameplayPhase
    {
        [SerializeField] protected VideoClip video;
        [SerializeField] private ActorActivator activator;
        [SerializeField] private PlayerInput Inputs;

        private float elapsedTimePassCutscene;
        protected bool videoPassed = false;

        public bool PlayWWISEState;
        [SerializeField] private bool canPass = false;
        [SerializeField] private bool playBlackPanel = true;


        private VideoView view;

        public System.Action chapterLoader;
        public override void PlayPhase()
        {
            base.PlayPhase();
            PlayElements();
        }

        public void PlayPhase(System.Action action)
        {
            base.PlayPhase();
            PlayElements();
            chapterLoader = action;
        }



        private void PlayElements()
        {
            videoPassed = false;
            view = UI.GetView<VideoView>(UI.Views.Video);

            view.Show(video);

            StartCoroutine(WaitEndVideo());

            if (activator != null)
                activator.DesactivateActor(false);

            BlackPanel.Instance.Hide(.5f);

            if (PlayWWISEState)
            {
                AkSoundEngine.SetState("GameplayPhase", "Intro");
                AkSoundEngine.SetState("Scene", "Intro");
            }

        }
        public override void EndPhase()
        {
            base.EndPhase();

            view.Hide();

            if (PlayWWISEState)
            {
                AkSoundEngine.SetState("Scene", "Scene1_TheGarden");
                AkSoundEngine.SetState("GameplayPhase", "Explo");

            }

            StopAllCoroutines();

            if(chapterLoader != null)
                chapterLoader.Invoke();

        }

        private IEnumerator WaitEndVideo()
        {
            yield return new WaitForSeconds((float)video.length - 1);
            if(playBlackPanel)
                BlackPanel.Instance.Show(.5f);
            yield return new WaitForSeconds(1);
            EndPhase();
            if (playBlackPanel)
                BlackPanel.Instance.Hide(1);
            yield return null;
        }

        private IEnumerator WaitEndVideoInput()
        {
            videoPassed = true;
            if(playBlackPanel)
                BlackPanel.Instance.Show();
            yield return new WaitForSeconds(1);
            EndPhase();
            if (playBlackPanel)
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
            if (canPass)
            {
                view.rappelInput.gameObject.SetActive(true);

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
            else
            {
                view.rappelInput.gameObject.SetActive(false);
            }
        }
        
        
    }
}

