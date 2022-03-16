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
        [SerializeField] private float duration;
            
        public void Show()
        {
            panel.DOFade(1f, duration);
        }

        public void Hide(float delay = 2f)
        {
            panel.DOFade(0f, duration).SetDelay(delay);
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
            Hide();
        }
    }
}
