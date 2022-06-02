using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
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
        [TitleGroup("Main Settings")]
        [SerializeField] private bool isHardChoice = false;
        [SerializeField, ShowIf("@this.isHardChoice == true")] private HardChoice hardChoice;
        
        [TitleGroup("Logic")]
        [SerializeField, ShowIf("@this.isHardChoice == false")] private ComparableActor comparator_A;
        [SerializeField, ShowIf("@this.isHardChoice == false")] private ComparableComponent component;
        [SerializeField, ShowIf("@this.isHardChoice == false")] private OPERATOR Operator;
        [SerializeField, ShowIf("@this.isHardChoice == false")] private ComparableActor comparator_B;

        [SerializeField, ShowIf("@this.comparator_B == ComparableActor.VALUE")] private float percent = 0;

        [SerializeField]
        
        public Node NextNode;
        
        public bool Test()
        {
            if (isHardChoice)
            {
                switch (hardChoice)
                {
                    case HardChoice.AT_LEAST_AN_ENEMY_HAVE_BUFF:
                    {
                        var enemies = TargetSelector.GetAllEnemies().Where(w => w.BattleActorInfo.AttackBonus > 0).ToArray();
                        if (!enemies.IsNullOrEmpty())
                            return true;
                        
                        return false;
                    }
                    case HardChoice.AT_LEAST_AN_ENEMY_HAVE_DEFEND_BUFF:
                    {
                        var enemies = TargetSelector.GetAllEnemies().Where(w => w.BattleActorInfo.DefenseBonus > 0).ToArray();
                        if (!enemies.IsNullOrEmpty())
                            return true;
                        
                        return false;
                    }
                    case HardChoice.AT_LEAST_AN_ALLY_HAVE_TAUNT:
                    {
                        var allies = TargetSelector.GetAllAllies().Where(w => w.BattleActorInfo.isTaunt).ToArray();
                        if (allies.Length > 0)
                            return true;

                        return false;
                    }
                    case HardChoice.IM_TAUNT:
                    {
                        if (BattleManager.CurrentBattleActor.BattleActorInfo.isTaunt)
                            return true;
                        else return false;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                switch (component)
                {
                    case ComparableComponent.HEALTH:
                        return CompareHealth();
                    case ComparableComponent.TEAM_COUNT:
                        return CompareTeamCount();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        private bool CompareTeamCount()
        {
            switch (comparator_A)
            {
                case ComparableActor.ALL_ALLIES:
                    var allies = TargetSelector.GetAllAllies().Length;
                    switch (Operator)
                    {
                        case OPERATOR.EQUAL:
                            return allies == percent;
                        case OPERATOR.MINUS:
                            return allies < percent;
                        case OPERATOR.MINUS_EQUAL:
                            return allies <= percent;
                        case OPERATOR.PLUS:
                            return allies > percent;
                        case OPERATOR.PLUS_EQUAL:
                            return allies >= percent;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                case ComparableActor.ALL_ENEMIES:
                    var enemies = TargetSelector.GetAllEnemies().Length;
                    switch (Operator)
                    {
                        case OPERATOR.EQUAL:
                            return enemies == percent;
                        case OPERATOR.MINUS:
                            return enemies < percent;
                        case OPERATOR.MINUS_EQUAL:
                            return enemies <= percent;
                        case OPERATOR.PLUS:
                            return enemies > percent;
                        case OPERATOR.PLUS_EQUAL:
                            return enemies >= percent;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
                case OPERATOR.AT_LEAST_PLUS:
                    if (comparator_B == ComparableActor.VALUE)
                        return Comparator.AtLeastOneHaveMinusHealthThan(GetActors(comparator_A), percent);
                    else
                        return false;
                    break;
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
                case ComparableActor.ALL_ALLIES:
                    var allies = TargetSelector.GetAllAllies();
                    
                    if (!allies.IsNullOrEmpty())
                    {
                        return allies;
                    }
                    else
                    {
                        return null;
                    }
                case ComparableActor.ALL_ENEMIES:
                    var enemies = TargetSelector.GetAllEnemies();
                    
                    if (!enemies.IsNullOrEmpty())
                    {
                        return enemies;
                    }
                    else
                    {
                        return null;
                    }
                case ComparableActor.VALUE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(actor), actor, null);
            }

            return null;
        }
    }

    public enum ComparableActor
    {
        MY,
        ROBYN,
        ABI,
        PHOEBE,
        ALL_ALLIES,
        ALL_ENEMIES,
        VALUE,
    }

    public enum ComparableComponent
    {
        HEALTH,
        TEAM_COUNT,
        BUFF,
    }

    public enum OPERATOR
    {
        EQUAL,
        MINUS,
        MINUS_EQUAL,
        PLUS,
        PLUS_EQUAL,
        AT_LEAST_PLUS,
    }

    public enum HardChoice
    {
        AT_LEAST_AN_ENEMY_HAVE_BUFF,
        AT_LEAST_AN_ENEMY_HAVE_DEFEND_BUFF,
        AT_LEAST_AN_ALLY_HAVE_TAUNT,
        IM_TAUNT,
    }
}
