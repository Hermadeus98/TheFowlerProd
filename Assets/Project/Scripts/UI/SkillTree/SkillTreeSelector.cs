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

        private Spell[] spellReminder;
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);

            hover.SetActive(true);
            unHover.SetActive(false);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            hover.SetActive(false);
            unHover.SetActive(true);
        }

        public void Equip()
        {
            skillState = SkillState.EQUIPPED;

            FeedbackEquipped();


            SetSpellArray();


            if (associatedSkill == null) return;

            associatedSkill.UnEquip();
        }

        public void UnEquip()
        {
            skillState = SkillState.UNEQUIPPED;

            FeedbackUnEquipped();
        }

        public void SetState()
        {
            switch (skillState)
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

        private void FeedbackEquipped()
        {
            equipped.SetActive(true);
            unequipped.SetActive(false);
            locked.SetActive(false);

            image.color = equippedColor;

        }

        private void FeedbackUnEquipped()
        {
            equipped.SetActive(false);
            unequipped.SetActive(true);
            locked.SetActive(false);

            image.color = lockedColor;
        }

        private void FeedbackLocked()
        {
            equipped.SetActive(false);
            unequipped.SetActive(false);
            locked.SetActive(true);

            image.color = lockedColor;
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

                for (int i = 0; i < associatedData.Spells.Length; i++)
                {
                    associatedData.Spells[i] = spellReminder[i];
                }
            }

            associatedData.Spells[associatedSpell.unlockOrder] = associatedSpell;
        }

    }

}

