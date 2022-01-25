using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QRCode;
using QRCode.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class GameInfo : MonoBehaviourSingleton<GameInfo>
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI 
            gameVersion,
            currentChapter;

        private Tween openTween;
        private bool isOpen;

        private void Awake()
        {
            Refresh();
        }

        private void FixedUpdate()
        {
            if (Keyboard.current.tabKey.wasPressedThisFrame)
            {
                Open();
            }
        }

        public void Refresh()
        {
            gameVersion.SetText("V " + Application.version);
            if(GameState.gameArguments.currentChapter != null)
                currentChapter.SetText("Current chapter: " + GameState.gameArguments.currentChapter.ChapterName);
        }

        private void Open()
        {
            isOpen = !isOpen;
            openTween?.Kill();

            if (isOpen)
            {
                openTween = canvasGroup.DOFade(1f, .1f);
            }
            else
            {
                openTween = canvasGroup.DOFade(0f, .1f);
            }
        }
    }
}
