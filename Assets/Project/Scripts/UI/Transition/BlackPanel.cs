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
        [SerializeField] private Image panel, panelIntro;
        public float duration;

        private Tween a, b;

        public static BlackPanel instance;

        private void Awake()
        {
            instance = this;
        }
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

        public void ShowPanelIntro()
        {
            b?.Kill();
            b= panelIntro.DOFade(1f, .5f);
        }

        public void HidePanelIntro()
        {
            b?.Kill();
            b = panelIntro.DOFade(0f, .2f);
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


        public void HideDirectly()
        {
            a?.Kill();
            if(panel!= null)
                panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0);

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
