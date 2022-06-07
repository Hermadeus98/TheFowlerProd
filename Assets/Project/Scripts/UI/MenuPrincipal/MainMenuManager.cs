using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
namespace TheFowler
{
    public class MainMenuManager : SerializedMonoBehaviour
    {
        public static bool havePlayIntro = false;
        
        [SerializeField] private TextNavigation
            main,
            chapters,
            settings;

        [SerializeField] private MenuPanel currentPanel;

        [SerializeField] private PlayerInput input;

        [SerializeField] private CanvasGroup openning, manette, menu, blackscreen;
        public float fadeDuration = .2f;
        public float showDuration = 2f;
        private float dissolveValue;

        public RectTransform backGround;

        private bool isCredit = false;
        private bool isActive;

        public Animator robyn;
        
        public enum MenuPanel
        {
            MAIN,
            SETTINGS,
            CHAPTER,
            CREDITS,
        }
        
        private void Awake()
        {
            Player.canOpenPauseMenu = false;
            Player.isInPauseMenu = true;
            
            currentPanel = MenuPanel.MAIN;
            
            main.StartNavigate();
            chapters.StartNavigate();
            settings.StartNavigate();

            menu.alpha = 0;
            openning.alpha = 0;
            manette.alpha = 0;
            blackscreen.alpha = 1;
            if (!havePlayIntro)
                StartCoroutine(Opening());
            else
            {
                ReturnToMain();
            }
        }

        IEnumerator Opening()
        {
            yield return new WaitForSeconds(showDuration);
            openning.DOFade(1f, fadeDuration);
            yield return new WaitForSeconds(fadeDuration);
            yield return new WaitForSeconds(showDuration);
            
            openning.DOFade(0f, fadeDuration);
            yield return new WaitForSeconds(fadeDuration);
            manette.DOFade(1f, fadeDuration);
            yield return new WaitForSeconds(fadeDuration);
            yield return new WaitForSeconds(showDuration);

            manette.DOFade(0f, fadeDuration);
            yield return new WaitForSeconds(fadeDuration);
            blackscreen.DOFade(0f, fadeDuration + 1);
            yield return new WaitForSeconds(fadeDuration + 1);
            menu.DOFade(1f, fadeDuration + 1).OnComplete(()=>ReturnToMain());
        }

        private void Update()
        {
            if(!isActive)
                return;
            
            if (input.actions["B"].WasPressedThisFrame())
            {
                if (CreditView.Instance.isActive)
                {
                    CreditView.Instance.Hide();
                    ReturnToMain();
                    main.canNavigate = true;
                    return;
                }
                
                switch (currentPanel)
                {
                    case MenuPanel.MAIN:
                        break;
                    case MenuPanel.SETTINGS:
                        ReturnToMain();
                        break;
                    case MenuPanel.CHAPTER:
                        ReturnToMain();
                        break;
                    case MenuPanel.CREDITS:
                        ReturnToMain();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void OpenChapter()
        {
            currentPanel = MenuPanel.CHAPTER;
            main.HideAnim();
            chapters.ShowAnim();
            settings.HideAnim();
        }

        public void OpenSettings()
        {
            currentPanel = MenuPanel.SETTINGS;
            main.HideAnim();
            chapters.HideAnim();
            settings.ShowAnim();
        }

        [Button]
        public void ReturnToMain()
        {
            havePlayIntro = true;
            isActive = true;
            
            currentPanel = MenuPanel.MAIN;
            main.ShowAnim();
            chapters.HideAnim();
            settings.HideAnim();
        }

        public void NewGame() => ChooseChapter(1);
        
        public void ChooseChapter(int chapter)
        {
            StartCoroutine(ChooseChapterIE(chapter));
        }

        IEnumerator ChooseChapterIE(int chapter)
        {
            robyn.SetTrigger("Play");
            
            //backGround.DOScale(backGround.localScale * 1.2f, 1f).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(.6f);

            BlackPanel.Instance.Show();
            yield return new WaitForSeconds(fadeDuration + .1f);

            SceneManager.UnloadSceneAsync("Scene_MenuPrincipal");

            if(chapter == 1)
                ChapterManager.GoChapterOne();
            if(chapter == 2)
                ChapterManager.GoChapterTwo();
            if(chapter == 3)
                ChapterManager.GoChapterThree();
            if(chapter == 4)
                ChapterManager.GoChapterFour();
            
            Player.canOpenPauseMenu = true;
            Player.isInPauseMenu = false;
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void SetPreviewOn()
        {
            Player.showPreview = true;
        }

        public void SetPreviewOff()
        {
            Player.showPreview = false;
        }

        public void SetDifficultyEasy()
        {
            DifficultyManager.ChangeDifficulty(DifficultyEnum.EASY);
        }

        public void SetDifficultyMedium()
        {
            DifficultyManager.ChangeDifficulty(DifficultyEnum.HARD);
        }

        public void SetLanguageFrench()
        {
            LocalisationManager.ChangeLanguage(1);
        }

        public void SetLanguageEnglish()
        {
            LocalisationManager.ChangeLanguage(0);
        }

        public void SetMasterVolume(float v)
        {
            SoundManager.SetMasterVolume(v);
        }

        public void SetAmbiantVolume(float v)
        {
            SoundManager.SetAmbiantVolume(v);
        }

        public void SetMusicVolume(float v)
        {
            SoundManager.SetMusicVolume(v);
        }

        public void SetVoicesVolume(float v)
        {
            SoundManager.SetVoicesVolume(v);
        }

        public void SetEffectsVolume(float v)
        {
            SoundManager.SetMusicVolume(v);
        }

        public void ChangeColorBlind(int v)
        {
            ColorBlindHandler.Instance.ChangeProfile(v);
        }

        public void Credit()
        {
            CreditView.Instance.Show();

            main.canNavigate = false;
        }
    }
}
