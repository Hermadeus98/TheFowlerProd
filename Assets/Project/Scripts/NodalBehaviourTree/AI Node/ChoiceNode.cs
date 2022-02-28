using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class ChoiceNode : CompositeNode
    {
        [SerializeField] private AI_Choice[] Choices;

        [SerializeField] private Node DefaultNextNode;
        
        public void FindBestChoice(out Node result)
        {
            for (int i = 0; i < Choices.Length; i++)
            {
                if (Choices[i].Test())
                {
                    result = Choices[i].NextNode;
                    return;  
                }
            }

            result = DefaultNextNode;
        }

        protected override void OnStart()
        {
            
        }
        
        protected override State OnUpdate()
        {
            return State.Success;
        }

        protected override void OnStop()
        {
            
        }
    }

    [Serializable]
    public class AI_Choice
    {
        [SerializeField] private ComparableActor comparator_A;
        [SerializeField] private ComparableComponent component;
        [SerializeField] private OPERATOR Operator;
        [SerializeField] private ComparableActor comparator_B;

        public Node NextNode;
        
        public bool Test()
        {
            switch (component)
            {
                case ComparableComponent.HEALTH:
                    return CompareHealth();
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return false;
        }

        private bool CompareHealth()
        {
            switch (Operator)
            {
                case OPERATOR.EQUAL:
                    return false;
                case OPERATOR.MINUS:
                    return Comparator.HaveLessHealth(GetActors(comparator_A), GetActors(comparator_B));
                case OPERATOR.MINUS_EQUAL:
                    return false;
                case OPERATOR.PLUS:
                    return Comparator.HaveMoreHealth(GetActors(comparator_A), GetActors(comparator_B));
                case OPERATOR.PLUS_EQUAL:
                    return false;
                case OPERATOR.IS_LOW:
                    return false;
                case OPERATOR.IS_HIGH:
                    return false;
                case OPERATOR.IS_MIDDLE:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private BattleActor[] GetActors(ComparableActor actor)
        {
            switch (actor)
            {
                case ComparableActor.MY:
                    return new[] {BattleManager.CurrentBattleActor};
                case ComparableActor.ROBYN:
                    if (BattleManager.CurrentBattle.robyn != null)
                    {
                        return new[] {BattleManager.CurrentBattle.robyn};
                    }
                    else
                    {
                        return null;
                    }
                case ComparableActor.ABI:
                    if (BattleManager.CurrentBattle.abi != null)
                    {
                        return new[] {BattleManager.CurrentBattle.abi};
                    }
                    else
                    {
                        return null;
                    }
                case ComparableActor.PHOEBE:
                    if (BattleManager.CurrentBattle.phoebe != null)
                    {
                        return new[] {BattleManager.CurrentBattle.phoebe};
                    }
                    else
                    {
                        return null;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(actor), actor, null);
            }
        }
    }

    public enum ComparableActor
    {
        MY,
        ROBYN,
        ABI,
        PHOEBE
    }

    public enum ComparableComponent
    {
        HEALTH,
    }

    public enum OPERATOR
    {
        EQUAL,
        MINUS,
        MINUS_EQUAL,
        PLUS,
        PLUS_EQUAL,
        IS_LOW,
        IS_HIGH,
        IS_MIDDLE,
    }
}
