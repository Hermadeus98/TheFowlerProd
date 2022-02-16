using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Unity.RemoteConfig;
using UnityEngine;

namespace TheFowler
{
    public class BattleActor : GameplayMonoBehaviour, ITurnActor, ITarget
    {
        [TabGroup("References")] 
        [SerializeField] private BattleActorData battleActorData;
        [TabGroup("References")]
        [SerializeField] private CameraBatch cameraBatchBattle;
        [TabGroup("References")]
        public Sockets sockets;
        [TabGroup("References")]
        public SelectionPointer SelectionPointer;

        [TabGroup("References")] public BattleActorAnimator BattleActorAnimator;
        
        [TabGroup("Components")] [SerializeField]
        private BattleActorComponent[] battleActorComponents;
        [TabGroup("Components")] [SerializeField]
        private Health health;
        [TabGroup("Components")] [SerializeField]
        private Mana mana;
        
        [TabGroup("Datas")]
        [SerializeField] protected BattleActorInfo battleActorInfo;
        [TabGroup("Datas")]
        public BattleActorStats BattleActorStats;
        
        protected Turn actorTurn;

        //--<Properties>-----------------------------------------------------------------------------------------------<
        public CameraBatch CameraBatchBattle => cameraBatchBattle;
        public BattleActorData BattleActorData => battleActorData;
        public BattleActorInfo BattleActorInfo => battleActorInfo;
        public Health Health => health;

        protected override void OnStart()
        {
            base.OnStart();
            
            OnChangeDifficulty(DifficultyManager.currentDifficulty);
        }

        protected virtual void InitializeComponents()
        {
            battleActorComponents.ForEach(w => w.Initialize());
            health?.Initialize(BattleActorStats.health);
            mana?.Initialize(BattleActorStats.mana);

            battleActorInfo.isDeath = false;
            battleActorInfo.isStun = false;
            BattleActorInfo.isTaunt = false;
            battleActorInfo.buffBonus = 0;
            battleActorInfo.debuffMalus = 0;
        }

        public virtual void OnTurnStart()
        {
            Debug.Log(gameObject.name + " start turn");
            
            battleActorComponents.ForEach(w => w.OnTurnStart());
        }

        public virtual void OnTurnEnd()
        {
            Debug.Log(gameObject.name + " end turn");
        }

        public virtual bool SkipTurn()
        {
            if (BattleActorInfo.isStun)
            {
                Debug.Log(gameObject.name + " skip turn : stun");
                GetBattleComponent<Stun>().OnTurnStart();
                return true;
            }
            
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
            if (BattleActorData.bindingType == BattleActorData.BindingType.REMOTE_SETTINGS)
            {
                switch (newDifficulty)
                {
                    case DifficultyEnum.TEST:
                    {
                        var dataInitializer =
                            new BattleActorDataInitializer(
                                ConfigManager.appConfig.GetJson(BattleActorData.datakey_default));
                        BattleActorStats = dataInitializer.datas;
                    }
                        break;
                    case DifficultyEnum.EASY:
                    {
                        var dataInitializer =
                            new BattleActorDataInitializer(
                                ConfigManager.appConfig.GetJson(BattleActorData.datakey_easy));
                        BattleActorStats = dataInitializer.datas;
                    }
                        break;
                    case DifficultyEnum.MEDIUM:
                    {
                        var dataInitializer =
                            new BattleActorDataInitializer(
                                ConfigManager.appConfig.GetJson(BattleActorData.datakey_medium));
                        BattleActorStats = dataInitializer.datas;
                    }
                        break;
                    case DifficultyEnum.HARD:
                    {
                        var dataInitializer =
                            new BattleActorDataInitializer(
                                ConfigManager.appConfig.GetJson(BattleActorData.datakey_hard));
                        BattleActorStats = dataInitializer.datas;
                    }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(newDifficulty), newDifficulty, null);
                }
            }
            else
            {
                BattleActorStats.health = BattleActorData.health;
                BattleActorStats.mana = BattleActorData.mana;
            }
            
            InitializeComponents();
        }
        
        public void OnTarget()
        {
            SelectionPointer.Show();
        }

        public void OnEndTarget()
        {
            SelectionPointer.Hide();
        }

        public T GetBattleComponent<T>() where T : BattleActorComponent
        {
            for (int i = 0; i < battleActorComponents.Length; i++)
            {
                if (battleActorComponents[i] is T cast)
                {
                    return cast;
                }
            }

            return null;
        }
    }

    [Serializable]
    public class BattleActorInfo
    {
        public bool isDeath;
        public bool isStun;
        public float buffBonus;
        public float debuffMalus;
        public bool isTaunt;
    }
}
