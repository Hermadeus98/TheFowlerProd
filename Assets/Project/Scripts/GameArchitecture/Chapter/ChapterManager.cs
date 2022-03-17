using System;
using QRCode;
using UnityEngine;

namespace TheFowler
{
    public static class ChapterManager
    {
        public static StateMachine Chapters;

        private const string DataPath = "Data/Scenes Datas/Chapters/";

        public static Action<Chapter> onChapterChange;
        public static Action<Chapter> onChapterLoaded;

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
                new ChapterTwoPartOne()
                {
                    StateName = "ChapterTwo - Part 1",
                    ChapterData = Resources.Load<ChapterData>(DataPath + "Chapter Two - Part 1"),
                },
                new ChapterTwoPartTwo()
                {
                    StateName = "ChapterTwo - Part 2",
                    ChapterData = Resources.Load<ChapterData>(DataPath + "Chapter Two - Part 2"),
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
            BlackPanel.Instance.Show();
            
            if(((Chapter) Chapters.CurrentState) != null)
            {
                SoundManager.PlaySound(((Chapter) Chapters.CurrentState).ChapterData.audioEventOnChapterEnded, null);
            }
            
            Chapters.SetState(GetChapterKey(chapter), GameState.gameArguments);
            onChapterChange?.Invoke(Chapters.CurrentState as Chapter);
        }

        public static void GoChapterOne() => ChangeChapter(ChapterEnum.CHAPTER_ONE);
        public static void GoChapterTwo() => ChangeChapter(ChapterEnum.CHAPTER_TWO_PART1);
        public static void GoChapterThree() => ChangeChapter(ChapterEnum.CHAPTER_THREE);

        public static T GetChapter<T>(ChapterEnum chapterEnum) where T : Chapter
        {
            return Chapters.GetState(GetChapterKey(chapterEnum)) as T;
        }

        private static string GetChapterKey(ChapterEnum key)
        {
            return key switch
            {
                ChapterEnum.CHAPTER_ONE => "ChapterOne",
                ChapterEnum.CHAPTER_TWO_PART1 => "ChapterTwo - Part 1",
                ChapterEnum.CHAPTER_TWO_PART2 => "ChapterTwo - Part 2",
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
        CHAPTER_TWO_PART1,
        CHAPTER_TWO_PART2,
        CHAPTER_THREE,
        OUTRO,
    }
}
