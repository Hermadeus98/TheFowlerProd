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

        private bool isClosed = true;
        
        [Button]
        public void Restart()
        {
            StartCoroutine(Close(BattleManager.CurrentBattle.Restart));
        }

        IEnumerator Close(Action onEnd)
        {
            TextNavigation.isActive = false;
            Hide();
            anim?.Kill();
            anim = CanvasGroup.DOFade(0f, .2f);
            yield return new WaitForSeconds(.2f);
            yield return H();
            Player.isInPauseMenu = false;
            onEnd?.Invoke();
        }

        [Button]
        public void MainMenu()
        {
            StartCoroutine(Close(Game.GoToMainMenu));
        }

        public override void Show()
        {
            base.Show();
            StartCoroutine(S());
        }

        IEnumerator S()
        {

            isClosed = false;
            
            Player.isInPauseMenu = true;
            TurnTransitionView.isLock = true;

            

            yield return LoseGraphics.Instance.Open();
            
            anim?.Kill();
            anim = CanvasGroup.DOFade(1f, .2f);
            TextNavigation.isActive = true;
            TextNavigation.StartNavigate();
        }

        public override void Hide()
        {
            Debug.Log("aaa");

            TextNavigation.isActive = false;

            StartCoroutine(H());
            
            base.Hide();
        }

        IEnumerator H()
        {
            if(isClosed)
                yield break;
            isClosed = true;

            
            anim?.Kill();
            anim = CanvasGroup.DOFade(0, .2f);
            yield return new WaitForSeconds(.2f);
            yield return LoseGraphics.Instance.Close();
            TurnTransitionView.isLock = false;
            yield return new WaitForSeconds(.25f);
        }
    }
}
