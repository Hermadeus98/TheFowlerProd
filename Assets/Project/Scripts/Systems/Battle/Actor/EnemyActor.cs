using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
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

        [SerializeField] private  GameObject bossIcon;
        [SerializeField] private  RectTransform fill, background, preview;
        [SerializeField] private FillBar FillBar, FillBarTrashMob, FillBarBoss;

        protected override void OnAwake()
        {
            base.OnAwake();
            SetHealthBar();
        }

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

            if (BattleActorData.actorType == Spell.SpellTypeEnum.NULL)
                TypeIcon.transform.parent.gameObject.SetActive(false);
            else
                TypeIcon.Refresh(BattleActorData.actorType);
            
            StateIcons.HideAll();
        }

        private void SetHealthBar()
        {
            switch (BattleActorData.enemyType)
            {
                case BattleActorData.EnemyType.MOB:
                    FillBar.gameObject.SetActive(true);
                    FillBarBoss.gameObject.SetActive(false);
                    FillBarTrashMob.gameObject.SetActive(false);
                    Health.SetFillBar(FillBar);
                    break;
                case BattleActorData.EnemyType.TRASHMOB:
                    FillBar.gameObject.SetActive(false);
                    FillBarBoss.gameObject.SetActive(false);
                    FillBarTrashMob.gameObject.SetActive(true);
                    Health.SetFillBar(FillBarTrashMob);
                    break;
                case BattleActorData.EnemyType.BOSS:
                    FillBar.gameObject.SetActive(false);
                    FillBarBoss.gameObject.SetActive(true);
                    FillBarTrashMob.gameObject.SetActive(false);
                    Health.SetFillBar(FillBarBoss);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
