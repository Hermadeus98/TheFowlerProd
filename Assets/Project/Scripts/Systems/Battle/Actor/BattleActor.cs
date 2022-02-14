using System;
using Unity.RemoteConfig;
using UnityEngine;

namespace TheFowler
{
    public class BattleActor : GameplayMonoBehaviour, ITurnActor, ITarget
    {
        public BattleActorData BattleActorData;
        
        public CameraBatch cameraBatchBattle;
        public CameraBatch CameraBatchBattle => cameraBatchBattle;

        public BattleActorInfo BattleActorInfo;

        public Sockets sockets;

        public SelectionPointer SelectionPointer;
        
        protected Turn actorTurn;

        public BattleActorStats BattleActorStats;

        protected override void OnStart()
        {
            base.OnStart();
            
            OnChangeDifficulty(DifficultyManager.currentDifficulty);
            InitializeComponents();
        }

        protected virtual void InitializeComponents()
        {
            
        }

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
            DifficultyManager.OnDifficultyChange += OnChangeDifficulty;
        }

        protected override void UnregisterEvent()
        {
            base.UnregisterEvent();
            BattleManager.OnBattleStateChange -= OnBattleStateChange;
            DifficultyManager.OnDifficultyChange -= OnChangeDifficulty;
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
                        //CameraManager.Instance.SetCamera(cameraBatchBattle, "ActionPicking");
                        break;
                    case BattleStateEnum.SKILL_PICKING:
                        //CameraManager.Instance.SetCamera(cameraBatchBattle, "SkillPicking");
                        break;
                    case BattleStateEnum.TARGET_PICKING:
                        //CameraManager.Instance.SetCamera(cameraBatchBattle, "TargetPicking");
                        break;
                    case BattleStateEnum.SKILL_EXECUTION:
                        //CameraManager.Instance.SetCamera(cameraBatchBattle, "SkillExecutionDefault");
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

        protected virtual void OnChangeDifficulty(DifficultyEnum newDifficulty)
        {
            switch(newDifficulty)
            {
                case DifficultyEnum.TEST:
                {
                    var dataInitializer = new BattleActorDataInitializer(ConfigManager.appConfig.GetJson("TestJson"));
                    BattleActorStats = dataInitializer.datas;
                }
                    break;
                case DifficultyEnum.EASY:
                {
                    var dataInitializer = new BattleActorDataInitializer(ConfigManager.appConfig.GetJson("EasyJson"));
                    BattleActorStats = dataInitializer.datas;
                }
                    break;
                case DifficultyEnum.MEDIUM:
                {
                    var dataInitializer = new BattleActorDataInitializer(ConfigManager.appConfig.GetJson("MediumJson"));
                    BattleActorStats = dataInitializer.datas;
                }
                    break;
                case DifficultyEnum.HARD:
                {
                    var dataInitializer = new BattleActorDataInitializer(ConfigManager.appConfig.GetJson("HardJson"));
                    BattleActorStats = dataInitializer.datas;
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newDifficulty), newDifficulty, null);
            }
        }
        
        public void OnTarget()
        {
            SelectionPointer.Show();
        }

        public void OnEndTarget()
        {
            SelectionPointer.Hide();
        }
    }

    [Serializable]
    public class BattleActorInfo
    {
        public bool isDeath;
    }
}
