using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public static class Fury
    {
        public static bool IsInFury { get; private set; }
        
        public static void PlayFury()
        {
            IsInFury = true;
            BattleManager.CurrentBattle.BattleState.SetState("Fury", EventArgs.Empty);
            BattleManager.CurrentRound.BlockNextTurn();
        }

        public static void StopFury()
        {
            IsInFury = false;
        }
    }
}
