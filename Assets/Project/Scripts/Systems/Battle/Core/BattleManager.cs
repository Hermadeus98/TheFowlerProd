using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class BattleManager
    {
        public static Battle CurrentBattle;
        public static ITurnActor CurrentTurnActor => CurrentBattle.TurnSystem.CurrentRound.currentTurnActor;

        public static Round CurrentRound => CurrentBattle.TurnSystem.CurrentRound;
    }
}
