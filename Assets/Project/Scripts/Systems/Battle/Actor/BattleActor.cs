using System;
using UnityEngine;

namespace TheFowler
{
    public class BattleActor : GameplayMonoBehaviour, ITurnActor
    {
        [SerializeField] private CameraBatch cameraBatchBattle;
        public CameraBatch CameraBatchBattle => cameraBatchBattle;

        public BattleActorInfo BattleActorInfo;

        public Snippets Snippets;
        
        protected Turn actorTurn;

        public virtual void OnTurnStart()
        {
            Debug.Log(gameObject.name + " start turn");
        }

        public virtual void OnTurnEnd()
        {
            Debug.Log(gameObject.name + " end turn");
        }

        public virtual bool SkipTurn()
        {
            if (BattleActorInfo.isDeath)
            {
                Debug.Log(gameObject.name + " skip turn");
                return true;
            }

            return false;
        }

        public virtual bool IsAvailable()
        {
            if (BattleActorInfo.isDeath)
            {
                return false;
            }

            return true;
        }

        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            BattleManager.OnBattleStateChange += OnBattleStateChange;
        }

        protected override void UnregisterEvent()
        {
            base.UnregisterEvent();
            BattleManager.OnBattleStateChange -= OnBattleStateChange;
        }

        protected virtual void OnBattleStateChange(BattleStateEnum currentBattleState)
        {
            if ((BattleActor) BattleManager.CurrentTurnActor == this)
            {
                switch (currentBattleState)
                {
                    case BattleStateEnum.START_BATTLE:
                        break;
                    case BattleStateEnum.ACTION_PICKING:
                        CameraManager.Instance.SetCamera(cameraBatchBattle, "ActionPicking");
                        break;
                    case BattleStateEnum.SKILL_PICKING:
                        CameraManager.Instance.SetCamera(cameraBatchBattle, "SkillPicking");
                        break;
                    case BattleStateEnum.TARGET_PICKING:
                        CameraManager.Instance.SetCamera(cameraBatchBattle, "TargetPicking");
                        break;
                    case BattleStateEnum.SKILL_EXECUTION:
                        CameraManager.Instance.SetCamera(cameraBatchBattle, "SkillExecutionDefault");
                        break;
                    case BattleStateEnum.FURY:
                        break;
                    case BattleStateEnum.END_BATTLE:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(currentBattleState), currentBattleState, null);
                }
            }
        }
    }

    [Serializable]
    public class BattleActorInfo
    {
        public bool isDeath;
    }
}
