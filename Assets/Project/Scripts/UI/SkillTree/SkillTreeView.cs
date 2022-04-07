using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using QRCode;
namespace TheFowler
{
    public class SkillTreeView : UIView
    {
        [TabGroup("References")] [SerializeField] private GameObject  firstSelectedObject;
        [TabGroup("References")] [SerializeField] private UnityEngine.InputSystem.PlayerInput Inputs;
        [TabGroup("References")] [SerializeField] private SkillTreeSelector[] skills;

        private GameObject eventSytemGO;
        private UnityEngine.EventSystems.EventSystem eventSytem;


        public override void Show()
        {
            base.Show();

            InfoBoxButtons[] infoButtons = new InfoBoxButtons[1];
            infoButtons[0] = InfoBoxButtons.CLOSE;

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);


            if (VolumesManager.Instance != null)
                VolumesManager.Instance.BlurryUI.enabled = true;

            if (eventSytemGO == null)
            {

                eventSytemGO = GameObject.Find("EventSystem");
                eventSytem = eventSytemGO.GetComponent<EventSystem>();

            }
            eventSytem.SetSelectedGameObject(firstSelectedObject);

            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].SetState();
            }

        }


        public override void Hide()
        {
            base.Hide();

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).Hide();

            if (VolumesManager.Instance != null)
                VolumesManager.Instance.BlurryUI.enabled = false;

        }


    }

}
