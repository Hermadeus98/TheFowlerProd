using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace TheFowler
{
    public class GameLogView : UIView
    {
        [TabGroup("References"), HideInInspector]
        public bool isOpen;
        [TabGroup("References")]
        [SerializeField] private UnityEngine.InputSystem.PlayerInput Inputs;
        [TabGroup("References")]
        [SerializeField] private Image indicatorTouchs;
        [TabGroup("References")]
        [SerializeField] private GameLogElement[] gameLogElements;

        private Vector2 hidePos, showPos;

        private Tween touchBlink;
        public override void Show()
        {
            base.Show();
            hidePos = RectTransform.anchoredPosition;
            showPos = Vector2.zero;

            touchBlink = indicatorTouchs.DOColor(Color.yellow, .5f).SetLoops(-1, LoopType.Yoyo);
        }


        public override void Hide()
        {
            base.Hide();

            Close();


        }

        private void Update()
        {
            if (isActive)
            {
                if (Inputs.actions["RightBumper"].WasPressedThisFrame())
                {
                    if (isOpen)
                    {
                        Close();
                    }
                    else
                    {
                        Open();

                    }
                }
            }
        }

        private void Close()
        {
            RectTransform.DOAnchorPos(hidePos, .3f);
            isOpen = false;
            touchBlink.Kill();
        }

        private void Open()
        {
            RectTransform.DOAnchorPos(showPos, .3f);
            isOpen = true;
            touchBlink.Kill();
            indicatorTouchs.color = Color.black;

            for (int i = 0; i < gameLogElements.Length; i++)
            {
                gameLogElements[i].canvasGroup.alpha = 0;
            }

            for (int i = 0; i < BattleManager.CurrentBattle.BattleGameLogComponent.enemyActionDatas.Count; i++)
            {
                gameLogElements[i].canvasGroup.alpha = 1;
                gameLogElements[i].Initialize(BattleManager.CurrentBattle.BattleGameLogComponent.enemyActionDatas[i]);
            }
        }
    }
}

