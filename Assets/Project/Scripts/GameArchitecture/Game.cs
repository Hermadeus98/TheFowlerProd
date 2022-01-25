using QRCode;
using QRCode.Extensions;
using UnityEngine.SceneManagement;

namespace TheFowler
{
    /// <summary>
    /// Callback Gestion
    /// Scene Management
    /// GameMode & GameState References
    /// </summary>
    public static class Game
    {
        public static void Initialize()
        {
            GameEvent.AddListener(GameEventAddressCore.OnGameStart, () => QRDebug.Log("GameState", FrenchPallet.CARROT, "Start Game"));
            GameEvent.AddListener(GameEventAddressCore.OnGamePause, () => QRDebug.Log("GameState", FrenchPallet.CARROT, "Pause Game"));
            GameEvent.AddListener(GameEventAddressCore.OnGameUnpause, () => QRDebug.Log("GameState", FrenchPallet.CARROT, "Unpause Game"));
            GameEvent.AddListener(GameEventAddressCore.SetDependancies, () => QRDebug.Log("GameState", FrenchPallet.CARROT, "Set Dependancies"));
        }

        public static void SetDependancies()
        {
            GameEvent.Broadcast(GameEventAddressCore.SetDependancies);
        }

        public static void LoadSceneAdditive(params SceneEnum[] scenes)
        {
            for (int i = 0; i < scenes.Length; i++)
            {
                SceneManager.LoadSceneAsync(SceneLoader.GetSceneKey(scenes[i]), LoadSceneMode.Additive);
            }
        }
        
        public static void UnloadScene(params SceneEnum[] scenes)
        {
            for (int i = 0; i < scenes.Length; i++)
            {
                SceneManager.UnloadSceneAsync(SceneLoader.GetSceneKey(scenes[i]));
            }
        }
    }
}
