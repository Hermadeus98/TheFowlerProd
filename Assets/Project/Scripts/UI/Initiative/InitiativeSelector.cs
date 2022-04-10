using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TheFowler
{
    public class InitiativeSelector : CustomElement
    {
        [SerializeField] private Outline outline;
        [SerializeField] private InitiativeView view;
        public BattleActorData associatedData;
        [SerializeField] private Transform parent;
        [SerializeField] private Image portrait;
        [SerializeField] private GameObject arrow;
        
        public bool isHover;

        
        public override void OnSelect(BaseEventData eventData)
        {

            if (view.isSelecting) return;
            base.OnSelect(eventData);


            outline.enabled = true;
            isHover = true;

        }

        public override void OnDeselect(BaseEventData eventData)
        {
            if (view.isSelecting) return;
            base.OnDeselect(eventData);
            outline.enabled = false;
            isHover = false;
        }

        public void _Deselect()
        {
            outline.enabled = false;
            isHover = false;
        }

        public void Pick()
        {
            portrait.color = Color.white;
            arrow.SetActive(true);
        }

        public void UnPick()
        {
            portrait.color = Color.grey;
            arrow.SetActive(false);
        }

        public void InitialState()
        {
            associatedData.initiativeOrder = transform.GetSiblingIndex() + 1;

            switch (associatedData.actorName)
            {
                case "Robyn":
                    Player.RobynSavedData.initiative = associatedData.initiativeOrder;
                    break;
                case "Abigael":
                    Player.AbiSavedData.initiative = associatedData.initiativeOrder;
                    break;
                case "Phoebe":
                    Player.PhoebeSavedData.initiative = associatedData.initiativeOrder;
                    break;

            }

            Navigation customNav = new Navigation();
            customNav.mode = Navigation.Mode.Explicit;

            switch (transform.GetSiblingIndex())
            {
                case 0:
                    customNav.selectOnDown = parent.GetChild(1).GetComponent<InitiativeSelector>();
                    break;
                case 1:
                    customNav.selectOnUp = parent.GetChild(0).GetComponent<InitiativeSelector>();
                    customNav.selectOnDown = parent.GetChild(2).GetComponent<InitiativeSelector>();
                    break;
                case 2:
                    customNav.selectOnUp = parent.GetChild(1).GetComponent<InitiativeSelector>();
                    break;
            }

            navigation = customNav;

            portrait.color = Color.white;
            arrow.SetActive(false);
        }

        public void InitializeMenu()
        {
            Navigation customNav = new Navigation();
            customNav.mode = Navigation.Mode.Explicit;
            navigation = customNav;
        }


    }
}

