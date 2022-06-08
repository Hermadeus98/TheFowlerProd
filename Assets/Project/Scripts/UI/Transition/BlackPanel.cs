using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QRCode;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class BlackPanel : MonoBehaviourSingleton<BlackPanel>
    {
        [SerializeField] private Image panel;
        public float duration;

        private Tween a;
        
        public void Show()
        {
            a?.Kill();
            a = panel.DOFade(1f, duration);
        }

        public void Show(float delay = 1)
        {
            a?.Kill();
            a = panel.DOFade(1f, delay);
        }

        public void Hide()
        {
            a?.Kill();
            a = panel.DOFade(0f, duration);
        }
        
        public void Hide(float delay = 2f)
        {
            a?.Kill();
            a = panel.DOFade(0f, duration).SetDelay(delay);
        }
        
        private void OnEnable()
        {
            ChapterManager.onChapterLoaded += OnChapterLoaded;
        }

        private void OnDisable()
        {
            ChapterManager.onChapterLoaded -= OnChapterLoaded;
        }

        private void OnChapterLoaded(Chapter chapter)
        {
            Hide(2f);
        }
    }
}
