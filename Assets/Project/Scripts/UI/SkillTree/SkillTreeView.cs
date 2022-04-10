using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using QRCode;
using DG.Tweening;
namespace TheFowler
{
    public class SkillTreeView : UIView
    {
        [TabGroup("References")] [SerializeField] private SkillTreeSelector firstSelectedObject;
        [TabGroup("References")] [SerializeField] private UnityEngine.InputSystem.PlayerInput Inputs;
        [TabGroup("Tree")] [SerializeField] private SkillTreeSelector[] skills;
        [TabGroup("Tree")] [SerializeField] private BattleActorData[] datas;
        [TabGroup("Tree")] private BattleActorData currentData;
        [TabGroup("Tree")] [SerializeField] private RectTransform descriptionBox;
        [TabGroup("Tree")] [SerializeField] private TMPro.TextMeshProUGUI descriptionName, descriptionText;
        [TabGroup("Tree")] [SerializeField] private Image targetImage, typeImage;
        [TabGroup("Tree")] [SerializeField] private Image[] hearts;
        [TabGroup("Tree")] [SerializeField] private Sprite heartEmpty, heartFilled;
        [TabGroup("Tree")] [SerializeField] private SpellTypeDatabase spellTypeDatabase;
        [TabGroup("Tree")] [SerializeField] private TargetTypeDatabase targetTypeDatabase;


        [TabGroup("Character")] [SerializeField] private Image character;
        [TabGroup("Character")] [SerializeField] private TMPro.TextMeshProUGUI characterName;
        [TabGroup("Character")] [SerializeField] private Sprite[] characterSprites;

        [TabGroup("Spell")] [SerializeField] private SpellTreeSelector[] spellTreeSelectors;

        [SerializeField] private MenuCharactersView menuView;

        public SpellTreeSelector[] SpellTreeSelectors
        {
            get
            {
                return spellTreeSelectors;
            }
            set
            {
                spellTreeSelectors = value;
            }
        }

        private GameObject eventSytemGO;
        private UnityEngine.EventSystems.EventSystem eventSytem;
        private int ID;

        public override void Show()
        {
            base.Show();

            for (int i = 0; i < skills.Length; i++)
            {
                skills[i]._Deselect();
            }

            CharacterPicker(0);

            SetCharacter();

            SetSpells();

            if (VolumesManager.Instance != null)
                VolumesManager.Instance.BlurryUI.enabled = true;

            if (eventSytemGO == null)
            {

                eventSytemGO = GameObject.Find("EventSystem");
                eventSytem = eventSytemGO.GetComponent<EventSystem>();

            }
            eventSytem.SetSelectedGameObject(firstSelectedObject.gameObject);

            menuView.onMenu = false;

        }


        public override void Hide()
        {
            base.Hide();

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).Hide();

            if (VolumesManager.Instance != null)
                VolumesManager.Instance.BlurryUI.enabled = false;

     
            if(menuView != null)
            {
                menuView.Show();
            }

        }

        private void Update()
        {
            if (isActive)
            {
                if (Inputs.actions["RightBumper"].WasPressedThisFrame())
                {
                    ID++;
                    if (ID == 3) ID = 0;

                    for (int i = 0; i < skills.Length; i++)
                    {
                        skills[i]._Deselect();
                    }

                    CharacterPicker(ID);
                    SetCharacter();
                    SetSpells();

                    firstSelectedObject._Select();
                    eventSytem.SetSelectedGameObject(firstSelectedObject.gameObject);




                }
                else if (Inputs.actions["LeftBumper"].WasPressedThisFrame())
                {
                    ID--;
                    if (ID == -1) ID = 2;

                    for (int i = 0; i < skills.Length; i++)
                    {
                        skills[i]._Deselect();
                    }

                    CharacterPicker(ID);
                    SetCharacter();
                    SetSpells();

                    firstSelectedObject._Select();
                    eventSytem.SetSelectedGameObject(firstSelectedObject.gameObject);
                }

                else if (Inputs.actions["Return"].WasPressedThisFrame())
                {
                    Hide();
                }

            }

        }

        private void CharacterPicker(int newID)
        {
            currentData = datas[newID];

            switch (newID)
            {
                case 0:
                    character.sprite = characterSprites[0];
                    characterName.text = "ROBYN";
                    break;
                case 1:
                    character.sprite = characterSprites[1];
                    characterName.text = "ABIGAIL";
                    break;
                case 2:
                    character.sprite = characterSprites[2];
                    characterName.text = "PHOEBE";
                    break;
            }



        }

        private void SetCharacter()
        {
            for (int i = 0; i < skills.Length; i++)
            {

                skills[i].Data = currentData;
                skills[i].SetDatas(i);
                skills[i].SetState();

            }
        }

        public void SetSpells()
        {
            for (int i = 0; i < spellTreeSelectors.Length; i++)
            {
                if(i >= currentData.Spells.Length)
                {
                    spellTreeSelectors[i].SetSpells(null);
                }
                else
                {
                    spellTreeSelectors[i].SetSpells(currentData.Spells[i]);
                }
                
            }
        }

        public void SetDescription(SkillTreeSelector selector, Spell spell)
        {
            descriptionName.text = spell.SpellName;
            descriptionText.text = spell.SpellDescription;

            targetImage.sprite = targetTypeDatabase.GetElement(spell.TargetType);
            typeImage.sprite = spellTypeDatabase.GetElement(spell.SpellType);

            switch (spell.SpellPower)
            {
                case Spell.SpellPowerEnum.EASY:
                    hearts[0].sprite = heartFilled;
                    hearts[1].sprite = heartEmpty;
                    hearts[2].sprite = heartEmpty;
                    break;
                case Spell.SpellPowerEnum.MEDIUM:
                    hearts[0].sprite = heartFilled;
                    hearts[1].sprite = heartFilled;
                    hearts[2].sprite = heartEmpty;
                    break;
                case Spell.SpellPowerEnum.HARD:
                    hearts[0].sprite = heartFilled;
                    hearts[1].sprite = heartFilled;
                    hearts[2].sprite = heartFilled;
                    break;
            }

            descriptionBox.anchoredPosition = selector.Rect.anchoredPosition;

        }


        }

    }
