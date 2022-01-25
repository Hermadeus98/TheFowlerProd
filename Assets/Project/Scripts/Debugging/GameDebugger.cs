using QRCode.Extensions;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TheFowler
{
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
    }
}
