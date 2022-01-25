using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using QRCode.Extensions;
using UnityEngine;

namespace TheFowler
{
    public class BattleGameState : State
    {
        public override void OnStateEnter(EventArgs arg)
        {
            QRDebug.Log("GameState Enter", FrenchPallet.CARROT, "Battle");
        }

        public override void OnStateExecute()
        {
            QRDebug.Log("GameState Exit", FrenchPallet.CARROT, "Battle");
        }

        public override void OnStateExit(EventArgs arg)
        {
            
        }
    }
}
