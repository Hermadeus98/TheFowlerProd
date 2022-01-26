using DG.Tweening;

using QRCode;
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
    public class GameInfo : MonoBehaviourSingleton<GameInfo>
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI 
            gameVersion,
            currentChapter,
            balancingVersion;

        [SerializeField] private Button fetchButton;
        
        private Tween openTween;
        private bool isOpen;

        private void Awake()
        {
            RemoteSettingsManager.AddFetchCompletedCallback(SetBalancingVersion);
            Refresh();
            fetchButton.onClick.AddListener(RemoteSettingsManager.Fetch);
        }

        private void FixedUpdate()
        {
            if (Keyboard.current.tabKey.wasPressedThisFrame)
            {
                Open();
            }
        }

        [Button]
        public void Refresh()
        {
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
                openTween = canvasGroup.DOFade(1f, .1f);
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
            else
            {
                openTween = canvasGroup.DOFade(0f, .1f);
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = true;
            }
        }
    }
}
