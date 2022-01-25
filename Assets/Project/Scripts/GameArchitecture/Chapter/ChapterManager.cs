using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using UnityEngine;

namespace TheFowler
{
    public static class ChapterManager
    {
        public static StateMachine Chapters;

        public static void Initialize()
        {
            var chapters = new Chapter[]
            {
                new ChapterIntro()
                {
                    StateName = "ChapterIntro",
                    ChapterName = "INTRO"
                },
                new ChapterOne()
                {
                    StateName = "ChapterOne",
                    ChapterName = "La rencontre avec Phoebe"
                },
                new ChapterTwo()
                {
                    StateName = "ChapterTwo",
                    ChapterName = "La Volière"
                },
                new ChapterThree()
                {
                    StateName = "ChapterThree",
                    ChapterName = "Le Tribunal"
                },
                new ChapterOutro()
                {
                    StateName = "ChapterOutro",
                    ChapterName = "OUTRO"
                },
            };

            Chapters = new StateMachine(chapters, UpdateMode.Update, GameState.gameArguments);
        }

        public static void ChangeChapter(ChapterEnum chapter)
        {
            Chapters.SetState(GetChapterKey(chapter), GameState.gameArguments);
            GameInfo.Instance.Refresh();
        }

        private static string GetChapterKey(ChapterEnum key)
        {
            return key switch
            {
                ChapterEnum.CHAPTER_ONE => "ChapterOne",
                ChapterEnum.CHAPTER_TWO => "ChapterTwo",
                ChapterEnum.CHAPTER_THREE => "ChapterThree",
                ChapterEnum.INTRO => "ChapterIntro",
                ChapterEnum.OUTRO => "ChapterOutro",
                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
            };
        }
    }

    public enum ChapterEnum
    {
        INTRO,
        CHAPTER_ONE,
        CHAPTER_TWO,
        CHAPTER_THREE,
        OUTRO,
    }
}
