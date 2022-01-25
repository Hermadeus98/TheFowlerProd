using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using QRCode.Extensions;
using UnityEngine;

namespace TheFowler
{
    public class HarmonisationGameState : State
    {
        public override void OnStateEnter(EventArgs arg)
        {
            QRDebug.Log("GameState Enter", FrenchPallet.CARROT, "Harmonisation");
        }

        public override void OnStateExecute()
        {
            
        }

        public override void OnStateExit(EventArgs arg)
        {
            QRDebug.Log("GameState Exit", FrenchPallet.CARROT, "Harmonisation");
        }
    }
}
