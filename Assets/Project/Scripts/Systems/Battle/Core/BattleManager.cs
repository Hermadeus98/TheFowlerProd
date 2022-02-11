using System;
using System.Linq;

namespace TheFowler
{
    public class BattleManager
    {
        public static Battle CurrentBattle;
        public static ITurnActor CurrentTurnActor => CurrentBattle.TurnSystem.CurrentRound.currentTurnActor;
        public static BattleActor CurrentBattleActor => CurrentBattle.TurnSystem.CurrentRound.currentTurnActor as BattleActor;

        public static Round CurrentRound => CurrentBattle.TurnSystem.CurrentRound;

        public static bool IsAllyTurn => CurrentTurnActor is AllyActor;
        public static bool IsEnemyTurn => CurrentTurnActor is EnemyActor;

        public static Action<BattleStateEnum> OnBattleStateChange;
        public static Action OnTurnChanged;

        public static BattleActor[] GetAllAllies() => CurrentBattle.Allies.Where(w => !w.BattleActorInfo.isDeath).ToArray();
        public static BattleActor[] GetAllEnemies() => CurrentBattle.Enemies.Where(w => !w.BattleActorInfo.isDeath).ToArray();
    }
}
