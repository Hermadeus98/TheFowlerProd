using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using QRCode;
using UnityEngine;

namespace TheFowler
{
    public static class ChapterManager
    {
        public static StateMachine Chapters;

        private const string DataPath = "Data/Scenes Datas/Chapters/";

        public static void Initialize()
        {
            var chapters = new Chapter[]
            {
                new ChapterIntro()
                {
                    StateName = "ChapterIntro",
                    ChapterData = Resources.Load<ChapterData>(DataPath + "Intro"),
                },
                new ChapterOne()
                {
                    StateName = "ChapterOne",
                    ChapterData = Resources.Load<ChapterData>(DataPath + "Chapter One"),
                },
                new ChapterTwo()
                {
                    StateName = "ChapterTwo",
                    ChapterData = Resources.Load<ChapterData>(DataPath + "Chapter Two"),
                },
                new ChapterThree()
                {
                    StateName = "ChapterThree",
                    ChapterData = Resources.Load<ChapterData>(DataPath + "Chapter Three"),
                },
                new ChapterOutro()
                {
                    StateName = "ChapterOutro",
                    ChapterData = Resources.Load<ChapterData>(DataPath + "Outro"),
                },
                new ChapterGymRoom()
                {
                    StateName = "GymRoom",
                    ChapterData = Resources.Load<ChapterData>("Data/Scenes Datas/Others/GymRoom"),
                }
            };

            Chapters = new StateMachine(chapters, UpdateMode.Update, GameState.gameArguments);
        }

        public static void ChangeChapter(ChapterEnum chapter)
        {
            Chapters.SetState(GetChapterKey(chapter), GameState.gameArguments);
            GameInfo.Instance.Refresh();
        }

        public static T GetChapter<T>(ChapterEnum chapterEnum) where T : Chapter
        {
            return Chapters.GetState(GetChapterKey(chapterEnum)) as T;
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
                ChapterEnum.GYMROOM => "GymRoom",
                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
            };
        }
    }

    public enum ChapterEnum
    {
        GYMROOM,
        
        INTRO,
        CHAPTER_ONE,
        CHAPTER_TWO,
        CHAPTER_THREE,
        OUTRO,
    }
}
