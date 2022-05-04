using System;
using System.Collections.Generic;
using System.Linq;

namespace TheFowler
{
    public class BattleManager
    {
        public static Battle CurrentBattle;
        public static bool IsReducingCD { get; set; }
        public static ITurnActor CurrentTurnActor
        {
            get
            {
                if (CurrentBattle.TurnSystem.CurrentRound.overrideTurnActor == null)
                {
                    return CurrentBattle.TurnSystem.CurrentRound.currentTurnActor;
                }
                else
                {
                    return CurrentBattle.TurnSystem.CurrentRound.overrideTurnActor;
                }
            }
        }
        public static BattleActor CurrentBattleActor => CurrentTurnActor as BattleActor;

        public static Round CurrentRound => CurrentBattle.TurnSystem.CurrentRound;

        public static bool IsAllyTurn => CurrentTurnActor is AllyActor;
        public static bool IsEnemyTurn => CurrentTurnActor is EnemyActor;

        public static Action<BattleStateEnum> OnBattleStateChange;
        public static Action OnTurnChanged;

        public static BattleActor[] GetAllAllies() => CurrentBattle.Allies.Where(w => !w.BattleActorInfo.isDeath).ToArray();
        public static BattleActor[] GetAllEnemies() => CurrentBattle.Enemies.Where(w => !w.BattleActorInfo.isDeath).ToArray();

        public static bool lastTurnWasAlly { get; set; } = false;
        
        public static bool lastTurnWasEnemiesTurn { get; set; } = false;
        
        public static List<BattleActor> lastTouchedActors = new List<BattleActor>();

    }
}
