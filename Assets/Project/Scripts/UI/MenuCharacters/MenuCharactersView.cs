using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MoreMountains.Feedbacks;
using UnityEngine.InputSystem;
namespace TheFowler
{
    public class MenuCharactersView : UIView
    {
        [SerializeField] InitiativeView initiativeView;
        [SerializeField] SkillTreeView skillTreeView;
        [SerializeField] public GameObject firstSelectedObject, background, confirmation, exclamation;
        [SerializeField] public BattleActorData[] battleActorDatas;
        [SerializeField] public MMFeedbacks[] MMUnselects;
        public UnityEngine.InputSystem.PlayerInput Inputs;
        public CustomElement[] customElements;
        private Battle battle;
        public bool onMenu;

        private GameObject eventSytemGO;
        private UnityEngine.EventSystems.EventSystem eventSytem;
        private bool onConfirmation;
        public int numberOfAllies = 0;
        private bool isBattleLaunched;

        [SerializeField] private CanvasGroup battleUI;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (eventSytemGO == null)
            {

                eventSytemGO = GameObject.Find("EventSystem");
                eventSytem = eventSytemGO.GetComponent<EventSystem>();

            }
        }
        public override void Show()
        {
            base.Show();

            Inputs.enabled = true;

            BlackPanel.Instance.Hide(1f);
            
            SetExclamation();

            numberOfAllies = battle.numberOfAllies;

            AkSoundEngine.SetState("GameplayPhase", "SkillTree");

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
                UI.GetView<TutorielView>(UI.Views.Tuto).Show(TutorielEnum.PROGRESSION, .5f);

            }

            MenuCharactersSKHandler.Instance.InMenu();

            battleUI.alpha = 1;

            //skillTreeView.Show();

        }

        public void ChangeCustomeElementState(bool value)
        {
            for (int i = 0; i < customElements.Length; i++)
            {
                customElements[i].enabled = value;
                customElements[i]._OnDeselect.PlayFeedbacks();
            }

            if (value)
            {
                if (eventSytem == null) return;
                eventSytem.SetSelectedGameObject(firstSelectedObject);
                customElements[2]._OnSelect.PlayFeedbacks();
            }

            
        }

        private void SetExclamation()
        {
            for (int i = 0; i < battleActorDatas.Length; i++)
            {
                if (battleActorDatas[i].hasNewSkills)
                {
                    exclamation.SetActive(true);
                    break;
                }
                else
                {
                    exclamation.SetActive(false);
                }
            }
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
            //background.SetActive(true);
            initiativeView.gameObject.SetActive(true);
            initiativeView.isActive = false;

            //if (VolumesManager.Instance != null)
            //    VolumesManager.Instance.BlurryUI.enabled = false;

            eventSytem.SetSelectedGameObject(null);
            background.SetActive(false);
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

            for (int i = 0; i < battleActorDatas.Length; i++)
            {
                if (battleActorDatas[i].hasNewSkills)
                {
                    battleActorDatas[i].hasNewSkills = false;
                }

            }
        }

        private void Update()
        {


            if(isActive && onMenu)
            {
                if (Inputs.actions["Return"].WasPressedThisFrame())
                {
                    if (!onConfirmation)
                    {
                        OpenConfirmationPanel();
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
                            if (!isBattleLaunched)
                            {
                                isBattleLaunched = true;
                                StartCoroutine(WaitLaunchBattle());
                            }

                        }

                    }
                }
            }
        }

        public void OpenConfirmationPanel()
        {

            SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_Confirm, gameObject);


            confirmation.SetActive(true);
            background.SetActive(false);

            initiativeView.gameObject.SetActive(false);

            InfoBoxButtons[] infoButtons = new InfoBoxButtons[2];
            infoButtons[0] = InfoBoxButtons.SELECT;
            infoButtons[1] = InfoBoxButtons.BACK;

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);

            StartCoroutine(WaitConfirmation());
        }

        private IEnumerator WaitConfirmation()
        {
            yield return new WaitForEndOfFrame();

            onConfirmation = true;
            yield return new WaitForEndOfFrame();
            onConfirmation = true;

            yield break;
        }

        public void LaunchBattle()
        {
            StartCoroutine(WaitLaunchBattle());
        }

        private IEnumerator WaitLaunchBattle()
        {
            BlackPanel.Instance.Show();
            
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            battle.PlayPhase();

            yield return new WaitForSeconds(BlackPanel.Instance.duration + .1f);
            BlackPanel.Instance.Hide(2.25f);
            
            Hide();

            isBattleLaunched = false;
        }

        private void OnApplicationFocus(bool focus)
        {
            if(isActive && focus)
            {
                Debug.Log("OnApplicationFocus");
                eventSytem.SetSelectedGameObject(firstSelectedObject);
                
            }

        }
    }
}

