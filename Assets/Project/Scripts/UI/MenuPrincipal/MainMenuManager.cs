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

namespace TheFowler
{
    public class MainMenuManager : SerializedMonoBehaviour
    {
        [SerializeField] private TextNavigation
            main,
            chapters,
            settings;

        [SerializeField] private MenuPanel currentPanel;

        [SerializeField] private PlayerInput input;

        [SerializeField] private CanvasGroup openning, manette, menu;
        public float fadeDuration = .2f;
        public float showDuration = 2f;

        public Image blackPanel;
        
        public enum MenuPanel
        {
            MAIN,
            SETTINGS,
            CHAPTER,
            CREDITS,
        }
        
        private void Awake()
        {
            currentPanel = MenuPanel.MAIN;
            
            main.StartNavigate();
            chapters.StartNavigate();
            settings.StartNavigate();

            menu.alpha = 0;
            openning.alpha = 0;
            manette.alpha = 0;
            
            StartCoroutine(Opening());
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
            menu.DOFade(1f, fadeDuration);
            
            ReturnToMain();
        }

        private void Update()
        {
            if (input.actions["B"].WasPressedThisFrame())
            {
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

        public void ReturnToMain()
        {
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
            blackPanel.DOFade(1f, fadeDuration);
            yield return new WaitForSeconds(fadeDuration + .1f);
            
            SceneManager.UnloadSceneAsync("Scene_MenuPrincipal");

            if(chapter == 1)
                ChapterManager.GoChapterOne();
            if(chapter == 2)
                ChapterManager.GoChapterTwo();
            if(chapter == 3)
                ChapterManager.GoChapterThree();
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Play()
        {
            
        }
    }
}
