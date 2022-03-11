using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TheFowler
{
    public class ChapterLoaderView : UIView
    {
        [SerializeField] private TextMeshProUGUI chapterName;
        [SerializeField] private float duration = 2f;

        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            //ChapterManager.onChapterLoaded += OnChapterLoaded;
        }

        protected override void UnregisterEvent()
        {
            base.UnregisterEvent();
            //ChapterManager.onChapterLoaded -= OnChapterLoaded;
        }

        private void OnChapterLoaded(Chapter chapter)
        {
            if (chapter.ChapterData.ShowChapterTitle)
            {
                Refresh(new ChapterLoadingArg()
                {
                    ChapterData = chapter.ChapterData,
                });
            }
        }

        private IEnumerator ShowIE()
        {
            Show();
            yield return new WaitForSeconds(duration);
            Hide();
        }

        public void Refresh(ChapterData ChapterData)
        {
            chapterName.SetText(ChapterData.ChapterName);
        }

        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);
            if (args is ChapterLoadingArg cast)
            {
                chapterName.SetText(cast.ChapterData.ChapterName);
                
                StartCoroutine(ShowIE());
            }
        }
    }

    public class ChapterLoadingArg : EventArgs
    {
        public ChapterData ChapterData;
    }
}
