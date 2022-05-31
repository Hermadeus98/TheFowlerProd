using System;
using System.Linq;
using QRCode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class InfoBoxView : UIView
    {
        [TabGroup("References")]
        [SerializeField] private GameObject profile1, profile2, profile3, profileUp;
        [TabGroup("References")]
        [SerializeField] private GameObject confirm, back, characters, selectSkill, selectTarget, hide, select, close, reset;
        private delegate void ProfileReminder(InfoBoxButtons[] buttons);
        private event ProfileReminder OnProfileReminder;
        [SerializeField] private List<InfoBoxButtons> infoBoxReminder;
        [SerializeField] private PlayerInput Inputs;

        private bool isOpen = true;

        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);
        }

        public override void Show()
        {
            base.Show();

            var actionPicking = UI.GetView<ActionPickingView>(UI.Views.ActionPicking);
            actionPicking.ShowDescription(true);

            var skillPicking = UI.GetView<SkillPickingView>(UI.Views.SkillPicking);
            skillPicking.ShowTriangle(true);
        }

        public override void Hide()
        {
            base.Hide();
            CloseProfiles();
            CloseButtons();
            DeleteAllEvents();
        }

        public void ShowProfile(InfoBoxButtons[] buttons)
        {
            Show();

            infoBoxReminder.Clear();
            for (int i = 0; i < buttons.Length; i++)
            {
                infoBoxReminder.Add(buttons[i]);
            }

            DeleteAllEvents();
            
            OnProfileReminder += ShowProfile;

            CloseProfiles();
            CloseButtons();

            switch (buttons.Length)
            {
                case 1:
                    profile1.SetActive(true);
                    break;
                case 2:
                    profile2.SetActive(true);
                    break;
                case 3:
                    profile3.SetActive(true);
                    break;
            }

            for (int i = 0; i < buttons.Length; i++)
            {
                SwitchButtons(buttons[i]);
            }

            if (!isOpen)
            {
                ShowProfilesUp();

                var actionPicking = UI.GetView<ActionPickingView>(UI.Views.ActionPicking);
                actionPicking.ShowDescription(false);

                var skillPicking = UI.GetView<SkillPickingView>(UI.Views.SkillPicking);
                skillPicking.ShowTriangle(false);
            }



        }


        private void CloseProfiles()
        {
            profile1.SetActive(false);
            profile2.SetActive(false);
            profile3.SetActive(false);
            profileUp.SetActive(false);
        }
        private void CloseButtons()
        {
            confirm.SetActive(false);
            back.SetActive(false);
            characters.SetActive(false);
            selectSkill.SetActive(false);
            selectTarget.SetActive(false);
            hide.SetActive(false);
            select.SetActive(false);
            close.SetActive(false);
            reset.SetActive(false);
        }

        private void SwitchButtons(InfoBoxButtons button)
        {
            switch (button)
            {
                case InfoBoxButtons.CONFIRM:
                    confirm.SetActive(true);
                    break;
                case InfoBoxButtons.BACK:
                    back.SetActive(true);
                    break;
                case InfoBoxButtons.CHARACTERS:
                    characters.SetActive(true);
                    break;
                case InfoBoxButtons.SELECTSKILL:
                    selectSkill.SetActive(true);
                    break;
                case InfoBoxButtons.SELECTTARGET:
                    selectTarget.SetActive(true);
                    break;
                case InfoBoxButtons.HIDE:
                    hide.SetActive(true);
                    break;
                case InfoBoxButtons.SELECT:
                    select.SetActive(true);
                    break;
                case InfoBoxButtons.CLOSE:
                    close.SetActive(true);
                    break;
                case InfoBoxButtons.RESET:
                    reset.SetActive(true);
                    break;
            }
        }

        public void ShowProfilesUp()
        {
            CloseProfiles();
            CloseButtons();

            profileUp.SetActive(true);
        }

        public void CheckInfoBox()
        {
            if(profileUp.activeSelf == true)
            {
                if(OnProfileReminder != null)
                {
                    isOpen = true;

                    OnProfileReminder.Invoke(infoBoxReminder.ToArray());
                    
                    var actionPicking = UI.GetView<ActionPickingView>(UI.Views.ActionPicking);
                    actionPicking.ShowDescription(true);

                    var skillPicking = UI.GetView<SkillPickingView>(UI.Views.SkillPicking);
                    skillPicking.ShowTriangle(true);

                }

            }
            else
            {
                ShowProfilesUp();
                
                var actionPicking = UI.GetView<ActionPickingView>(UI.Views.ActionPicking);
                actionPicking.ShowDescription(false);

                var skillPicking = UI.GetView<SkillPickingView>(UI.Views.SkillPicking);
                skillPicking.ShowTriangle(false);
                isOpen = false;
            }
        }

        private void DeleteAllEvents()
        {
            if (OnProfileReminder != null)
            {
                System.Delegate[] deletegates = OnProfileReminder.GetInvocationList();
                for (int i = 0; i < deletegates.Length; i++)
                {
                    //Remove all event
                    OnProfileReminder -= deletegates[i] as ProfileReminder;
                }
            }
        }

        private void Update()
        {
            if (isActive)
            {
                //if (Inputs.actions["InfoBox"].WasPressedThisFrame())
                //{
                //    CheckInfoBox();
                //}
            }

        }

    }

    public enum InfoBoxButtons
    {
        CONFIRM,
        BACK,
        CHARACTERS,
        SELECTSKILL,
        SELECTTARGET,
        HIDE,
        SELECT,
        CLOSE,
        RESET
    }

}
