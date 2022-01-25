using System;

using QRCode;

using UnityEngine;

namespace TheFowler
{
    /// <summary>
    /// This Class manage all generic game state of the game.
    /// </summary>
    public static class GameState
    {
        public static StateMachine GameStates;

        public static bool GameIsPaused = false;

        public static GameArg gameArguments = new GameArg();

        public static void Initialize()
        {
            var states = new State[]
            {
                new LaunchGameState(){StateName = "launch"},
                new BattleGameState(){StateName = "battle"},
                new CinematicGameState(){StateName = "cinematic"},
                new ExplorationGameState(){StateName = "exploration"},
                new HarmonisationGameState(){StateName = "harmonisation"},
                new CutSceneGameState(){StateName = "cutscene"},
            };

            GameStates = new StateMachine(states, UpdateMode.Update, gameArguments);
        }

        public static void ChangeState(GameStateEnum state)
        {
            GameStates.SetState(GetStateKey(state), gameArguments);
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

        private static string GetStateKey(GameStateEnum key)
        {
            return key switch
            {
                GameStateEnum.LAUNCH => "launch",
                GameStateEnum.BATTLE => "battle",
                GameStateEnum.CINEMATIC => "cinematic",
                GameStateEnum.EXPLORATION => "exploration",
                GameStateEnum.HARMONISATION => "harmonisation",
                GameStateEnum.CUTSCENE => "cutscene",
                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
            };
        }
    }

    public class GameArg : EventArgs
    {
        public Chapter currentChapter;
    }
    
    public enum GameStateEnum
    {
        LAUNCH,
        BATTLE,
        CINEMATIC,
        EXPLORATION,
        HARMONISATION,
        CUTSCENE
    }
}
