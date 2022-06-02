using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class TargetNode : CompositeNode
    {
        [TitleGroup("Target")]
        [SerializeField] private TargetIntention TargetIntention;

        [TitleGroup("Target")] [SerializeField]
        private SelectTargetWith With;

        [SerializeField] private float value;
        
        public void SelectTarget()
        {
            var taunt = BattleManager.CurrentBattleActor.GetBattleComponent<Taunt>();
            if (BattleManager.CurrentBattleActor.BattleActorInfo.isTaunt && !taunt.taunter.BattleActorInfo.isDeath && TargetIntention != TargetIntention.ALL)
            {
                taunt.taunter.SelectAsTarget();
                return;
            }
            
            //TargetSelector.Initialize(TargetTypeEnum.SELF);
            switch (TargetIntention)
            {
                case TargetIntention.NONE:
                    break;
                case TargetIntention.ALL:
                    TargetSelector.GetAllActors(BattleManager.CurrentBattleActor).SelectAsTargets();
                    break;
                case TargetIntention.WEAKER_ALLY:
                    TargetSelector.GetWeakerAlly().SelectAsTarget();
                    break;
                case TargetIntention.STRONGER_ALLY:
                    TargetSelector.GetStrongerAlly().SelectAsTarget();
                    break;
                case TargetIntention.WEAKER_ENEMY:
                    TargetSelector.GetWeakerEnemy().SelectAsTarget();
                    break;
                case TargetIntention.STRONGER_ENEMY:
                    TargetSelector.GetStrongerEnemy().SelectAsTarget();
                    break;
                case TargetIntention.ROBYN:
                    BattleManager.CurrentBattle.robyn?.SelectAsTarget();
                    break;
                case TargetIntention.ABI:
                    BattleManager.CurrentBattle.abi?.SelectAsTarget();
                    break;
                case TargetIntention.PHOEBE:
                    BattleManager.CurrentBattle.phoebe?.SelectAsTarget();
                    break;
                case TargetIntention.RANDOM_ALLY:
                    TargetSelector.GetRandomAlly().SelectAsTarget();
                    break;
                case TargetIntention.RANDOM_ENEMY:
                    TargetSelector.GetRandomEnemy().SelectAsTarget();
                    break;
                case TargetIntention.ALLY:
                    switch (With)
                    {
                        case SelectTargetWith.NONE:
                            break;
                        case SelectTargetWith.WITH_DEFEND_BUFF:
                            TargetSelector.GetAllAllies().First(w => w.BattleActorInfo.attackBonus > 0).SelectAsTarget();
                            break;
                        case SelectTargetWith.WITH_LESS_HEALTH_THAN:
                        {
                            var allies = TargetSelector.GetAllAllies();
                            for (int i = 0; i < allies.Length; i++)
                            {
                                if (Comparator.HaveLessHealth(new[] {allies[i]}, value))
                                {
                                    allies[i].SelectAsTarget();
                                    return;
                                }
                            }
                        }
                            break;
                        case SelectTargetWith.WITH_MORE_HEALTH_THAN:
                        {
                            var allies = TargetSelector.GetAllAllies();
                            for (int i = 0; i < allies.Length; i++)
                            {
                                if (Comparator.HaveMoreHealth(new[] {allies[i]}, value))
                                {
                                    allies[i].SelectAsTarget();
                                    return;
                                }
                            }
                        }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case TargetIntention.ENEMY:
                    switch (With)
                    {
                        case SelectTargetWith.NONE:
                            break;
                        case SelectTargetWith.WITH_DEFEND_BUFF:
                            TargetSelector.GetAllEnemies().First(w => w.BattleActorInfo.attackBonus > 0).SelectAsTarget();
                            break;
                        case SelectTargetWith.WITH_LESS_HEALTH_THAN:
                        {
                            var enemies = TargetSelector.GetAllEnemies();
                            for (int i = 0; i < enemies.Length; i++)
                            {
                                if (Comparator.HaveLessHealth(new[] {enemies[i]}, value))
                                {
                                    enemies[i].SelectAsTarget();
                                    return;
                                }
                            }
                        }
                            break;
                        case SelectTargetWith.WITH_MORE_HEALTH_THAN:
                        {
                            var enemies = TargetSelector.GetAllEnemies();
                            for (int i = 0; i < enemies.Length; i++)
                            {
                                if (Comparator.HaveMoreHealth(new[] {enemies[i]}, value))
                                {
                                    enemies[i].SelectAsTarget();
                                    return;
                                }
                            }
                        }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case TargetIntention.ALL_ALLIES:
                    TargetSelector.GetAllAllies().SelectAsTargets();
                    break;
                case TargetIntention.ALL_ENEMIES:
                    TargetSelector.GetAllEnemies().SelectAsTargets();
                    break;
                case TargetIntention.SELF:
                    BattleManager.CurrentBattleActor.SelectAsTarget();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
        RANDOM_ALLY = 9,
        RANDOM_ENEMY = 10,
        ALLY = 11,
        ENEMY = 12,
        ALL_ALLIES = 13,
        ALL_ENEMIES = 14,
        SELF,
    }
        
    public enum SelectTargetWith
    {
        NONE,
        WITH_DEFEND_BUFF,
        WITH_LESS_HEALTH_THAN,
        WITH_MORE_HEALTH_THAN,
    }
}
