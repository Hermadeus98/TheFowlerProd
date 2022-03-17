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

        public GameStateEnum InitialGameState;
        public GameInstructions InitialGameInstructions;

        public bool ShowChapterTitle = true;

        public GameplayPhaseEnum OnStartGamephase_Id;

        [TitleGroup("Audio")]
        public AudioGenericEnum audioEventOnChapterStart;
        public AudioGenericEnum audioEventOnChapterEnded;
    }
}
