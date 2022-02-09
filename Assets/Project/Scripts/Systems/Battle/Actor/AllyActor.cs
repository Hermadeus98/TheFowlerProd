using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class AllyActor : BattleActor
    {
        
        public override void OnTurnStart()
        {
            base.OnTurnStart();
            actorTurn = new PlayerTurn();
            actorTurn.OnTurnStart();
        }
    }
}
