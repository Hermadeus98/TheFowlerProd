using System.Collections;
using System.Collections.Generic;
using QRCode;
using QRCode.Utils;
using UnityEngine;

namespace TheFowler
{
    public class PlayerTurn : Turn
    {
        public override void OnTurnStart()
        {
            base.OnTurnStart();
            Player.SelectedSpell = null;
        }
    }
}
