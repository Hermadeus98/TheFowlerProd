using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace TheFowler
{
    public class MenuCharactersView : UIView
    {
        [SerializeField] InitiativeView initiativeView; 
        [SerializeField] SkillTreeView skillTreeView;
        [SerializeField] GameObject firstSelectedObject, background, confirmation;
        [SerializeField] UnityEngine.InputSystem.PlayerInput Inputs;
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
            background.SetActive(true);
            if (VolumesManager.Instance != null)
                VolumesManager.Instance.BlurryUI.enabled = true;

            if (eventSytemGO == null)
            {

                eventSytemGO = GameObject.Find("EventSystem");
                eventSytem = eventSytemGO.GetComponent<EventSystem>();

            }
            eventSytem.SetSelectedGameObject(firstSelectedObject);

            initiativeView.Show(false);

            StartCoroutine(WaitOnMenu());

            InfoBoxButtons[] infoButtons = new InfoBoxButtons[2];
            infoButtons[0] = InfoBoxButtons.CONFIRM;
            infoButtons[1] = InfoBoxButtons.CLOSE;

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);

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


            if (VolumesManager.Instance != null)
                VolumesManager.Instance.BlurryUI.enabled = false;

            eventSytem.SetSelectedGameObject(null);
        }

        private IEnumerator WaitOnMenu()
        {
            yield return new WaitForEndOfFrame();
            onMenu = true;
        }

        private void Update()
        {
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
    }
}

