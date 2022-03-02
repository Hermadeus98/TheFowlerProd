using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using QRCode.Extensions;
using QRCode.Utils;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheFowler
{
    [Serializable]
    public class AIEnemy
    {
        public BehaviourTree brain;
        public Node currentNode;
        public EnemyActor referedEnemy;

        [SerializeField, ReadOnly] private Node nextNode;

        [ShowInInspector, ReadOnly] public Spell SelectedSpell { get; private set; }

        public AIEnemy(BehaviourTree behaviourTree, EnemyActor enemyActor)
        {
            brain = behaviourTree;
            currentNode = brain.rootNode;
            referedEnemy = enemyActor;
        }

        [Button]
        public void DebugAI()
        {
            BattleManager.CurrentBattle.TurnSystem.CurrentRound.currentTurnActor = referedEnemy;
            StartThink();

            //Coroutiner.Play(referedEnemy.AI.SelectedSpell.Cast(referedEnemy,
            //    TargetSelector.SelectedTargets.ToArray()));
        }
        
        public void StartThink()
        {
            currentNode = brain.rootNode;
            Think();
        }
        
        public void Think()
        {
            if (currentNode is ChoiceNode choiceNode)
            {
                QRDebug.Log("AI", FrenchPallet.GREEN_SEA, $"ITERATE IN {choiceNode.NodeName}");
                choiceNode.FindBestChoice(out nextNode);
                Iterate();
                return;
            }

            if (currentNode is DebugLogNode log)
            {
                QRDebug.Log("AI", FrenchPallet.GREEN_SEA, $"ITERATE IN {log.NodeName}");
                log.DebugLog();
                return;
            }

            if (currentNode is TargetNode targetNode)
            {
                QRDebug.Log("AI", FrenchPallet.GREEN_SEA, $"ITERATE IN {targetNode.NodeName}");
                targetNode.SelectTarget();
                TargetSelector.DebugSelectedTargets();
                if (targetNode.children.IsNullOrEmpty())
                {
                    QRDebug.Log("AI", FrenchPallet.GREEN_SEA, $"NO ITERATION FOUNDED");
                    return;
                }
                nextNode = targetNode.children[Random.Range(0, targetNode.children.Count)];
                Iterate();
                return;
            }

            if (currentNode is AleatoryChoiceNode aleatoryChoiceNode)
            {
                QRDebug.Log("AI", FrenchPallet.GREEN_SEA, $"ITERATE ALEATORY IN {aleatoryChoiceNode.NodeName}");
                nextNode = aleatoryChoiceNode.GetRandomNode();
                Iterate();
                return;
            }
            
            if (currentNode is CastNode castNode)
            {
                QRDebug.Log("AI", FrenchPallet.GREEN_SEA, $"RESULT FOUNDED -> {castNode.NodeName}");
                SelectedSpell = castNode.GetRandomSpell();
                return;
            }
        }

        [Button]
        private void Iterate()
        {
            if(nextNode == null)
                return;
            
            currentNode = nextNode;
            Think();
        }
    }
}
