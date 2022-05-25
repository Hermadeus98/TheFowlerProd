using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.Tools;
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
        public AnimatedIcon weak, resist;
        public BehaviourTree Brain => brain;
        public AIEnemy AI => ai;

        public CanvasGroup UI;
        public RectTransform canvasWorld;

        [SerializeField] private  GameObject bossIcon;
        [SerializeField] private  RectTransform fill, background, preview;
        [SerializeField] private FillBar FillBar, FillBarTrashMob, FillBarBoss;

        [SerializeField] private HelmetBinding[] HelmetBinding;
        [SerializeField] private SkinnedMeshRenderer[] Contrebasses;

        public Transform spawnBasicAttack;

        public Color beakColor, featherColor, clawColor;

        protected override void OnAwake()
        {
            base.OnAwake();
            SetHealthBar();
        }

        public override void InitializeComponents()
        {
            base.InitializeComponents();
            StartCoroutine(SetCanvasWorld());
        }

        IEnumerator SetCanvasWorld()
        {
            if (BattleManager.CurrentBattle.useUIOnPivot)
            {
                canvasWorld.SetParent(BattleManager.CurrentBattle.UIPivot, true);
                yield return new WaitForEndOfFrame();
                canvasWorld.localPosition = new Vector3(canvasWorld.localPosition.x, -.33f, 0f);
                yield return new WaitForSeconds(.1f);
                BattleManager.CurrentBattle.UIPivot.GetComponent<BillBoard>().enabled = true;
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

            brain = BattleActorData.brain;
            
            if(brain.IsNotNull())
                ai = new AIEnemy(brain, this);
            
            Health.onDeath.AddListener(delegate
            {
                weak.Hide();
                resist.Hide();
            });

            if (BattleActorData.actorType == Spell.SpellTypeEnum.NULL)
                TypeIcon.transform.parent.gameObject.SetActive(false);
            else
                TypeIcon.Refresh(BattleActorData.actorType);
            
            StateIcons.HideAll();
            SetHelmet();
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
            if(this == BattleManager.GetAllEnemies()[0])
            {
                BattleManager.CurrentBattle.BattleGameLogComponent.ResetComponent();
            }

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
            
            punchline.RegisterPunchline(PunchlineCallback.DEATH);
        }

        private void SetHelmet()
        {
            HelmetBinding.First(w => w.Archetype == BattleActorData.archetype).Helmets
                .ForEach(w => w.gameObject.SetActive(true));

            for (int i = 0; i < Contrebasses.Length; i++)
            {
                if(Contrebasses[i].gameObject.activeSelf == true)
                {
                    if (i == 0) return;

                    Material newMat = Contrebasses[i].material;
                    Contrebasses[i].material = newMat;

                    switch (BattleActorData.actorType)
                    {
                        case Spell.SpellTypeEnum.BEAK:
                            Contrebasses[i].material.SetColor("_ColorEMISSIVE", beakColor);
                            break;
                        case Spell.SpellTypeEnum.CLAW:
                            Contrebasses[i].material.SetColor("_ColorEMISSIVE", clawColor);
                            break;
                        case Spell.SpellTypeEnum.FEATHER:
                            Contrebasses[i].material.SetColor("_ColorEMISSIVE", featherColor);
                            break;
                    }
                    
                }
            }
        }

        
    }

    [Serializable]
    public class HelmetBinding
    {
        public BattleActorData.Archetype Archetype;
        public GameObject[] Helmets;
    }
}
