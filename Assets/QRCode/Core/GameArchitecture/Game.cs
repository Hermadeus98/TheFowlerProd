using QRCode.Extensions;
using UnityEngine;

namespace QRCode
{
    /// <summary>
    /// Callback Gestion
    /// Scene Management
    /// GameMode & GameState References
    /// </summary>
    public static class Game
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            GameEvent.AddListener(GameEventAddressCore.OnGameStart, () => QRDebug.Log("GameState", FrenchPallet.CARROT, "Start Game"));
            GameEvent.AddListener(GameEventAddressCore.OnGamePause, () => QRDebug.Log("GameState", FrenchPallet.CARROT, "Pause Game"));
            GameEvent.AddListener(GameEventAddressCore.OnGameUnpause, () => QRDebug.Log("GameState", FrenchPallet.CARROT, "Unpause Game"));
            GameEvent.AddListener(GameEventAddressCore.SetDependancies, () => QRDebug.Log("GameState", FrenchPallet.CARROT, "Set Dependancies"));
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void SetDependancies()
        {
            GameEvent.Broadcast(GameEventAddressCore.SetDependancies);
        }
    }
}
