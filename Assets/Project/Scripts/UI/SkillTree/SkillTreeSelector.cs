using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.Collections;

namespace TheFowler
{
    public class SkillTreeSelector : CustomElement
    {
        [SerializeField] private Spell associatedSpell;

        [SerializeField] private SkillTreeSelector associatedSkill;

        [SerializeField] private BattleActorData associatedData;

        [SerializeField] private SkillState skillState;
        
        [SerializeField] private GameObject equipped, unequipped, locked;
        [SerializeField] private GameObject hover, unHover;

        [SerializeField] private Color equippedColor, lockedColor;

        [SerializeField] private UnityEngine.InputSystem.PlayerInput Inputs;

        [SerializeField] private RectTransform rect;

        public RectTransform Rect => rect;
        [SerializeField] private SkillTreeView view;

        [SerializeField] private Image[] lines;

        private Spell[] spellReminder;

        private bool canInteract = false;
        private bool isHover;
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            _Select();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            _Deselect();


        }

        public void _Select()
        {
            hover.SetActive(true);
            unHover.SetActive(false);
            isHover = true;
            SetState();
            view.SetDescription(this, associatedSpell);
            CheckSpells();
            //SetLines(true);
        }

        public void _Deselect()
        {
            hover.SetActive(false);
            unHover.SetActive(true);
            isHover = false;
            //SetLines(false);
        }

        private void Update()
        {
            if(canInteract && isHover && Inputs.actions["Select"].WasPressedThisFrame())
            {
                Equip();
            }
        }

        public void Equip()
        {
            if (associatedSkill == null) return;

            associatedSkill.UnEquip();

            FeedbackEquipped();


            SetSpellArray();

            view.SetSpells();
            CheckSpells();

        }


        public void UnEquip()
        {
            FeedbackUnEquipped();
        }

        public void SetLines(bool value)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (value)
                {
                    lines[i].color = Color.white;
                }
                else
                {
                    lines[i].color = Color.grey;
                }

            }
        }

        public void SetState()
        {
            switch (associatedSpell.spellState)
            {
                case SkillState.BASIC:
                    FeedbackEquipped();
                    break;
                case SkillState.EQUIPPED:
                    FeedbackEquipped();
                    break;
                case SkillState.UNEQUIPPED:
                    FeedbackUnEquipped();
                    break;
                case SkillState.LOCKED:
                    FeedbackLocked();
                    break;
            }


        }

        public BattleActorData Data
        {
            get
            {
                return associatedData;
            }
            set
            {
                associatedData = value;
            }
        }

        public void SetDatas(int ID)
        {

            associatedSpell = associatedData.AllSpells[ID];
            skillState = associatedSpell.spellState;
            image.sprite = associatedSpell.sprite;
        }

        private void FeedbackEquipped()
        {


            skillState = SkillState.EQUIPPED;

            equipped.SetActive(true);
            unequipped.SetActive(false);
            locked.SetActive(false);

            image.color = equippedColor;

            canInteract = false;

            associatedSpell.spellState = SkillState.EQUIPPED;

            InfoBoxButtons[] infoButtons = new InfoBoxButtons[1];
            infoButtons[0] = InfoBoxButtons.BACK;
            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);



        }

        private void FeedbackUnEquipped()
        {

            skillState = SkillState.UNEQUIPPED;


            equipped.SetActive(false);
            unequipped.SetActive(true);
            locked.SetActive(false);

            image.color = lockedColor;

            canInteract = true;

            associatedSpell.spellState = SkillState.UNEQUIPPED;

            InfoBoxButtons[] infoButtons = new InfoBoxButtons[2];
            infoButtons[0] = InfoBoxButtons.CONFIRM;
            infoButtons[1] = InfoBoxButtons.BACK;
            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);
        }

        private void FeedbackLocked()
        {
            skillState = SkillState.LOCKED;

            equipped.SetActive(false);
            unequipped.SetActive(false);
            locked.SetActive(true);

            image.color = lockedColor;
            canInteract = false;

            associatedSpell.spellState = SkillState.LOCKED;

            InfoBoxButtons[] infoButtons = new InfoBoxButtons[1];
            infoButtons[0] = InfoBoxButtons.BACK;
            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);

        }

        private void CheckSpells()
        {
            for (int i = 0; i < view.SpellTreeSelectors.Length; i++)
            {
                view.SpellTreeSelectors[i].SetUnHover();
                
            }
            for (int i = 0; i < view.SpellTreeSelectors.Length; i++)
            {
                if (view.SpellTreeSelectors[i].associatedSpell == associatedSpell)
                {
                    view.SpellTreeSelectors[i].SetHover();
                    
                }

            }



        }

        private void SetSpellArray()
        {
            if (associatedData.Spells.Length <= associatedSpell.unlockOrder)
            {
                spellReminder = new Spell[associatedData.Spells.Length];

                for (int i = 0; i < spellReminder.Length; i++)
                {
                    spellReminder[i] = associatedData.Spells[i];
                }

                associatedData.Spells = new Spell[associatedData.Spells.Length + 1];

                for (int i = 0; i < spellReminder.Length; i++)
                {
                    associatedData.Spells[i] = spellReminder[i];
                }
            }

            associatedData.Spells[associatedSpell.unlockOrder] = associatedSpell;
        }

    }



}

