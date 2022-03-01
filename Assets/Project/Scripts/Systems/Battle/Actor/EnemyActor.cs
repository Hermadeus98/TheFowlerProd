using System.Collections;
using System.Collections.Generic;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class EnemyActor : BattleActor
    {
        [SerializeField] private BehaviourTree brain;
        [SerializeField] private AIEnemy ai;

        public BehaviourTree Brain => brain;
        public AIEnemy AI => ai;

        protected override void OnStart()
        {
            base.OnStart();
            
            if(brain.IsNotNull())
                ai = new AIEnemy(brain, this);
        }

        public override void OnTurnStart()
        {
            base.OnTurnStart();
            actorTurn = new EnemyTurn();
            actorTurn.OnTurnStart();
        }

        [Button]
        private void Think() => ai.DebugAI();
    }
}
