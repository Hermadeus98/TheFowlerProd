using System.Collections;
using System.Collections.Generic;
using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class EnemyActor : BattleActor
    {
        [SerializeField] private BehaviourTree brain;
        [SerializeField] private AIEnemy ai;
        [SerializeField] private TypeIcon TypeIcon;
        public GameObject weak, resist;

        public BehaviourTree Brain => brain;
        public AIEnemy AI => ai;

        public CanvasGroup UI;

        protected override void OnStart()
        {
            base.OnStart();

            brain = BattleActorData.brain;
            
            if(brain.IsNotNull())
                ai = new AIEnemy(brain, this);
            
            Health.onDeath.AddListener(delegate
            {
                weak.SetActive(false);
                resist.SetActive(false);
            });
            
            TypeIcon.Refresh(BattleActorData.actorType);
            StateIcons.HideAll();
        }

        public override void OnTurnStart()
        {
            base.OnTurnStart();
            actorTurn = new EnemyTurn();
            actorTurn.OnTurnStart();
        }

        public void SetAI(AIEnemy newAI)
        {
            brain = newAI.brain;
            ai = newAI; 
        }

        [Button]
        private void Think() => ai.DebugAI();

        public override void OnDeath()
        {
            base.OnDeath();
            BattleActorAnimator.Death();

            BattleManager.CurrentBattle.EnemyDeathCount++;
            Fury.PlayBreakDown();
            //Fury.AddFuryPoint(15);
        }
    }
}
