using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class TargetNode : CompositeNode
    {
        [TitleGroup("Target")]
        [SerializeField] private TargetIntention TargetIntention;

        public void SelectTarget()
        {
            TargetSelector.Initialize(TargetTypeEnum.SELF);
            switch (TargetIntention)
            {
                case TargetIntention.NONE:
                    break;
                case TargetIntention.ALL:
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
}
