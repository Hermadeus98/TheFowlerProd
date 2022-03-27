using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace TheFowler
{
    public class SkillTreeView : UIView
    {
        [TabGroup("References")] [SerializeField] private GameObject  firstSelectedObject;
        [TabGroup("Tree References")] [SerializeField] private TMPro.TextMeshProUGUI spellName, spellDescription;

        private GameObject eventSytemGO;
        private UnityEngine.EventSystems.EventSystem eventSytem;

        public override void Show()
        {
            base.Show();

            //if (VolumesManager.Instance != null)
            //    VolumesManager.Instance.BlurryUI.enabled = true;

            if(eventSytemGO == null)
            {

                eventSytemGO = GameObject.Find("EventSystem");
                eventSytem = eventSytemGO.GetComponent<EventSystem>();

            }
            eventSytem.SetSelectedGameObject(firstSelectedObject);
        }

        public override void Hide()
        {
            base.Hide();

            //if(VolumesManager.Instance != null)
            //    VolumesManager.Instance.BlurryUI.enabled = false;
        }

        public void ShowTreeSkillData(Spell spell)
        {
            spellName.text = spell.SpellName;
            spellDescription.text = spell.SpellDescription;
        }


    }

}
