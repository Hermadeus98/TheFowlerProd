using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using TheFowler;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuPauseManager : MonoBehaviour
{
    [SerializeField] private TextNavigation
        main,
        settings;
    
    public float fadeDuration = .2f;
    public float showDuration = 2f;
    
    [SerializeField] private MenuPausePanel currentPanel;
    [SerializeField] private PlayerInput input;

    private bool isActive = false;
    private bool isOpen = false;
    
    public RectTransform backGround;
    public GameObject menu;
    [SerializeField]
    private CanvasGroup battleUI;

    [SerializeField]
    private TextNavigation textNavigation;
    [SerializeField]
    private UISelectorElement restartElement;

    private void Awake()
    {
        currentPanel = MenuPausePanel.MAIN;
            
        main.StartNavigate();
        settings.StartNavigate();
    }

    public void Open()
    {
        if(!Player.canOpenPauseMenu)
            return;
        
        menu.gameObject.SetActive(true);
        
        Player.isInPauseMenu = true;

        isActive = true;
        isOpen = true;
        ReturnToMain();
        GetComponent<CanvasGroup>().alpha = 1;

        MenuPauseHandler.Instance.Initialize();




        Player.canOpenPauseMenu = true;

        if(BattleManager.CurrentBattle != null)
        {
            if (!textNavigation.all_elements.Contains(restartElement))
            {
                restartElement.gameObject.SetActive(true);
                textNavigation.all_elements.Insert(1, restartElement);
                textNavigation.StartNavigate();
            }
            
        }
    }

    private IEnumerator WaitIsActive()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();


        if (BattleManager.CurrentBattle != null)
        {
            BattleManager.CurrentBattle.Inputs.enabled = true;
            battleUI.alpha = 1;

            UI.GetView<SkillPickingView>(UI.Views.SkillPicking).Inputs.enabled = true;
            UI.GetView<ActionPickingView>(UI.Views.ActionPicking).Inputs.enabled = true;
        }

        if (BattleManager.CurrentBattle != null)
        {
            if (!textNavigation.all_elements.Contains(restartElement))
            {
                restartElement.gameObject.SetActive(false);
                textNavigation.all_elements.Remove(restartElement);

            }

        }

    }

    public void Hide()
    {
        menu.gameObject.SetActive(false);
        
        Player.isInPauseMenu = false;

        StartCoroutine(WaitIsActive());
        isOpen = false;
        isActive = false;
        GetComponent<CanvasGroup>().alpha = 0;

        MenuPauseHandler.Instance.Close();

    }
    
    [Button]
    public void ReturnToMain()
    {
        isActive = true;
        
        currentPanel = MenuPausePanel.MAIN;
        main.ShowAnim();
        settings.HideAnim();
    }
    
    public void NewGame() => ChooseChapter(1);
        
    public void ChooseChapter(int chapter)
    {
        StartCoroutine(ChooseChapterIE(chapter));
    }

    public void RestartBattle()
    {

        BattleManager.CurrentBattle.Lose();
        menu.gameObject.SetActive(false);

        Player.isInPauseMenu = false;
        isOpen = false;
        isActive = false;
        GetComponent<CanvasGroup>().alpha = 0;

        MenuPauseHandler.Instance.Close();

    }
    
    IEnumerator ChooseChapterIE(int chapter)
    {
        backGround.DOScale(backGround.localScale * 1.2f, 1f).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(1f + .1f);
            
        BlackPanel.Instance.Show();
        yield return new WaitForSeconds(fadeDuration + .1f);

        SceneManager.UnloadSceneAsync("Scene_MenuPrincipal");

        if(chapter == 1)
            ChapterManager.GoChapterOne();
        if(chapter == 2)
            ChapterManager.GoChapterTwo();
        if(chapter == 3)
            ChapterManager.GoChapterThree();
        if(chapter == 3)
            ChapterManager.GoChapterFour();
    }
    
    public void OpenSettings()
    {
        currentPanel = MenuPausePanel.SETTINGS;
        main.HideAnim();
        settings.ShowAnim();
    }
    
    private void Update()
    {
        if(!Player.canOpenPauseMenu)
            return;
        
        if (input.actions["Pause"].WasPressedThisFrame())
        {
            if (!isOpen)
            {
                FindObjectOfType<MMTimeManager>().SetTimescaleTo(0);
                Open();
            }
            else
            {
                FindObjectOfType<MMTimeManager>().SetTimescaleTo(1);
                Hide();
            }
        }

        if (isActive)
        {
            if (BattleManager.CurrentBattle != null)
            {
                BattleManager.CurrentBattle.Inputs.enabled = false;
                battleUI.alpha = 0;

                UI.GetView<SkillPickingView>(UI.Views.SkillPicking).Inputs.enabled = false;
                UI.GetView<ActionPickingView>(UI.Views.ActionPicking).Inputs.enabled = false;
            }

        }
        
        if(!isActive)
            return;
            
        if (input.actions["B"].WasPressedThisFrame())
        {
            switch (currentPanel)
            {
                case MenuPausePanel.MAIN:
                    Hide();
                    break;
                case MenuPausePanel.SETTINGS:
                    ReturnToMain();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void ReturnToMainMenu()
    {
        Hide();
        Game.GoToMainMenu();
    }
    
    public void SetDifficultyEasy()
    {
        DifficultyManager.ChangeDifficulty(DifficultyEnum.EASY);
    }

    public void SetDifficultyNormal()
    {
        DifficultyManager.ChangeDifficulty(DifficultyEnum.MEDIUM);
    }

    public void SetDifficultyHard()
    {
        DifficultyManager.ChangeDifficulty(DifficultyEnum.HARD);
    }
    
    public void SetPreviewOn()
    {
        Player.showPreview = true;
    }

    public void SetPreviewOff()
    {
        Player.showPreview = false;
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
    
    public void SetAmbiantVolume(float v)
    {
        SoundManager.SetAmbiantVolume(v);
    }
    
    public void ChangeColorBlind(int v)
    {
        ColorBlindHandler.Instance.ChangeProfile(v);
    }
}

public enum MenuPausePanel
{
    MAIN,
    SETTINGS
}
