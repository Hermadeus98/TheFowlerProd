using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class SceneLoaderComponent : SerializedMonoBehaviour
    {
        [SerializeField] private ChapterEnum chapterToLoad;

        public void ChangeChapter()
        {
            ChapterManager.ChangeChapter(chapterToLoad);
        }
    }
}
