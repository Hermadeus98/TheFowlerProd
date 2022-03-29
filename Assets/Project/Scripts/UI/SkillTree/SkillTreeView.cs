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
        [TabGroup("References")] [SerializeField] private int complicityLevelRobyn, complicityLevelAbi, complicityLevelPhoebe;
        [TabGroup("Tree References")] [SerializeField] private TMPro.TextMeshProUGUI spellName, spellDescription;

        [TabGroup("Spell References") ]
        [SerializeField] private TMPro.TextMeshProUGUI descriptionText, easyDescriptionText, targetText;

        [TabGroup("Spell References")]
        [Title("Robyn")]
        [SerializeField] private SkillSelectorElement[] skillSelectorsRobyn;
        [TabGroup("Spell References")]
        [SerializeField] private SkillTreeSelector[] skillTreeSelectorsRobyn;

        [TabGroup("Spell References")]
        [Title("Abi")]
        [SerializeField] private SkillSelectorElement[] skillSelectorsAbi;
        [TabGroup("Spell References")]
        [SerializeField] private SkillTreeSelector[] skillTreeSelectorsAbi;

        [TabGroup("Spell References")]
        [Title("Phoebe")]
        [SerializeField] private SkillSelectorElement[] skillSelectorsPhoebe;
        [TabGroup("Spell References")]
        [SerializeField] private SkillTreeSelector[] skillTreeSelectorsPhoebe;

        private GameObject eventSytemGO;
        private UnityEngine.EventSystems.EventSystem eventSytem;
        private Spell currentSpell;
        private BattleActorData currentBattleActorData;
        public CustomElement currentCustomElement;

        public override void Show()
        {
            base.Show();

            InfoBoxButtons[] infoButtons = new InfoBoxButtons[1];
            infoButtons[0] = InfoBoxButtons.BACK;

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);


            if (VolumesManager.Instance != null)
                VolumesManager.Instance.BlurryUI.enabled = true;

            if (eventSytemGO == null)
            {

                eventSytemGO = GameObject.Find("EventSystem");
                eventSytem = eventSytemGO.GetComponent<EventSystem>();

            }
            eventSytem.SetSelectedGameObject(firstSelectedObject);
        }

        public override void Hide()
        {
            base.Hide();

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).Hide();

            if (VolumesManager.Instance != null)
                VolumesManager.Instance.BlurryUI.enabled = false;
        }

        public void ShowTreeSkillData(Spell spell)
        {
            descriptionText.SetText(spell.SpellDescription);
            easyDescriptionText.SetText(spell.EasySpellDescription.ToString());
            targetText.SetText(spell.TargetDescription);

            currentSpell = spell;

            if (currentCustomElement.isClickable)
            {
                InfoBoxButtons[] infoButtons = new InfoBoxButtons[2];
                infoButtons[0] = InfoBoxButtons.CONFIRM;
                infoButtons[1] = InfoBoxButtons.BACK;

                UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);
            }
            else
            {
                InfoBoxButtons[] infoButtons = new InfoBoxButtons[1];
                infoButtons[0] = InfoBoxButtons.BACK;

                UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);
            }

        }

        public void ShowTreeSkillData(SkillSelectorElement skillSelector)
        {
            descriptionText.SetText(skillSelector.referedSpell.SpellDescription);
            easyDescriptionText.SetText(skillSelector.referedSpell.EasySpellDescription.ToString());
            targetText.SetText(skillSelector.referedSpell.TargetDescription);

            InfoBoxButtons[] infoButtons = new InfoBoxButtons[1];
            infoButtons[0] = InfoBoxButtons.BACK;

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);
        }

        public void ChangeDataCharacter(int ID)
        {
            if (currentBattleActorData.complicityLevel < ID)
            {
                // Insérer feedback négatifs lorsqu'on clique sur un sort qui n'est pas encore débloqué.

                return;
            }


            // Insérer feedback positifs car le click est réussi
            if (ID > currentBattleActorData.Spells.Length -1)
            {
                Spell[] reminder = new Spell[currentBattleActorData.Spells.Length];
                
                for (int i = 0; i < reminder.Length; i++)
                {
                    reminder[i] = currentBattleActorData.Spells[i];
                }

                currentBattleActorData.Spells = new Spell[ID + 1];

                for (int i = 0; i < reminder.Length; i++)
                {
                    currentBattleActorData.Spells[i] = reminder[i];
                }
                
            }


            currentBattleActorData.Spells[ID] = currentSpell;
            ChangeSkillSelector(currentBattleActorData);

        }

        private void CheckComplicityLevel(SkillTreeSelector[] skillTreeSelector)
        {
            for (int j = 0; j < skillTreeSelector.Length; j++)
            {
                if (skillTreeSelector[j].isPassive)
                {
                    if(skillTreeSelector[j].complicityLevel <= currentBattleActorData.complicityLevel)
                    {
                        skillTreeSelector[j].SetPicken();
                    }
                    else
                    {
                        skillTreeSelector[j].SetDisable();
                    }

                    
                }
                else
                {
                    for (int i = 0; i < currentBattleActorData.Spells.Length; i++)
                    {


                        if (currentBattleActorData.Spells[i] == skillTreeSelector[j].linkedSpell)
                        {
                            skillTreeSelector[j].SetPicken();
                            break;
                        }
                        else
                        {
                            if (skillTreeSelector[j].complicityLevel <= currentBattleActorData.complicityLevel)
                            {
                                skillTreeSelector[j].SetUnactive();
                            }
                            else
                            {
                                skillTreeSelector[j].SetDisable();
                            }
                        }
                    }

                }


            }

            
        }



        public void ChangeSkillSelector(BattleActorData data)
        {
            InfoBoxButtons[] infoButtons = new InfoBoxButtons[1];
            infoButtons[0] = InfoBoxButtons.BACK;

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).ShowProfile(infoButtons);

            ChangeSkill(skillSelectorsRobyn, data);
            ChangeSkill(skillSelectorsAbi, data);
            ChangeSkill(skillSelectorsPhoebe, data);

            CheckComplicityLevel(skillTreeSelectorsRobyn);
            CheckComplicityLevel(skillTreeSelectorsAbi);
            CheckComplicityLevel(skillTreeSelectorsPhoebe);

            switch (data.actorName)
            {
                case "Robyn":
                    complicityLevelRobyn = FeedbackNewComplicity(complicityLevelRobyn, skillTreeSelectorsRobyn);
                    break;
                case "Phoebe":
                    complicityLevelPhoebe = FeedbackNewComplicity(complicityLevelPhoebe, skillTreeSelectorsPhoebe);
                    break;
                case "Abigail":
                    complicityLevelAbi = FeedbackNewComplicity(complicityLevelAbi, skillTreeSelectorsAbi);
                    break;
            }

            
        }

        private void ChangeSkill(SkillSelectorElement[] elements, BattleActorData data)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (i <= data.Spells.Length - 1)
                {
                    elements[i].gameObject.SetActive(true);
                    elements[i].transform.parent.GetComponent<CustomElement>().enabled = true;
                    elements[i].Refresh(data.Spells[i]);
                }
                else
                {
                    elements[i].transform.parent.GetComponent<CustomElement>().enabled = false;
                    elements[i].gameObject.SetActive(false);

                }

            }

            currentBattleActorData = data;
        }

        private int FeedbackNewComplicity(int complicityLevel, SkillTreeSelector[] skillTreeSelectors)
        {
            for (int i = 0; i < skillTreeSelectors.Length; i++)
            {
                if (skillTreeSelectors[i].complicityLevel > complicityLevel && skillTreeSelectors[i].complicityLevel <= currentBattleActorData.complicityLevel)
                {
                    skillTreeSelectors[i].UnlockFeedback.PlayFeedbacks();
                }
            }

            return currentBattleActorData.complicityLevel;
        }


    }

}
