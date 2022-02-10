using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public static class TargetSelector
    {
        public static List<BattleActor> AvailableTargets;
        public static List<BattleActor> SelectedTargets;

        private static TargetTypeEnum targetType;
        private static int currentIndex;
        
        public static void Initialize(TargetTypeEnum targetType)
        {
            TargetSelector.targetType = targetType;
            AvailableTargets = new List<BattleActor>();
            SelectedTargets = new List<BattleActor>();
            currentIndex = 0;

            switch (targetType)
            {
                case TargetTypeEnum.SELF:
                    AvailableTargets.Add(GetSelf());
                    SelectedTargets.Add(GetSelf());
                    break;
                case TargetTypeEnum.SOLO_ENEMY:
                    AvailableTargets.AddRange(GetAllEnemiesOf(BattleManager.CurrentBattleActor));
                    break;
                case TargetTypeEnum.ALL_ENEMIES:
                    AvailableTargets.AddRange(GetAllEnemiesOf(BattleManager.CurrentBattleActor));
                    SelectedTargets.AddRange(GetAllEnemiesOf(BattleManager.CurrentBattleActor));
                    break;
                case TargetTypeEnum.SOLO_ALLY:
                    AvailableTargets.AddRange(GetAllAlliesOf(BattleManager.CurrentBattleActor));
                    break;
                case TargetTypeEnum.ALL_ALLIES:
                    AvailableTargets.AddRange(GetAllAlliesOf(BattleManager.CurrentBattleActor));
                    SelectedTargets.AddRange(GetAllAlliesOf(BattleManager.CurrentBattleActor));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetType), targetType, null);
            }
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public static bool Select(bool validate, out IEnumerable<BattleActor> battleActors)
        {
            switch (targetType)
            {
                case TargetTypeEnum.SELF:
                    if (validate)
                    {
                        battleActors = SelectedTargets;
                        return true;
                    }

                    battleActors = null;
                    return false;
                case TargetTypeEnum.SOLO_ENEMY:
                    if (validate)
                    {
                        battleActors = SelectedTargets;
                        return true;
                    }

                    battleActors = null;
                    return false;
                case TargetTypeEnum.ALL_ENEMIES:
                    battleActors = SelectedTargets;
                    return true;
                case TargetTypeEnum.SOLO_ALLY:
                    if (validate)
                    {
                        battleActors = SelectedTargets;
                        return true;
                    }

                    battleActors = null;
                    return false;
                case TargetTypeEnum.ALL_ALLIES:
                    if (validate)
                    {
                        battleActors = SelectedTargets;
                        return true;
                    }

                    battleActors = null;
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            battleActors = null;
            return false;
        }

        public static void Navigate(bool previous, bool next)
        {
            if (targetType == TargetTypeEnum.SOLO_ALLY || targetType == TargetTypeEnum.SOLO_ENEMY)
            {
                if (next)
                {
                    SelectNext();
                }

                if (previous)
                {
                    SelectPrevious();
                }
            }
        }
        
        private static void SelectNext()
        {
            currentIndex++;
            if (currentIndex >= AvailableTargets.Count)
            {
                currentIndex = AvailableTargets.Count - 1;
            }
            
            Select(AvailableTargets[currentIndex]);
        }

        private static void SelectPrevious()
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = 0;
            }

            Select(AvailableTargets[currentIndex]);
        }

        private static void Select(BattleActor actor)
        {
            if (SelectedTargets.Count > 0)
            {
                SelectedTargets.ForEach(w => Deselect(w));
            }
            
            Debug.Log("SELECT " + actor.BattleActorData.actorName);

            SelectedTargets.Clear();
            SelectedTargets.Add(actor);
        }

        private static void Deselect(BattleActor actor)
        {
            Debug.Log("DESELECT " + actor.BattleActorData.actorName);
        }
        
        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        public static BattleActor GetSelf()
        {
            return BattleManager.CurrentBattleActor;
        }

        public static BattleActor[] GetAllAlliesOf(BattleActor battleActor)
        {
            if (battleActor is AllyActor)
            {
                return GetAllAllies();
            }
            
            if (battleActor is EnemyActor)
            {
                return GetAllEnemies();
            }

            return null;
        }
        
        public static BattleActor[] GetAllEnemiesOf(BattleActor battleActor)
        {
            if (battleActor is AllyActor)
            {
                return GetAllEnemies();
            }
            
            if (battleActor is EnemyActor)
            {
                return GetAllAllies();
            }

            return null;
        }
        
        public static BattleActor[] GetAllEnemies()
        {
            return BattleManager.GetAllEnemies();
        }

        public static BattleActor[] GetAllAllies()
        {
            return BattleManager.GetAllAllies();
        }
        
    }

    public interface ITarget
    {
        public void OnTarget();
        public void OnEndTarget();
    }

    public enum TargetTypeEnum
    {
        SELF,
        SOLO_ENEMY,
        ALL_ENEMIES,
        SOLO_ALLY,
        ALL_ALLIES,
    }
}
