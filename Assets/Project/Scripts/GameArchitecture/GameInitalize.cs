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
        [TitleGroup("Settings")]
        [SerializeField, ShowIf("@this.loadChapter")] private ChapterEnum startAtChapter;

        [TitleGroup("Settings")]
        [SerializeField, Tooltip(ToolTips.TT_loadGymRoom)] private bool loadGymRoom;
        [TitleGroup("Settings")]
        [SerializeField, Tooltip(ToolTips.TT_loadPlayer)] private bool loadPlayer;

        [TitleGroup("Settings")] [SerializeField]
        private bool loadMainMenu;
        
        [TitleGroup("Difficulty Settings")] [SerializeField]
        private DifficultyEnum startDifficulty = DifficultyEnum.TEST;
        
        private IEnumerator Start()
        {
            //--<REMOTE SETTINGS>
            RemoteSettingsManager.Fetch();
            
            //--<SCENE UI>
            if (loadMainMenu)
                SceneManager.LoadSceneAsync("Scene_MenuPrincipal", LoadSceneMode.Additive);
            Game.LoadSceneAdditive(SceneEnum.Scene_UI);

            yield return new WaitForEndOfFrame();
            
            //--<SYSTEM>
            GameEventInternal.Init();
            Game.Initialize();
            Game.SetDependancies();
            
            //--<DIFFICULTY>
            DifficultyManager.ChangeDifficulty(startDifficulty);
            
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
