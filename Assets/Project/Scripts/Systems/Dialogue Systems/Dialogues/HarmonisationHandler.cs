using System;
using System.Collections;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class HarmonisationHandler : GameplayPhase
    {
        private HarmonisationView view;
        private InfoBoxView infoView;
        [TabGroup("References")]
        [SerializeField] private PlayerInput Inputs;
        [TabGroup("References")]
        [SerializeField] private GameplayPhase harmonisationAbigail, harmonisationPhoebe;
        [TabGroup("References")]
        [SerializeField] private bool isAbigailSolo;
        [TabGroup("References")]
        [SerializeField] private ActorActivator actorActivator;


        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            ChapterManager.onChapterChange += delegate (Chapter chapter) { EndPhase(); };
        }

        protected override void UnregisterEvent()
        {
            base.UnregisterEvent();
            ChapterManager.onChapterChange -= delegate (Chapter chapter) { EndPhase(); };
        }
        public override void PlayPhase()
        {
            base.PlayPhase();
            PlaceActor();

            view = UI.GetView<HarmonisationView>(UI.Views.Harmo);
            infoView = UI.GetView<InfoBoxView>(UI.Views.InfoBox);

            infoView.Hide();

            view.isAbigailSolo = isAbigailSolo;
            view.Show();

            if (isAbigailSolo)
            {
                infoView.Show();

                InfoBoxButtons[] infoButtons = new InfoBoxButtons[1];
                infoButtons[0] = InfoBoxButtons.CONFIRM;
                infoView.ShowProfile(infoButtons);
            }

            
        }

        public override void EndPhase()
        {
            base.EndPhase();

            infoView.Hide();
            view.Hide();
            actorActivator?.DesactivateActor();
        }

        //public override void PlayWithTransition()
        //{
        //    UI.GetView<TransitionView>(UI.Views.TransitionView).Show(TransitionType.HARMONISATION, PlayPhase);
        //}

        public void CheckInputs()
        {
            if (view.isChosing)
            {
                if (Inputs.actions["NavigateRight"].WasPressedThisFrame())
                {
                    view.ShowPhoebe();

                    infoView.Show();

                    InfoBoxButtons[] infoButtons = new InfoBoxButtons[2];
                    infoButtons[0] = InfoBoxButtons.CONFIRM;
                    infoButtons[1] = InfoBoxButtons.BACK;

                    infoView.ShowProfile(infoButtons);
                }
                else if (Inputs.actions["NavigateLeft"].WasPressedThisFrame())
                {
                    view.ShowAbi();

                    infoView.Show();

                    InfoBoxButtons[] infoButtons = new InfoBoxButtons[2];
                    infoButtons[0] = InfoBoxButtons.CONFIRM;
                    infoButtons[1] = InfoBoxButtons.BACK;

                    infoView.ShowProfile(infoButtons);
                }

            }
            else if (view.onAbi)
            {
                if (Inputs.actions["Select"].WasPressedThisFrame())
                {
                    view.Hide();
                    StartCoroutine(WaitCutscenePhase(harmonisationAbigail));
                    EndPhase();
                }
                else if (Inputs.actions["Return"].WasPressedThisFrame())
                {
                    view.ShowChoice();
                    infoView.Hide();
                }
            }

            else if (view.onPhoebe)
            {
                if (Inputs.actions["Select"].WasPressedThisFrame())
                {
                    view.Hide();
                    StartCoroutine(WaitCutscenePhase(harmonisationPhoebe));
                    EndPhase();
                }
                else if (Inputs.actions["Return"].WasPressedThisFrame())
                {
                    view.ShowChoice();
                    infoView.Hide();
                }
            }

            if (Inputs.actions["InfoBox"].WasPressedThisFrame())
            {

                infoView.CheckInfoBox();
            }

        }

        private void PlaceActor()
        {
            actorActivator?.ActivateActor();
        }

        private IEnumerator WaitCutscenePhase(GameplayPhase cutscene)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            cutscene.PlayPhase();
            yield break;
        }
        private void Update()
        {
            if (isActive)
            {
                CheckInputs();
            }
        }

    }
}

