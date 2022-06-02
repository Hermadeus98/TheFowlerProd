using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;


namespace TheFowler
{
    public class TutorielView : UIView
    {

        [TabGroup("References"), SerializeField] private MMFeedbacks feedbackIn, feedbackOut;
        [TabGroup("References"), SerializeField] private PlayerInput playerInput;
        [TabGroup("References"), SerializeField] private CanvasGroup battleUI, menuCharacters;
        [TabGroup("Panels"), SerializeField] private CanvasGroup basicAttack, basicAttack2, spell, types, fury, target, buff, parry, heal, done;
        [TabGroup("Tutoriel"), SerializeField] public TutorielElement _basicAttack, _spell, _quickAttack, _breakdown, _progression, _dead, _welcome, _buff, _types;
        [TabGroup("References"), SerializeField] private AK.Wwise.Event tutoOn, tutoOff;

        private CanvasGroup currentPanel;
        private TutorielElement currentElement;


        private bool isShowed = false;
        private bool isDisplayed = false;
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);
        }

        [Button]
        public override void Show()
        {
            base.Show();

            if (!isShowed)
            {
                feedbackIn.PlayFeedbacks();
                isShowed = true;
                tutoOn.Post(gameObject);

                if (VolumesManager.Instance != null)
                    VolumesManager.Instance.TutoVolume.enabled = true;

                
                
                menuCharacters.gameObject.SetActive(false);

                battleUI.alpha = 0;
            }


            playerInput.enabled = true;
        }

        private void Update()
        {
            if (isActive)
            {
                if (playerInput.actions["Confirm"].WasPressedThisFrame() && isDisplayed)
                {

                    StartCoroutine(WaitEndTutoElement());
                    
                }
            }
        }

        private IEnumerator WaitEndTutoElement()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            if (!Tutoriel.endIntro) Tutoriel.endIntro = true;

            if (currentElement != null && currentElement.nextElement != null)
            {
                currentElement.canvasGroup.alpha = 0;
                currentElement.End();
                currentElement.nextElement.canvasGroup.DOFade(1, .2f).OnComplete(() => playerInput.enabled = true);
                currentElement.nextElement.Initialize();

                currentElement = currentElement.nextElement;

                isDisplayed = true;
            }
            else
            {
                FadeAllCanvasGroup();
                isDisplayed = false;
            }

            yield break;
        }

        public  void Show(PanelTutoriel panel)
        {
            FadeAllCanvasGroup();
            
            switch (panel)
            {
                case PanelTutoriel.BASICATTACK:
                    basicAttack.DOFade(1, .2f);
                    currentPanel = basicAttack;
                    break;
                case PanelTutoriel.BASICATTACK2:
                    basicAttack2.DOFade(1, 0f);
                    currentPanel = basicAttack2;
                    break;
                case PanelTutoriel.SPELL:
                    spell.DOFade(1, .2f);
                    currentPanel = spell;
                    break;
                case PanelTutoriel.TYPES:
                    types.DOFade(1, 0);
                    currentPanel = types;
                    break;
                case PanelTutoriel.FURY:
                    fury.DOFade(1, .2f);
                    currentPanel = fury;
                    break;
                case PanelTutoriel.TARGET:
                    target.DOFade(1, .2f);
                    currentPanel = target;
                    break;
                case PanelTutoriel.BUFF:
                    buff.DOFade(1, .2f);
                    currentPanel = buff;
                    break;
                case PanelTutoriel.PARRY:
                    parry.DOFade(1, .2f);
                    currentPanel = parry;
                    break;
                case PanelTutoriel.HEAL:
                    heal.DOFade(1, .2f);
                    currentPanel = heal;
                    break;
                case PanelTutoriel.DONE:
                    done.DOFade(1, .2f);
                    currentPanel = done;
                    break;
            }

            Show();


        }

        public void Show(TutorielEnum panel, float timeToWait)
        {
            FadeAllCanvasGroup();

            

            switch (panel)
            {
                case TutorielEnum.BASICATTACK:
                    currentElement = _basicAttack;
                    break;
                case TutorielEnum.SPELL:
                    currentElement = _spell;
                    break;
                case TutorielEnum.QUICKATTACK:
                    currentElement = _quickAttack;
                    break;
                case TutorielEnum.BREAKDOWN:
                    currentElement = _breakdown;
                    break;
                case TutorielEnum.PROGRESSION:
                    currentElement = _progression;
                    break;
                case TutorielEnum.DEAD:
                    currentElement = _dead;
                    break;
                case TutorielEnum.WELCOME:
                    currentElement = _welcome;
                    break;
                case TutorielEnum.BUFF:
                    currentElement = _buff;
                    break;
                case TutorielEnum.TYPES:
                    currentElement = _types;
                    break;
            }
            StartCoroutine(WaitTuto(timeToWait));
           
        }

        private IEnumerator WaitTuto(float timeToWait)
        {
            UI.GetView<MenuCharactersView>(UI.Views.MenuCharacters).Inputs.enabled = false;
            UI.GetView<SkillPickingView>(UI.Views.SkillPicking).Inputs.enabled = false;



            if (BattleManager.CurrentBattle != null)
            {
                BattleManager.CurrentBattle.Inputs.enabled = false;
            }

            yield return new WaitForSeconds(timeToWait);



            currentElement.canvasGroup.DOFade(1, .2f).OnComplete(() => playerInput.enabled = true);
            currentElement.Initialize();

            Show();

            isDisplayed = true;

            yield break;
        }

        private void FadeAllCanvasGroup()
        {

            basicAttack.alpha = 0;
            basicAttack2.alpha = 0;
            spell.alpha = 0;
            types.alpha = 0;
            fury.alpha = 0;
            target.alpha = 0;
            buff.alpha = 0;
            parry.alpha = 0;
            heal.alpha = 0;
            done.alpha = 0;

            if(currentElement != null)
            {
                currentElement.canvasGroup.alpha = 0;
                currentElement.End();

            }




            Hide();
            
        }

        public override void Hide()
        {
            base.Hide();
            if (isShowed)
            {
                feedbackOut.PlayFeedbacks();
                isShowed = false;
                //currentPanel.alpha = 0;
                tutoOff.Post(gameObject);

                if (VolumesManager.Instance != null)
                    VolumesManager.Instance.TutoVolume.enabled = false;

                battleUI.alpha = 1;
                menuCharacters.gameObject.SetActive(true);
                currentElement = null;

                if (BattleManager.CurrentBattle != null)
                {
                    BattleManager.CurrentBattle.Inputs.enabled = true;
                }

                UI.GetView<MenuCharactersView>(UI.Views.MenuCharacters).Inputs.enabled = true;
                UI.GetView<SkillPickingView>(UI.Views.SkillPicking).Inputs.enabled = true;
                playerInput.enabled = false;
            }
        }
    }

    public enum TutorielEnum
    {
        BASICATTACK,
        SPELL,
        QUICKATTACK,
        BREAKDOWN,
        PROGRESSION,
        DEAD,
        WELCOME,
        BUFF,
        INITIATIVE,
        TYPES
    }
}

