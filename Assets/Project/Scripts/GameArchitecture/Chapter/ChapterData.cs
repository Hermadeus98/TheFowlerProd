using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu(menuName = CreateAssetMenuPath.chapterData)]
    public class ChapterData : SerializedScriptableObject
    {
        public string ChapterName;

        public bool LoadRobyn = true;
        public bool LoadAbigael = false;
        public bool LoadPhoebe = false;
    }
}
