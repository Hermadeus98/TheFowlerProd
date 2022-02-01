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
            ChapterManager.onChapterLoaded += OnChapterLoaded;
        }

        protected override void UnregisterEvent()
        {
            ChapterManager.onChapterLoaded -= OnChapterLoaded;
        }

        private void OnChapterLoaded(Chapter chapter)
        {
            Refresh(new ChapterLoadingArg()
            {
                ChapterData = chapter.ChapterData,
            });
            
            StartCoroutine(ShowIE());
        }

        private IEnumerator ShowIE()
        {
            Show();
            yield return new WaitForSeconds(duration);
            Hide();
        }

        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);
            if (args is ChapterLoadingArg cast)
            {
                chapterName.SetText(cast.ChapterData.ChapterName);
                
            }
        }
    }

    public class ChapterLoadingArg : EventArgs
    {
        public ChapterData ChapterData;
    }
}
