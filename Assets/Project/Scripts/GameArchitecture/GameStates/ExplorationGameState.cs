using System;

using QRCode;
using QRCode.Extensions;

namespace TheFowler
{
    public class ExplorationGameState : State
    {
        public override void OnStateEnter(EventArgs arg)
        {
            QRDebug.Log("GameState Enter", FrenchPallet.CARROT, "Exploration");
        }

        public override void OnStateExecute()
        {
            QRDebug.Log("GameState Exit", FrenchPallet.CARROT, "Exploration");
        }

        public override void OnStateExit(EventArgs arg)
        {
            
        }
    }
}
