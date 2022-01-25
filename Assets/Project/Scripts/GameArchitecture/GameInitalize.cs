using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheFowler
{
    public class GameInitalize : SerializedMonoBehaviour
    {
        [SerializeField] private ChapterEnum startAtChapter;
        [SerializeField] private bool loadChapter;

        [SerializeField] private bool loadGymRoom;
        
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
        }
    }
}
