using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
            
            main.ShowAnim();
            chapters.HideAnim();
            settings.HideAnim();
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
