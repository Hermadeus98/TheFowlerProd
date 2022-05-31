using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using QRCode;
using DG.Tweening;
using System;

namespace TheFowler
{
    public class SkillTreeView : UIView
    {
        [TabGroup("References")] public SkillTreeSelector firstSelectedObject;
        [TabGroup("References")] [SerializeField] private UnityEngine.InputSystem.PlayerInput Inputs;
        [TabGroup("Tree")] [SerializeField] public SkillTreeSelector[] skills, littleSkills, mediumSkills, bigSkills;
        [TabGroup("Tree")] [SerializeField] private LineBehavior[] lines;
        [TabGroup("Tree")] [SerializeField] private BattleActorData[] datas;
        [TabGroup("Tree")] private BattleActorData currentData;
        [TabGroup("Tree")] [SerializeField] private RectTransform descriptionBox;
        [TabGroup("Tree")] [SerializeField] private TMPro.TextMeshProUGUI descriptionName, descriptionText;
        [TabGroup("Tree")] [SerializeField] private Image targetImage, typeImage;
        [TabGroup("Tree")] [SerializeField] private Image[] hearts;
        [TabGroup("Tree")] [SerializeField] private Sprite heartEmpty, heartFilled;
        [TabGroup("Tree")] [SerializeField] private SpellTypeDatabase spellTypeDatabase;
        [TabGroup("Tree")] [SerializeField] private TargetTypeDatabase targetTypeDatabase;
        [TabGroup("Tree")] [SerializeField] public List<SkillTreeSelector> skillsWay;
        [TabGroup("Tree")] [SerializeField] public RectTransform littleCircle,mediumCircle,bigCircle;
        [TabGroup("Tree")] [SerializeField] public Color yellow;


        [TabGroup("Character")] [SerializeField] private Image character;
        [TabGroup("Character")] [SerializeField] private RectTransform characterBox;
        [TabGroup("Character")] [SerializeField] private TMPro.TextMeshProUGUI characterName;
        [TabGroup("Character")] [SerializeField] private Sprite[] characterSprites;

        [TabGroup("Spell")] [SerializeField] private SpellTreeSelector[] spellTreeSelectors;

        [SerializeField] private MenuCharactersView menuView;

        public int usedSkillPoints;
        private int availableSkillPoints;
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

            ID = 0;

            MenuCharactersSKHandler.Instance.DisableEveryone();

            for (int i = 0; i < skills.Length; i++)
            {
                skills[i]._Deselect();
            }

            CharacterPicker(0);

            SetCharacter();

            SetSpells();


            if (eventSytemGO == null)
            {

                eventSytemGO = GameObject.Find("EventSystem");
                eventSytem = eventSytemGO.GetComponent<EventSystem>();

            }

            if (!menuView.onMenu)
            {
                descriptionBox.gameObject.SetActive(true);
            }

            SpawnCircles();
            RefreshSkillPoint();
            RefreshCircles();
        }


        public override void Hide()
        {
            base.Hide();

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).Hide();

