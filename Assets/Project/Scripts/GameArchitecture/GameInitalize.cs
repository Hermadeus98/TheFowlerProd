using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheFowler
{
    /// <summary>
    /// This class manage the initilisation of the game
    /// </summary>
    public class GameInitalize : SerializedMonoBehaviour
    {
        [TitleGroup("Chapter Settings")]
        [SerializeField, Tooltip(ToolTips.TT_loadChapter)] private bool loadChapter;
        [SerializeField, ShowIf("@this.loadChapter")] private ChapterEnum startAtChapter;

        [TitleGroup("Settings")]
        [SerializeField, Tooltip(ToolTips.TT_loadGymRoom)] private bool loadGymRoom;
        [SerializeField, Tooltip(ToolTips.TT_loadPlayer)] private bool loadPlayer;
        
        private void Start()
        {
            //--<SYSTEM>
            GameEventInternal.Init();
            Game.Initialize();
            Game.SetDependancies();
            
            //--<GAME STATES>
            GameState.Initialize();

            if (loadGymRoom)
            {
                SceneManager.LoadScene("Scene_GymRoom", LoadSceneMode.Additive);
                Player.Initialize();
            }
            else if (loadChapter)
            {
                //--<CHAPTER>
                ChapterManager.Initialize();
                ChapterManager.ChangeChapter(startAtChapter);
            }
            else if (loadPlayer)
            {
                Player.Initialize();
            }
        }
    }
}
