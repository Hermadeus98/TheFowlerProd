using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class EnemyActor : BattleActor
    {
        public override void OnTurnStart()
        {
            base.OnTurnStart();
            EnemyTurn.Initialize();
            EnemyTurn.Play();
        }
    }
}
