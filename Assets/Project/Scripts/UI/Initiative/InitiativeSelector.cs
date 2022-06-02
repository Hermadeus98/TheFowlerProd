using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace TheFowler
{
    public class InitiativeSelector : CustomElement
    {
        [SerializeField] private Image glow;
        [SerializeField] private InitiativeView view;
        public BattleActorData associatedData;
        [SerializeField] private Transform parent;
        [SerializeField] private Image portrait, lifeFilled;
        [SerializeField] private GameObject arrow;
        [SerializeField] private TMPro.TextMeshProUGUI lifeTxt;
        
        public bool isHover;


        public override void OnSelect(BaseEventData eventData)
        {

            if (view.isSelecting) return;
            base.OnSelect(eventData);


            glow.color = new Color(glow.color.r, glow.color.g, glow.color.b, 1);
            isHover = true;
            arrow.SetActive(true);


        }

        public override void OnDeselect(BaseEventData eventData)
        {
            if (view.isSelecting) return;
            base.OnDeselect(eventData);
            glow.color = new Color(glow.color.r, glow.color.g, glow.color.b, 0);
            isHover = false;
            arrow.SetActive(false);
        }

        public void _Deselect()
        {
            glow.color = new Color(glow.color.r, glow.color.g, glow.color.b, 0);
            isHover = false;
           
        }

        public void Pick()
        {
            portrait.color = Color.white;
            arrow.SetActive(true);
            portrait.rectTransform.DOAnchorPos(new Vector2(portrait.rectTransform.anchoredPosition.x - 50, 0), .2f);
        }

        public void UnPick()
        {
            portrait.color = Color.grey;
            //arrow.SetActive(false);
            portrait.rectTransform.DOAnchorPos(new Vector2(0, 0), .2f);
        }

        private void SetLife()
        {
            switch (associatedData.actorName)
            {
                case "Robyn":
                    lifeFilled.fillAmount = Player.RobynSavedData.health / 100;
                    lifeTxt.text = Player.RobynSavedData.health.ToString() + "/100";
                    break;
                case "Abigael":
                    lifeFilled.fillAmount = Player.AbiSavedData.health / 100;
                    lifeTxt.text = Player.AbiSavedData.health.ToString() + "/100";
                    break;
                case "Phoebe":
                    lifeFilled.fillAmount = Player.PhoebeSavedData.health / 100;
                    lifeTxt.text = Player.PhoebeSavedData.health.ToString() + "/100";
                    break;

            }
        }

        public void InitialState()
        {
            associatedData.initiativeOrder = transform.GetSiblingIndex() + 1;

            switch (associatedData.actorName)
            {
                case "Robyn":
                    Player.RobynSavedData.initiative = transform.GetSiblingIndex() + 1;
                    break;
                case "Abigail":
                    Player.AbiSavedData.initiative = transform.GetSiblingIndex() + 1;
                    break;
                case "Phoebe":
                    Player.PhoebeSavedData.initiative = transform.GetSiblingIndex() + 1;
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
            //arrow.SetActive(false);
            portrait.rectTransform.DOAnchorPos(new Vector2(0, 0), .2f);
            // SetLife();
            view.SetSKActorInSpace();
        }

        public void InitializeMenu()
        {
            Navigation customNav = new Navigation();
            customNav.mode = Navigation.Mode.Explicit;
            navigation = customNav;
        }



    }
}

