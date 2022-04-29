using System.Collections;
using System.Collections.Generic;
using QRCode.Utils;
using UnityEngine;

namespace TheFowler
{
    public class EnemyTurn : Turn
    {
        public override void OnTurnStart()
        {
            base.OnTurnStart();
            BattleManager.CurrentBattle.BattleGameLogComponent.HideGameLogView();
        }
    }
}
