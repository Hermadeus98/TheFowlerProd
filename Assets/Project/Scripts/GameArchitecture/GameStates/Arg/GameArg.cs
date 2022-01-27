using System;

namespace TheFowler
{
    public class GameArg : EventArgs
    {
        public bool noloadingChapter = false;
        
        public Chapter currentChapter;
        public ChapterData currentChapterData => currentChapter.ChapterData;
    }
}
