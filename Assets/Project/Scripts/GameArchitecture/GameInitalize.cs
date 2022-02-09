using System.Collections;
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
        [SerializeField] private bool loadUI;
        
        private IEnumerator Start()
        {
            //--<REMOTE SETTINGS>
            RemoteSettingsManager.Fetch();
            
            //--<SCENE UI>
            if(loadUI) Game.LoadSceneAdditive(SceneEnum.Scene_UI);

            yield return new WaitForEndOfFrame();
            
            //--<SYSTEM>
            GameEventInternal.Init();
            Game.Initialize();
            Game.SetDependancies();
            
            //--<GAME STATES>
            GameState.Initialize();
            ChapterManager.Initialize();

            if (loadGymRoom)
            {
                SceneManager.LoadScene("Scene_GymRoom", LoadSceneMode.Additive);
                GameState.gameArguments.noloadingChapter = true;
                Player.Initialize();
            }
            else if (loadChapter)
            {
                //--<CHAPTER>
                ChapterManager.ChangeChapter(startAtChapter);
            }
            else if (loadPlayer)
            {
                GameState.gameArguments.noloadingChapter = true;
                Player.Initialize();
            }
        }
    }
}
