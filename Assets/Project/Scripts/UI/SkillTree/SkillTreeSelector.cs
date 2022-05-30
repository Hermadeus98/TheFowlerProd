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
        [SerializeField] public Palier palier;

        [SerializeField] public Spell associatedSpell;

        [SerializeField] private SkillTreeSelector[] linkedSkills;

        [SerializeField] private BattleActorData associatedData;

        [SerializeField] private SkillState skillState;

        [SerializeField] private GameObject equipped, unequipped, locked;
        [SerializeField] private GameObject hover, unHover;

        [SerializeField] private Color equippedColor, lockedColor, unselectionnedColor;

        [SerializeField] private UnityEngine.InputSystem.PlayerInput Inputs;

        [SerializeField] private RectTransform rect;

        public RectTransform Rect => rect;
        [SerializeField] private SkillTreeView view;

        [SerializeField] private Image[] lines;

        private Spell[] spellReminder;

        private bool canInteract = false;
        private bool isHover;

        public SkillLinks[] links;

        public List<SkillTreeSelector> previousSelector;
    
    

        protected override void Awake()
        {
            previousSelector.Clear();
            for (int i = 0; i < links.Length; i++)
            {
                links[i].linkedSelector.previousSelector.Add(this);
            }
        }
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
            ChangeOutline(true);
            RefreshLines();
            //SetLines(true);
        }

        public void _Deselect()
        {
            hover.SetActive(false);
            unHover.SetActive(true);
            isHover = false;
            ChangeOutline(false);
            //SetLines(false);
        }

        private void Update()
        {
            if (canInteract && isHover && Inputs.actions["Select"].WasPressedThisFrame())
            {
                for (int i = 0; i < previousSelector.Count; i++)
                {
                    if (previousSelector[i] == view.skillsWay[view.skillsWay.Count - 1])
                    {
                        Equip();
                        break;
                    }
                }
                

            }
        }

        public void Equip()
        {

            for (int i = 0; i < linkedSkills.Length; i++)
            {
                if(linkedSkills[i].skillState == SkillState.EQUIPPED)
                {
                    return;
                }
                else
                {
                    linkedSkills[i].FeedbackUnselectionned();
                }
                
            }

            if(palier == Palier.LITTLE)
            {

                for (int i = 0; i < view.mediumSkills.Length; i++)
                {
                    for (int j = 0; j < links.Length; j++)
                    {
                        if (view.mediumSkills[i] != links[j].linkedSelector)
                        {
                            view.mediumSkills[i].FeedbackUnselectionned();
                        }
                        else
                        {
                            view.mediumSkills[i].FeedbackUnEquipped();
                            break;
                        }
                    }

                }
            }
            else if (palier == Palier.MEDIUM)
            {
                for (int i = 0; i < view.bigSkills.Length; i++)
                {
                    for (int j = 0; j < links.Length; j++)
                    {
                        if (view.bigSkills[i] != links[j].linkedSelector)
                        {
                            view.bigSkills[i].FeedbackUnselectionned();
                        }

                        else
                        {
                            view.bigSkills[i].FeedbackUnEquipped();
                            break;
                        }
                    }

                }

            }


            FeedbackEquipped();


            SetSpellArray();

            view.SetSpells();
            CheckSpells();

            view.RefreshAllLines();

            view.RefreshSkillsWay();

            view.RefreshSkillPoint();
            view.RefreshCircles();



          

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
                case SkillState.UNSELECTIONNED:
                    FeedbackUnselectionned();
                    break;
            }


        }

        private void ChangeOutline(bool value)
        {
            for (int i = 0; i < links.Length; i++)
            {
                links[i].lineBehavior.EnableOutline(value);
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
            switch (skillState)
            {
                case SkillState.BASIC:
                    image.sprite = associatedSpell.sprite;
                    break;
                case SkillState.EQUIPPED:
                    image.sprite = associatedSpell.sprite;
                    break;
                case SkillState.LOCKED:
                    image.sprite = associatedSpell.spriteBlocked;
                    break;
                case SkillState.UNEQUIPPED:
                    image.sprite = associatedSpell.spriteBlocked;
                    break;
                    

            }
        }

        private void FeedbackUnselectionned()
        {


            skillState = SkillState.UNSELECTIONNED;

            image.sprite = associatedSpell.spriteBlocked;
            image.color = unselectionnedColor;

            canInteract = false;

            associatedSpell.spellState = SkillState.UNSELECTIONNED;

            InfoBoxButtons[] infoButtons = new InfoBoxButtons[1];
            infoButtons[0] = InfoBoxButtons.BACK;
            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);



        }

        private void FeedbackEquipped()
        {


            skillState = SkillState.EQUIPPED;

            equipped.SetActive(true);
            unequipped.SetActive(false);
            locked.SetActive(false);

            image.sprite = associatedSpell.sprite;
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

            image.sprite = associatedSpell.spriteBlocked;
            image.color = equippedColor;

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

            image.sprite = associatedSpell.spriteBlocked;
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



        public void RefreshLines()
        {
            for (int i = 0; i < links.Length; i++)
            {
                switch (links[i].linkedSelector.skillState)
                {
                    case SkillState.EQUIPPED:
                        if(skillState == SkillState.EQUIPPED || skillState == SkillState.BASIC)
                        {
                            links[i].lineBehavior.ToSelected();
                        }
                        else
                        {
                            links[i].lineBehavior.ToUnSelected();
                            
                        }

                        break;
                    case SkillState.UNEQUIPPED:
                        links[i].lineBehavior.ToUnSelected();
                        break;
                    case SkillState.LOCKED:
                        links[i].lineBehavior.ToDisable();
                        break;
                    case SkillState.UNSELECTIONNED:
                        links[i].lineBehavior.ToDisable();
                        break;
                }
            

            }



        }


    }



    [System.Serializable]
    public struct SkillLinks
    {
        public LineBehavior lineBehavior;
        public SkillTreeSelector linkedSelector;
    }
    [System.Serializable]
    public enum Palier
    {
        LITTLE,
        MEDIUM,
        BIG
    }

}

