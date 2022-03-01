using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class ChoiceNode : CompositeNode
    {
        [TitleGroup("Choices")]
        [SerializeField] private AI_Choice[] Choices;

        [TitleGroup("Choices")]
        [SerializeField] private Node DefaultNextNode;
        
        public void FindBestChoice(out Node result)
        {
            for (int i = 0; i < Choices.Length; i++)
            {
                Debug.Log(Choices[i].Test());

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
        [SerializeField, ShowIf("@this.comparator_B == ComparableActor.VALUE")] private float percent = 0;

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
                    if (comparator_B == ComparableActor.VALUE)
                        return Comparator.HaveEqualHealth(GetActors(comparator_A), percent);
                    else
                        return Comparator.HaveEqualHealth(GetActors(comparator_A), GetActors(comparator_B));
                case OPERATOR.MINUS:
                    if (comparator_B == ComparableActor.VALUE)
                        return Comparator.HaveLessHealth(GetActors(comparator_A), percent);
                    else
                        return Comparator.HaveLessHealth(GetActors(comparator_A), GetActors(comparator_B));
                case OPERATOR.MINUS_EQUAL:
                    if (comparator_B == ComparableActor.VALUE)
                        return Comparator.HaveLessEqualHealth(GetActors(comparator_A), percent);
                    else
                        return Comparator.HaveLessEqualHealth(GetActors(comparator_A), GetActors(comparator_B));
                case OPERATOR.PLUS:
                    if (comparator_B == ComparableActor.VALUE)
                        return Comparator.HaveMoreHealth(GetActors(comparator_A), percent);
                    else 
                        return Comparator.HaveMoreHealth(GetActors(comparator_A), GetActors(comparator_B));
                case OPERATOR.PLUS_EQUAL:
                    if (comparator_B == ComparableActor.VALUE)
                        return Comparator.HaveMoreEqualHealth(GetActors(comparator_A), percent);
                    else 
                        return Comparator.HaveMoreEqualHealth(GetActors(comparator_A), GetActors(comparator_B));
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
        PHOEBE,
        VALUE,
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
        PLUS_EQUAL
    }
}
