using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class LoseView : UIView
    {
        private Tween anim;
        public TextNavigation TextNavigation;
        
        [Button]
        public void Restart()
        {
            BattleManager.CurrentBattle.Restart();
        }

        [Button]
        public void MainMenu()
        {
            Game.GoToMainMenu();
        }

        public override void Show()
        {
            base.Show();
            anim?.Kill();
            anim = CanvasGroup.DOFade(1f, .2f);
            TextNavigation.isActive = true;
            TextNavigation.StartNavigate();
        }

        public override void Hide()
        {
            TextNavigation.isActive = false;

            base.Hide();
            anim?.Kill();
            anim = CanvasGroup.DOFade(0, .2f);
        }
    }
}
