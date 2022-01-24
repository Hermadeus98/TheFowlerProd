using System;
using UnityEngine;

namespace QRCode
{
    /// <summary>
    /// cette class gère les etats du jeux.
    /// </summary>
    public static class GameState
    {
        public static StateMachine GameStates;

        public static bool GameIsPaused = false;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Initialize()
        {
            var states = new State[]
            {
                new LaunchGameState(){StateName = "launch"},
            };

            GameStates = new StateMachine(states, "launch", UpdateMode.Update, EventArgs.Empty);
        }
        
        public static void StartGame()
        {
            GameEvent.Broadcast(GameEventAddressCore.OnGameStart);
        }

        public static void Pause()
        {
            GameIsPaused = true;
            GameEvent.Broadcast(GameEventAddressCore.OnGamePause);
        }

        public static void Unpause()
        {
            GameIsPaused = false;
            GameEvent.Broadcast(GameEventAddressCore.OnGameUnpause);
        }
    }
}
