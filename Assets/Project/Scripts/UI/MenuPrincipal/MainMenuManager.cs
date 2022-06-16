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
using TMPro;

namespace TheFowler
{
    public class MainMenuManager : SerializedMonoBehaviour
    {
        public static bool havePlayIntro = false;

        public AK.Wwise.Event robynPlay;
        public float delay;
        
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

        public TextMeshProUGUI description;

        
        public enum MenuPanel
        {
            MAIN,
            SETTINGS,
            CHAPTER,
            CREDITS,
        }
        
        private void Awake()
        {
            description.gameObject.SetActive(false);
            Player.canOpenPauseMenu = false;
            Player.isInPauseMenu = true;


            currentPanel = MenuPanel.MAIN;
            
            BlackPanel.Instance.Hide();
            
            menu.alpha = 0;
            openning.alpha = 0;
            manette.alpha = 0;
            blackscreen.alpha = 1;
            if (!havePlayIntro)
                StartCoroutine(Opening());
            else
            {
                chapters.StartNavigate();
                settings.StartNavigate();
                main.StartNavigate();
                ReturnToMain();
                menu.DOFade(1f, .2f);
                blackscreen.DOFade(0f, fadeDuration + 1);
            }
        }

        IEnumerator Opening()
        {
            
            yield return new WaitForSeconds(showDuration);
            BlackPanel.Instance.HideDirectly();
            openning.DOFade(1f, fadeDuration);
            yield return new WaitForSeconds(fadeDuration);
            BlackPanel.Instance.HideDirectly();
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

            menu.DOFade(1f, fadeDuration + 1).OnComplete(()=>
            {
                chapters.StartNavigate();
                settings.StartNavigate();
                main.StartNavigate();

                ReturnToMain();
                description.gameObject.SetActive(true);
            });
        }

        private void Update()
        {
            if(!isActive)
                return;
            
            if (input.actions["B"].WasPressedThisFrame())
            {
                SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_Cancel, null);
                
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
            blackscreen.DOFade(0f, 0f);
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

        IEnumerator PlaySound()
        {
            yield return new WaitForSeconds(delay);
            if(robynPlay != null)
                robynPlay.Post(gameObject);
        }

        IEnumerator ChooseChapterIE(int chapter)
        {
            robyn.SetTrigger("Play");
            yield return PlaySound();


            //backGround.DOScale(backGround.localScale * 1.2f, 1f).SetEase(Ease.InOutSine);


            yield return new WaitForSeconds(.6f);
            if (chapter == 1)
            {
                BlackPanel.instance.ShowPanelIntro();
            }

            yield return new WaitForSeconds(fadeDuration + .1f);

            AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync("Scene_MenuPrincipal");

            if (chapter == 1)
            {
                //UI.OpenView("VideoView");
                ChapterManager.GoChapterOne();
            }


            if (chapter == 2)
            {
                ChapterManager.GoChapterTwo();
                Tutoriel.SkipTuto();
            }

            if (chapter == 3)
            {
                ChapterManager.GoChapterThree();
                Tutoriel.SkipTuto();
            }

            if(chapter == 4)
            {
                ChapterManager.GoChapterFour();
                Tutoriel.SkipTuto();
            }

            
            Player.canOpenPauseMenu = true;
            Player.isInPauseMenu = false;
            havePlayIntro = false;

            //while (!asyncLoad.isDone)
            //{
            //    yield return null;
            //}

            //BlackPanel.Instance.Hide(4);
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
            DifficultyManager.ChangeDifficulty(DifficultyEnum.MEDIUM);
        }

        public void SetDifficultyHard()
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
            CreditView.Instance.onEnd.AddListener(()=> main.canNavigate = true);

            main.canNavigate = false;
        }

        private void OnDisable()
        {
            Player.canOpenPauseMenu = true;
        }
    }
}
