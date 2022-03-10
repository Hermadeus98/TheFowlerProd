using System;
using DG.Tweening;

using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;
using Unity.RemoteConfig;
using UnityEngine.UI;

namespace TheFowler
{
    /// <summary>
    /// This class is used to show information useful to debug.
    /// </summary>
    public class GameInfo : UIView
    {
        [SerializeField] private TextMeshProUGUI 
            gameVersion,
            currentChapter,
            balancingVersion;

        [SerializeField] private Button fetchButton;

        [SerializeField]
        private Toggle audioToggle;
        [SerializeField]
        private bool mute;

        private Tween openTween;
        private bool isOpen;

        private void Awake()
        {
            RemoteSettingsManager.AddFetchCompletedCallback(SetBalancingVersion);
            Refresh(null);
            fetchButton.onClick.AddListener(RemoteSettingsManager.Fetch);
            if(mute)
            SoundManager.Mute();
        }

        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            ChapterManager.onChapterChange += delegate(Chapter chapter)
            {
                Refresh(null);
            };
        }

        protected override void UnregisterEvent()
        {
            base.UnregisterEvent();
            ChapterManager.onChapterChange -= delegate(Chapter chapter)
            {
                Refresh(null);
            };
        }

        private void FixedUpdate()
        {
            if (Keyboard.current.tabKey.wasPressedThisFrame)
            {
                Open();
            }
        }

        [Button]
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);
            gameVersion.SetText("V " + Application.version);
            if(GameState.gameArguments.currentChapter != null)
                currentChapter.SetText("Current chapter: " + GameState.gameArguments.currentChapter.ChapterName);
        }

        public void SetBalancingVersion(ConfigResponse response)
        {
            var currentBalancingVersion = ConfigManager.appConfig.GetString("BalancingVersion");
            balancingVersion.SetText($"Balancing Version : {currentBalancingVersion}");
        }

        private void Open()
        {
            isOpen = !isOpen;
            openTween?.Kill();

            if (isOpen)
            {
                openTween = CanvasGroup.DOFade(1f, .1f);
                CanvasGroup.interactable = true;
                CanvasGroup.blocksRaycasts = true;
            }
            else
            {
                openTween = CanvasGroup.DOFade(0f, .1f);
                CanvasGroup.interactable = false;
                CanvasGroup.blocksRaycasts = true;
            }
        }

        private void AudioToggle(bool value)
        {
            if (value)
            {
                SoundManager.UnMute();
            }
            else
            {
                SoundManager.Mute();
            }
        }
    }
}
