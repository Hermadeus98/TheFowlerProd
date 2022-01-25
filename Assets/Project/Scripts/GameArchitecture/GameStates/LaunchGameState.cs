using System;
using QRCode;
using QRCode.Extensions;

namespace TheFowler
{
    public class LaunchGameState : State
    {
        public override void OnStateEnter(EventArgs arg)
        {
            QRDebug.Log("GameState Enter", FrenchPallet.CARROT, "Launch");
        }

        public override void OnStateExecute()
        {
            
        }

        public override void OnStateExit(EventArgs arg)
        {
            QRDebug.Log("GameState Exit", FrenchPallet.CARROT, "Launch");
        }
    }
}
