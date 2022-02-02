using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using QRCode;
using QRCode.Extensions;
using QRCode.Utils;
using UnityEngine;
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
        
        public static void LoadSceneAdditive(params SceneReference[] sceneReferences)
        {
            for (int i = 0; i < sceneReferences.Length; i++)
            {
                var sceneName = SceneManager.GetSceneByPath(sceneReferences[i].ScenePath).name;
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
        }

        public static void LoadSceneAdditive(string key, Action OnComplete)
        {
            Coroutiner.Play(IELoadSceneAdditive(key, OnComplete));
        }

        private static IEnumerator IELoadSceneAdditive(string key, Action OnComplete)
        {
            var db = SceneDatabase.Instance;
            var batch = db.GetBatch(key);

            AsyncOperation[] operations = new AsyncOperation[batch.sceneReferences.Length];
            
            for (int i = 0; i < batch.sceneReferences.Length; i++)
            {
                operations[i] = SceneManager.LoadSceneAsync(batch.sceneReferences[i].ScenePath, LoadSceneMode.Additive);
            }

            while (operations.All(w => !w.isDone))
            {
                QRDebug.Log("Scenes loading...", FrenchPallet.CLOUDS, $"{key} is loading.");
                yield return null;
            }

            yield return new WaitForEndOfFrame();
            
            QRDebug.Log("Scenes loaded", FrenchPallet.CLOUDS, $"{key} is loaded.");

            OnComplete?.Invoke();
        }

        public static void UnloadScene(params SceneReference[] sceneReferences)
        {
            for (int i = 0; i < sceneReferences.Length; i++)
            {
                var sceneName = SceneManager.GetSceneByPath(sceneReferences[i].ScenePath).name;
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }
        
        public static void UnloadScene(params SceneEnum[] scenes)
        {
            for (int i = 0; i < scenes.Length; i++)
            {
                SceneManager.UnloadSceneAsync(SceneLoader.GetSceneKey(scenes[i]));
            }
        }
        
        public static void UnloadScene(string key)
        {
            var db = SceneDatabase.Instance;
            var batch = db.GetBatch(key);
            for (int i = 0; i < batch.sceneReferences.Length; i++)
            {
                var sceneName = SceneManager.GetSceneByPath(batch.sceneReferences[i].ScenePath).name;
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }
    }
}
