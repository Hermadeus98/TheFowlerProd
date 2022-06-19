using System;
using MoreMountains.Feedbacks;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TheFowler
{
    /// <summary>
    /// This class is used to ez debug in scene.
    /// </summary>
    public class GameDebugger : SerializedMonoBehaviour
    {
        [Button]
        private void ChangeGameState(GameStateEnum gameState) => GameState.ChangeState(gameState);

        [Button]
        private void ChangeChapter(ChapterEnum chapter) => ChapterManager.ChangeChapter(chapter);

        [Button]
        private void SelectRobyn()
        {
            if (Player.Robyn.IsNotNull())
            {
#if UNITY_EDITOR
                Selection.activeGameObject = Player.Robyn.gameObject;
#endif
            }
        }

        private bool itPause;


        [Button]
        private void DesactiveAllActorsInBattles()
        {
            var battles = FindObjectsOfType<Battle>();

            foreach (var battle in battles)
            {
                battle.DesactivateAllActors();
            }
        }
    }
}
