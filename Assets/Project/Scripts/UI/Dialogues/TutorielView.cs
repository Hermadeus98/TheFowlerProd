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
        [TabGroup("Tutoriel"), SerializeField] public TutorielElement _basicAttack, _spell, _quickAttack, _breakdown, _progression, _dead, _welcome, _buff, _types;
        [TabGroup("References"), SerializeField] private AK.Wwise.Event tutoOn, tutoOff;

        private CanvasGroup currentPanel;
        private TutorielElement currentElement;

        private Tween tween;

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

                UI.GetView<MenuCharactersView>(UI.Views.MenuCharacters).ChangeCustomeElementState(false);

                battleUI.alpha = 0;
            }

            Player.canOpenPauseMenu = false;
            playerInput.enabled = true;

        }

        private void Update()
        {
            if (isActive)
            {
                if (playerInput.actions["Confirm"].WasPressedThisFrame() && isDisplayed)
                {

                    StartCoroutine(WaitEndTutoElement());

                    SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_SkillSelection, gameObject);
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
                playerInput.enabled = false;
                currentElement.GetComponent<RectTransform>().DOAnchorPos(new Vector3(800, 91, 0), .2f).SetEase(Ease.Linear).OnComplete(() => EndTutoElementAndLaunchNext());

                yield return new WaitForSeconds(.7f);

                isDisplayed = true;
            }
            else
            {
                currentElement.GetComponent<RectTransform>().DOAnchorPos(new Vector3(800, 91, 0), .2f).SetEase(Ease.Linear).OnComplete(()=>EndTutoElement());
            
                isDisplayed = false;
            }

            yield break;
        }

        private void EndTutoElement()
        {
            if (currentElement != null)
            {
                currentElement.canvasGroup.alpha = 0;
                currentElement.End();

            }

            Hide();
        }

        private void EndTutoElementAndLaunchNext()
        {
            if (currentElement != null)
            {
                currentElement.canvasGroup.alpha = 0;
                currentElement.End();


                currentElement.nextElement.GetComponent<RectTransform>().anchoredPosition = new Vector3(800, 91, 0);
                currentElement.nextElement.canvasGroup.alpha = 1;
                currentElement.nextElement.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, 91, 0), .2f).SetEase(Ease.Linear).OnComplete(() => playerInput.enabled = true);
                currentElement.nextElement.Initialize();

                currentElement = currentElement.nextElement;

            }

        }

       

        public void Show(TutorielEnum panel, float timeToWait)
        {
            MenuPauseHandler.Instance.Close();
            FadeAllCanvasGroup();
            Player.canOpenPauseMenu = false;


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

            currentElement.canvasGroup.alpha = 1;
            currentElement.GetComponent<RectTransform>().anchoredPosition = new Vector3(800, 91, 0);
            currentElement.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, 91, 0), .2f).SetEase(Ease.Linear).OnComplete(() => playerInput.enabled = true);
            currentElement.Initialize();

            Show();

            yield return new WaitForSeconds(.5f);

            isDisplayed = true;

            yield break;
        }

        private void FadeAllCanvasGroup()
        {



            if(currentElement != null)
            {
                currentElement.canvasGroup.alpha = 0;
                currentElement.End();

            }




            Hide();
            
        }

        public override void Hide()
        {

            if(UI.GetView<MenuCharactersView>(UI.Views.MenuCharacters).isActive)
                UI.GetView<MenuCharactersView>(UI.Views.MenuCharacters).Inputs.enabled = true;
            UI.GetView<SkillPickingView>(UI.Views.SkillPicking).Inputs.enabled = true;


            if (BattleManager.CurrentBattle != null)
            {
                Player.canOpenPauseMenu = true;
            }

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

                UI.GetView<MenuCharactersView>(UI.Views.MenuCharacters).ChangeCustomeElementState(true);
                playerInput.enabled = false;
                
                Player.canOpenPauseMenu = true;
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

