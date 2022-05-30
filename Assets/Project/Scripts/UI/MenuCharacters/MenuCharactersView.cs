using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MoreMountains.Feedbacks;
namespace TheFowler
{
    public class MenuCharactersView : UIView
    {
        [SerializeField] InitiativeView initiativeView; 
        [SerializeField] SkillTreeView skillTreeView;
        [SerializeField] public GameObject firstSelectedObject, background, confirmation;
        [SerializeField] public MMFeedbacks[] MMUnselects;
        [SerializeField] public UnityEngine.InputSystem.PlayerInput Inputs;
        private Battle battle;
        public bool onMenu;

        private GameObject eventSytemGO;
        private UnityEngine.EventSystems.EventSystem eventSytem;
        private bool onConfirmation;
        public int numberOfAllies = 0;
        public override void Show()
        {
            base.Show();

            numberOfAllies = battle.numberOfAllies;

            if (!onMenu)
            {
                MenuCharactersSKHandler.Instance.Initialize();


                if (numberOfAllies == 2)
                {
                    MenuCharactersSKHandler.Instance.abi.gameObject.SetActive(true);
                    MenuCharactersSKHandler.Instance.robyn.gameObject.SetActive(true);
                    MenuCharactersSKHandler.Instance.phoebe.gameObject.SetActive(false);
                }
                else
                {
                    MenuCharactersSKHandler.Instance.abi.gameObject.SetActive(true);
                    MenuCharactersSKHandler.Instance.robyn.gameObject.SetActive(true);
                    MenuCharactersSKHandler.Instance.phoebe.gameObject.SetActive(true);
                }

                MenuCharactersSKHandler.Instance.abi.SetTrigger("SpellExecution1");
                MenuCharactersSKHandler.Instance.robyn.SetTrigger("SpellExecution1");
                MenuCharactersSKHandler.Instance.phoebe.SetTrigger("SpellExecution1");




            }

            onMenu = true;

            background.SetActive(true);
            //if (VolumesManager.Instance != null)
            //    VolumesManager.Instance.BlurryUI.enabled = true;

            if (eventSytemGO == null)
            {

                eventSytemGO = GameObject.Find("EventSystem");
                eventSytem = eventSytemGO.GetComponent<EventSystem>();

            }

            for (int i = 0; i < MMUnselects.Length; i++)
            {
                MMUnselects[i].PlayFeedbacks();
            }


            eventSytem.SetSelectedGameObject(firstSelectedObject);

            initiativeView.Show(false);

            StartCoroutine(WaitOnMenu());

            InfoBoxButtons[] infoButtons = new InfoBoxButtons[2];
            infoButtons[0] = InfoBoxButtons.CONFIRM;
            infoButtons[1] = InfoBoxButtons.CLOSE;

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);

            if (!Tutoriel.hasProgression)
            {
                Tutoriel.hasProgression = true;
                UI.GetView<TutorielView>(UI.Views.Tuto).Show(TutorielEnum.PROGRESSION, 1f);

            }

            MenuCharactersSKHandler.Instance.InMenu();

            //skillTreeView.Show();

        }

        public void Show(Battle _battle)
        {
            battle = _battle;
            Show();
        }

        public override void Hide()
        {
            base.Hide();
            eventSytem.SetSelectedGameObject(null);

            onMenu = false;
            battle = null;
            onConfirmation = false;

            confirmation.SetActive(false);
            background.SetActive(true);
            initiativeView.gameObject.SetActive(true);
            initiativeView.isActive = false;

            //if (VolumesManager.Instance != null)
            //    VolumesManager.Instance.BlurryUI.enabled = false;

            eventSytem.SetSelectedGameObject(null);

            MenuCharactersSKHandler.Instance.Close();
        }

        private IEnumerator WaitOnMenu()
        {
            yield return new WaitForEndOfFrame();
            onMenu = true;
        }

        public void RefreshSkillTree()
        {
            initiativeView.Hide(false);
            onMenu = false;
            
            skillTreeView.Show();
            eventSytem.SetSelectedGameObject(skillTreeView.firstSelectedObject.gameObject);
            
            background.SetActive(false);

            MenuCharactersSKHandler.Instance.InSkillTree();
        }

        private void Update()
        {
            return;


            if(isActive && onMenu)
            {
                if (Inputs.actions["Return"].WasPressedThisFrame())
                {
                    if (!onConfirmation)
                    {
                        onConfirmation = true;

                        confirmation.SetActive(true);
                        background.SetActive(false);

                        initiativeView.gameObject.SetActive(false);

                        InfoBoxButtons[] infoButtons = new InfoBoxButtons[2];
                        infoButtons[0] = InfoBoxButtons.SELECT;
                        infoButtons[1] = InfoBoxButtons.BACK;

                        UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);
                    }
                    else
                    {
                        onConfirmation = false;
                        confirmation.SetActive(false);
                        background.SetActive(true);


                        eventSytem.SetSelectedGameObject(firstSelectedObject);
                        initiativeView.gameObject.SetActive(true);

                        InfoBoxButtons[] infoButtons = new InfoBoxButtons[2];
                        infoButtons[0] = InfoBoxButtons.SELECT;
                        infoButtons[1] = InfoBoxButtons.CLOSE;

                        UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);
                    }

                }

                else if (Inputs.actions["Select"].WasPressedThisFrame())
                {
                    if (onConfirmation)
                    {
                        if (battle != null)
                        {
                            battle.PlayPhase();
                        }

                        Hide();

                    }
                }
            }
        }

        public void LaunchBattle()
        {
            StartCoroutine(WaitLaunchBattle());
        }

        private IEnumerator WaitLaunchBattle()
        {
            yield return new WaitForEndOfFrame();
            battle.PlayPhase();

            Hide();
        }
    }
}

