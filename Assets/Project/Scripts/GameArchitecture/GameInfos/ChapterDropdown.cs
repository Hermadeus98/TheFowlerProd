using System.Collections;
using System.Collections.Generic;
using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheFowler
{
    public class ChapterDropdown : SerializedMonoBehaviour
    {
        public void LoadScenes(int value)
        {
            if (value == 0)
            {
                ChapterManager.ChangeChapter(ChapterEnum.INTRO);
            }
            
            if (value == 1)
            {
                ChapterManager.ChangeChapter(ChapterEnum.CHAPTER_ONE);
            }
            
            if (value == 2)
            {
                ChapterManager.ChangeChapter(ChapterEnum.CHAPTER_TWO);
            }
            
            if (value == 3)
            {
                ChapterManager.ChangeChapter(ChapterEnum.CHAPTER_THREE);
            }
            
            if (value == 4)
            {
                ChapterManager.ChangeChapter(ChapterEnum.OUTRO);
            }

            if (value == 5)
            {
                ChapterManager.ChangeChapter(ChapterEnum.GYMROOM);
            }
        }
    }
}
