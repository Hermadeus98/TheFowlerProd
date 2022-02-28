using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using QRCode.Extensions;
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

        [SerializeField, ReadOnly] private Node nextNode;

        [ShowInInspector, ReadOnly] public Spell SelectedSpell { get; private set; }

        public AIEnemy(BehaviourTree behaviourTree)
        {
            brain = behaviourTree;
            currentNode = brain.rootNode;
        }
        
        [Button]
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

            if (currentNode is CastNode castNode)
            {
                QRDebug.Log("AI", FrenchPallet.GREEN_SEA, $"RESULT FOUNDED -> {castNode.NodeName}");
                SelectedSpell = castNode.spellToCast;
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

    public enum Intention
    {
        ALEATOIRE = 0,
        AGRESSIVE = 1, //Cherche à infliger des dégats
    }

    public enum TargetIntention
    {
        NONE = 0,
        ALL = 1,
        WEAKER_ALLY = 2,
        STRONGER_ALLY = 3,
        WEAKER_ENEMY = 4,
        STRONGER_ENEMY = 5,
        ROBYN = 6,
        ABI = 7,
        PHOEBE = 8,
    }
}