            descriptionBox.gameObject.SetActive(false);
            menuView.background.SetActive(true);
            menuView.Show();
        }

        private void Update()
        {
            if (isActive)
            {
                if (Inputs.actions["RightBumper"].WasPressedThisFrame())
                {
                    ID++;
                    if (ID == menuView.numberOfAllies) ID = 0;

                    for (int i = 0; i < skills.Length; i++)
                    {
                        skills[i]._Deselect();
                    }

                    CharacterPicker(ID);
                    SetCharacter();
                    SetSpells();

                    firstSelectedObject._Select();
                    if(!menuView.onMenu)
                        eventSytem.SetSelectedGameObject(firstSelectedObject.gameObject);

                    RefreshSkillPoint();
                    RefreshCircles();

                }
                else if (Inputs.actions["LeftBumper"].WasPressedThisFrame())
                {
                    ID--;
                    if (ID == -1) ID = menuView.numberOfAllies - 1;

                    for (int i = 0; i < skills.Length; i++)
                    {
                        skills[i]._Deselect();
                    }

                    CharacterPicker(ID);
                    SetCharacter();
                    SetSpells();

                    firstSelectedObject._Select();

                    if(!menuView.onMenu)
                        eventSytem.SetSelectedGameObject(firstSelectedObject.gameObject);

                    RefreshSkillPoint();
                    RefreshCircles();

                }

                else if (Inputs.actions["C"].WasPressedThisFrame())
                {
                    ResetTree();
                }

                else if (Inputs.actions["Return"].WasPressedThisFrame())
                {
                    Hide();
                }

            }

        }

        public void ResetTree()
        {

            Array.Resize(ref datas[ID].Spells, 1);

            for (int i = 1; i < datas[ID].AllSpells.Length; i++)
            {
                if(datas[ID].AllSpells[i].spellState == SkillState.EQUIPPED || datas[ID].AllSpells[i].spellState == SkillState.UNSELECTIONNED)
                {
                    datas[ID].AllSpells[i].spellState = SkillState.UNEQUIPPED;
                }
            }

            SetCharacter();

            SetSpells();

            eventSytem.SetSelectedGameObject(firstSelectedObject.gameObject);

            RefreshSkillPoint();
            RefreshCircles();


        }


        private void CharacterPicker(int newID)
        {
            currentData = datas[newID];

            MenuCharactersSKHandler.Instance.DisableEveryone();

            switch (newID)
            {
                case 0:
                    characterName.text = "ROBYN";
                    break;
                case 1:
                    characterName.text = "ABIGAIL";
                    break;
                case 2:
                    characterName.text = "PHOEBE";
                    break;
            }

            MenuCharactersSKHandler.Instance.SetActorTree(newID);



        }

        private void SetCharacter()
        {
            for (int i = 0; i < skills.Length; i++)
            {

                skills[i].Data = currentData;
                skills[i].SetDatas(i);
                skills[i].SetState();


            }

            RefreshAllLines();

            RefreshSkillsWay();
            
        }

        public void RefreshSkillsWay()
        {
            skillsWay.Clear();

            for (int i = 0; i < datas[ID].Spells.Length; i++)
            {
                for (int j = 0; j < skills.Length; j++)
                {
                    if (skills[j].associatedSpell == datas[ID].Spells[i])
                    {
                        skillsWay.Add(skills[j]);
                    }
                }

            }
        }

        public void RefreshAllLines()
        {
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].RefreshLines();

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



            if (LocalisationManager.language == Language.ENGLISH)
            {
                descriptionName.text = spell.SpellName;
                descriptionText.text = spell.SpellDescription;
            }
            else
            {
                descriptionName.text = spell.SpellNameFrench;
                descriptionText.text = spell.SpellDescriptionFrench;
            }


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

        }

        private Tween spawnTween;
        public void SpawnCircles()
        {
            if (spawnTween != null)
                spawnTween.Kill();

            littleCircle.localScale = Vector2.zero;
            mediumCircle.localScale = Vector2.zero;
            bigCircle.localScale = Vector2.zero;

            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].GetComponent<RectTransform>().localScale = Vector2.zero;
            }

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].GetComponent<RectTransform>().localScale = Vector2.zero;
            };

            spawnTween = littleCircle.DOScale(Vector2.one, .2f).
                OnComplete(() => mediumCircle.DOScale(Vector2.one, .2f).
                OnComplete(() => bigCircle.DOScale(Vector2.one, .2f).
                OnComplete(() => RescaleSkills())));

        }

        private void RescaleSkills()
        {
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].GetComponent<RectTransform>().DOScale(new Vector3(.5f, .5f, 1f), .1f);
            };

            RescaleLines();
        }

        private void RescaleLines()
        {
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].GetComponent<RectTransform>().DOScale(Vector2.one, .1f);
            };
        }

        public void RefreshCircles()
        {
            switch (datas[ID].complicityLevel)
            {
                case 1:
                    littleCircle.GetComponent<Image>().color = Color.white;
                    mediumCircle.GetComponent<Image>().color = Color.grey;
                    bigCircle.GetComponent<Image>().color = Color.grey;
                    break;
                case 2:
                    littleCircle.GetComponent<Image>().color = Color.white;
                    mediumCircle.GetComponent<Image>().color = Color.white;
                    bigCircle.GetComponent<Image>().color = Color.grey;
                    break;
                case 3:
                    littleCircle.GetComponent<Image>().color = Color.white;
                    mediumCircle.GetComponent<Image>().color = Color.white;
                    bigCircle.GetComponent<Image>().color = Color.white;
                    break;
            }

            switch (usedSkillPoints)
            {
                case 1:
                    littleCircle.GetComponent<Image>().color = yellow;
                    break;
                case 2:
                    littleCircle.GetComponent<Image>().color = yellow;
                    mediumCircle.GetComponent<Image>().color = yellow;
                    break;
                case 3:
                    littleCircle.GetComponent<Image>().color = yellow;
                    mediumCircle.GetComponent<Image>().color = yellow;
                    bigCircle.GetComponent<Image>().color = yellow;
                    break;
            }
            
        }

        public void RefreshSkillPoint()
        {
            availableSkillPoints = datas[ID].complicityLevel - usedSkillPoints ;
            usedSkillPoints = datas[ID].Spells.Length - 1;


        }


    }

    }
