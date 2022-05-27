using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using DG.Tweening;
namespace TheFowler
{
    public class InitiativeView : UIView
    {

        [TabGroup("References")]
        [SerializeField] private InitiativeSelector[] selectors;
        [TabGroup("References")]
        [SerializeField] private UnityEngine.InputSystem.PlayerInput Inputs;
        [TabGroup("References")]
        [SerializeField] private Transform parentSelectors;
        [TabGroup("References")]
        [SerializeField] private MenuCharactersView menuView;
        [TabGroup("References")]
        [SerializeField] private RectTransform rectInitiative;

        [TabGroup("Wwise References")]
        [SerializeField] private AK.Wwise.Event onSelect, onDeselect, onValidate;

        private Transform selectedTransform;
        public bool isSelecting;

        private GameObject eventSytemGO;
        private UnityEngine.EventSystems.EventSystem eventSytem;

        private bool isInitiative = true;
        private bool isMenu;
        private bool canInput;
        public override void Show()
        {
            base.Show();

            InitializeSelectors();

            if (isInitiative)
            {

                menuView.onMenu = false;

                //if (VolumesManager.Instance != null)
                //    VolumesManager.Instance.BlurryUI.enabled = true;

                if (eventSytemGO == null)
                {

                    eventSytemGO = GameObject.Find("EventSystem");
                    eventSytem = eventSytemGO.GetComponent<EventSystem>();

                }
                eventSytem.SetSelectedGameObject(parentSelectors.GetChild(0).gameObject);

                StartCoroutine(WaitCanInput());

                InfoBoxButtons[] infoButtons = new InfoBoxButtons[2];
                infoButtons[0] = InfoBoxButtons.SELECT;
                infoButtons[1] = InfoBoxButtons.BACK;

                UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);

                rectInitiative.DOAnchorPos(new Vector2(-400, -254), .3f).SetEase(Ease.OutBounce);

                MenuCharactersSKHandler.Instance.InInitative();
            }
            else
            {
                SetSKActorInSpace();
            }

        }

        private IEnumerator WaitCanInput()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            canInput = true;
        }

        public void Show(bool value)
        {
            isInitiative = value;

            Show();
        }

        public void SetSKActorInSpace()
        {
            for (int i = 0; i < selectors.Length; i++)
            {
                switch (selectors[i].associatedData.actorName)
                {
                    case "Robyn":
                        switch (selectors[i].associatedData.initiativeOrder)
                        {
                            case 1:
                                MenuCharactersSKHandler.Instance.robyn.transform.DOMove(MenuCharactersSKHandler.Instance.position1.position, .2f);
                                break;
                            case 2:
                                MenuCharactersSKHandler.Instance.robyn.transform.DOMove(MenuCharactersSKHandler.Instance.position2.position, .2f);
                                break;
                            case 3:
                                MenuCharactersSKHandler.Instance.robyn.transform.DOMove(MenuCharactersSKHandler.Instance.position3.position, .2f);
                                break;
                        }
                        break;
                    case "Abigail":
                        switch (selectors[i].associatedData.initiativeOrder)
                        {
                            case 1:
                                MenuCharactersSKHandler.Instance.abi.transform.DOMove(MenuCharactersSKHandler.Instance.position1.position, .2f);
                                break;
                            case 2:
                                MenuCharactersSKHandler.Instance.abi.transform.DOMove(MenuCharactersSKHandler.Instance.position2.position, .2f);
                                break;
                            case 3:
                                MenuCharactersSKHandler.Instance.abi.transform.DOMove(MenuCharactersSKHandler.Instance.position3.position, .2f);
                                break;
                        }
                        break;
                    case "Phoebe":
                        switch (selectors[i].associatedData.initiativeOrder)
                        {
                            case 1:
                                MenuCharactersSKHandler.Instance.phoebe.transform.DOMove(MenuCharactersSKHandler.Instance.position1.position, .2f);
                                break;
                            case 2:
                                MenuCharactersSKHandler.Instance.phoebe.transform.DOMove(MenuCharactersSKHandler.Instance.position2.position, .2f);
                                break;
                            case 3:
                                MenuCharactersSKHandler.Instance.phoebe.transform.DOMove(MenuCharactersSKHandler.Instance.position3.position, .2f);
                                break;
                        }
                        break;
                }
                
            }
        }

        public override void Hide()
        {
            base.Hide();

            CanvasGroup.alpha = 0;
            //if (VolumesManager.Instance != null)
            //    VolumesManager.Instance.BlurryUI.enabled = false;

            for (int i = 0; i < selectors.Length; i++)
            {
                selectors[i]._Deselect();
            }

            if (isMenu)
            {
                if (menuView != null)
                    menuView.Show();
                isInitiative = false;
            }

            canInput = false;
            rectInitiative.DOAnchorPos(new Vector2(-281, -254), .3f).SetEase(Ease.OutBounce);
        }

        public void Hide(bool value)
        {
            isMenu = value;
            Hide();

        }

        public void InitializeSelectors()
        {
            for (int i = 0; i < selectors.Length; i++)
            {
                selectors[i].transform.SetSiblingIndex(selectors[i].associatedData.initiativeOrder - 1);

                if (isInitiative)
                {
                    selectors[i].InitialState();
                }
                else
                {
                    selectors[i].InitializeMenu();
                }

                selectors[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < menuView.numberOfAllies; i++)
            {
                selectors[i].gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (isActive && canInput)
            {
                if (Inputs.actions["Select"].WasPressedThisFrame())
                {
                    if (!isSelecting)
                    {
                        isSelecting = true;
                        onSelect.Post(gameObject);

                        for (int i = 0; i < menuView.numberOfAllies; i++)
                        {
                            if (selectors[i].isHover)
                            {
                                selectors[i].Pick();
                                selectedTransform = selectors[i].transform;
                            }
                            else
                            {
                                selectors[i].UnPick();
                            }
                        }
                    }
                    else
                    {
                        isSelecting = false;
                        onValidate.Post(gameObject);

                        for (int i = 0; i < menuView.numberOfAllies; i++)
                        {
                            if (selectors[i].isHover)
                            {
                                eventSytem.SetSelectedGameObject(selectors[i].gameObject);
                            }

                            selectors[i].InitialState();
                        }

                       
                    }


                }
                else if (Inputs.actions["Return"].WasPressedThisFrame())
                {
                    if (isSelecting)
                    {
                        isSelecting = false;
                        onDeselect.Post(gameObject);

                        for (int i = 0; i < menuView.numberOfAllies; i++)
                        {
                            if (selectors[i].isHover)
                            {
                                eventSytem.SetSelectedGameObject(selectors[i].gameObject);
                            }

                            selectors[i].InitialState();
                        }


                    }
                    else
                    {
                        Hide(true);
                        menuView.background.SetActive(true);
                    }


                }

                else if (Inputs.actions["NavigateUp"].WasReleasedThisFrame())
                {
                    if (!isSelecting) return;

                    ChangePlaceUp();
                }

                else if (Inputs.actions["NavigateDown"].WasReleasedThisFrame())
                {
                    if (!isSelecting) return;

                    ChangeDown();
                }
            }
        }

        private void ChangePlaceUp()
        {
            if (selectedTransform.GetSiblingIndex() == 0) return;

            selectedTransform.SetSiblingIndex(selectedTransform.GetSiblingIndex() - 1);
        }

        private void ChangeDown()
        {
            if (selectedTransform.GetSiblingIndex() == menuView.numberOfAllies - 1) return;

            selectedTransform.SetSiblingIndex(selectedTransform.GetSiblingIndex() + 1);
        }

    }
}

