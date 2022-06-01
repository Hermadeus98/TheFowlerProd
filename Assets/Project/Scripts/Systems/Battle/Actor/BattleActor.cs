using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Unity.RemoteConfig;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

namespace TheFowler
{
    public class BattleActor : GameplayMonoBehaviour, ITurnActor, ITarget
    {
        public bool isParticipant = true;
        
        [TabGroup("References")] 
        [SerializeField] protected BattleActorData battleActorData;
        [TabGroup("References")]
        [SerializeField] private CameraBatch cameraBatchBattle;
        [TabGroup("References")]
        public Sockets sockets;
        [TabGroup("References")]
        public SelectionPointer SelectionPointer;

        [TabGroup("References")]
        public ParticleSystem SelectionVFX, SelectionVFXEmitter;
        [TabGroup("References")]
        [SerializeField] protected StateIcons stateIcons;

        [TabGroup("References")] public FeedbackHandler FeedbackHandler;
        
        [TabGroup("References")] public BattleActorAnimator BattleActorAnimator;
        [TabGroup("References")] public FeedbackReferences feedbackReferences;
        [TabGroup("References")] public AnimTriggerBase AnimTrigger;
        [TabGroup("References")] public SignalAsset SignalAsset_CastSpell;
        [TabGroup("References")] public SignalReceiver SignalReceiver_CastSpell;
        [TabGroup("References")] public SequenceHandler SequenceHandler;
          
        [TabGroup("Components")] [SerializeField]
        private BattleActorComponent[] battleActorComponents;
        [TabGroup("Components")] [SerializeField]
        private Health health;
        [TabGroup("Components")] [SerializeField]
        private CooldownComponent mana;
        [TabGroup("Components")] [SerializeField]
        public Punchline punchline;

        public GameObject reviveFeedback;
        
        [TabGroup("Datas")]
        [SerializeField] protected BattleActorInfo battleActorInfo;
        public BattleActorStats BattleActorStats;

        public int TurnCount { get; set; } = 0;
        [ShowInInspector, ReadOnly] public int Initiative { get; set; }
        
        public Turn actorTurn { get; set; }

        //--<Properties>-----------------------------------------------------------------------------------------------<
        public CameraBatch CameraBatchBattle => cameraBatchBattle;
        public BattleActorData BattleActorData { get { return battleActorData; } set { battleActorData = value; } }
        public BattleActorInfo BattleActorInfo => battleActorInfo;
        public Health Health => health;
        public CooldownComponent Mana => mana;

        public int orderInBattle;

        public StateIcons StateIcons
        {
            get => stateIcons;
            set => stateIcons = value;
        }

        public AllyData AllyData { get; set; }
        
        public bool mustResurect { get; set; }

        protected override void OnStart()
        {
            FeedbackHandler.Generate();
            
            base.OnStart();
            
            OnChangeDifficulty(DifficultyManager.currentDifficulty);
        }

        public virtual void InitializeComponents()
        {
            health?.Initialize(BattleActorStats.health);

            battleActorInfo.isStun = false;
            BattleActorInfo.isTaunt = false;
            GetBattleComponent<Taunt>().taunter = null;
            
            var currentBattle = BattleManager.CurrentBattle;
            
            if (currentBattle.HasRestart || currentBattle.StartWithSavedData)
            {
                for (int i = 0; i < battleActorComponents.Length; i++)
                {
                    if (battleActorComponents[i].GetType() == typeof(SpellHandler))
                    {
                        SpellHandler sh  = battleActorComponents[i] as SpellHandler;

                        if (currentBattle.robyn != null)
                            if (currentBattle.robyn == this)
                            {
                                sh.InitializeWithData();
                            }

                        if (currentBattle.abi != null)
                            if (currentBattle.abi == this)
                            {
                                sh.InitializeWithData();
                            }

                        if (currentBattle.phoebe != null)
                            if (currentBattle.phoebe == this)
                            {
                                sh.InitializeWithData();
                            }
                    }
                    else
                    {
                        battleActorComponents[i].Initialize();
                    }
                }


                if (currentBattle.robyn != null)
                    if (currentBattle.robyn == this)
                    {
                        health?.SetCurrentHealth(Player.RobynSavedData.health);
                        battleActorInfo.attackBonus = Player.RobynSavedData.attackBonus;
                        battleActorInfo.defenseBonus = Player.RobynSavedData.defenseBonus;
                        AllyData?.Refresh();
                        RefreshStateIcons();
                    }
                
                if(currentBattle.abi != null)
                    if (currentBattle.abi == this)
                    {
                        health?.SetCurrentHealth(Player.AbiSavedData.health);
                        battleActorInfo.attackBonus = Player.AbiSavedData.attackBonus;
                        battleActorInfo.defenseBonus = Player.AbiSavedData.defenseBonus;
                        AllyData?.Refresh();
                        RefreshStateIcons();
                    }
                
                if(currentBattle.phoebe != null)
                    if (currentBattle.phoebe == this)
                    {
                        health?.SetCurrentHealth(Player.PhoebeSavedData.health);
                        battleActorInfo.attackBonus = Player.PhoebeSavedData.attackBonus;
                        battleActorInfo.defenseBonus = Player.PhoebeSavedData.defenseBonus;
                        AllyData?.Refresh();
                        RefreshStateIcons();
                    }
            }
            else
            {
                battleActorComponents.ForEach(w => w.Initialize());
                battleActorInfo.isDeath = false;
                battleActorInfo.attackBonus = 0;
                battleActorInfo.defenseBonus = 0;
                RefreshStateIcons();
                GetBattleComponent<CooldownComponent>().ResetCD();
            }
        }

        [Button]
        public virtual void OnTurnStart()
        {
            if (GetBattleComponent<Taunt>().taunter.battleActorInfo.isDeath)
            {
                GetBattleComponent<Taunt>().EndTaunt();
            }
            
            TurnCount++;
            
            Debug.Log(gameObject.name + " start turn");

            battleActorComponents.ForEach(w => w.OnTurnStart());

            AllyData?.Select();
        }


        public virtual void OnTurnEnd()
        {
            Debug.Log(gameObject.name + " end turn");
            AllyData?.UnSelect();
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
            AllyData?.Refresh();
            RefreshStateIcons();
        }
        
        private void RefreshStateIcons()
        {
            stateIcons?.Refresh_Att(this);
            stateIcons?.Refresh_CD(this);
            stateIcons?.RefreshBuff_Def(this);
        }

        private Sequence pulse;
        
        public void OnTarget()
        {
            SelectionPointer?.Show();
            SelectionVFX.gameObject.SetActive(true);
            SelectionVFX?.Play();

            if (battleActorInfo.isDeath)
            {
                reviveFeedback.gameObject.SetActive(true);
                pulse?.Kill();
                pulse = DOTween.Sequence();
                pulse.Append(reviveFeedback.transform.DOScale(Vector3.one * 1.1f, .2f).SetEase(Ease.InOutSine));
                pulse.Append(reviveFeedback.transform.DOScale(Vector3.one, .2f).SetEase(Ease.InOutSine));
                pulse.SetLoops(-1);
                pulse.Play();
            }
        }

        public void OnEndTarget()
        {
            SelectionPointer?.Hide();
            SelectionVFX?.Stop();
            SelectionVFX.gameObject.SetActive(false);
            
            if (battleActorInfo.isDeath)
            {
                reviveFeedback.gameObject.SetActive(false);
                pulse?.Kill();
                reviveFeedback.transform.localScale = Vector3.one;
            }
        }

        public void OnTargetEmitterLog()
        {
            SelectionVFXEmitter.gameObject.SetActive(true);
            SelectionVFXEmitter?.Play();
        }

        public void OnEndTargetEmitterLog()
        {
            SelectionVFXEmitter?.Stop();
            SelectionVFXEmitter.gameObject.SetActive(false);
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

        public bool IsWeakOf(Spell.SpellTypeEnum spellTypeEnum)
        {
            var result = DamageCalculator.CalculateSpellTypeBonus(spellTypeEnum, BattleActorData.actorType);
            return result switch
            {
                DamageCalculator.ResistanceFaiblesseResult.NEUTRE => false,
                DamageCalculator.ResistanceFaiblesseResult.FAIBLESSE => true,
                DamageCalculator.ResistanceFaiblesseResult.RESISTANCE => false,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public bool IsResistantOf(Spell.SpellTypeEnum spellTypeEnum)
        {
            var result = DamageCalculator.CalculateSpellTypeBonus(spellTypeEnum, BattleActorData.actorType);
            return result switch
            {
                DamageCalculator.ResistanceFaiblesseResult.NEUTRE => false,
                DamageCalculator.ResistanceFaiblesseResult.FAIBLESSE => false,
                DamageCalculator.ResistanceFaiblesseResult.RESISTANCE => true,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public virtual void OnDeath()
        {
            if (GetBattleComponent<Taunt>().taunter != null)
            {
                GetBattleComponent<Taunt>().taunter.GetBattleComponent<Taunt>().EndTaunt();
            }

            BattleManager.CurrentBattle.lastDeath = this;
            GetBattleComponent<Buff>().StopVFX();
            GetBattleComponent<Defense>().StopVFX();
            Debug.Log("ON DEEAATH");
        }

        public virtual void OnResurect()
        {
            BattleActorAnimator.Resurect();
        }


        private void OnDisable()
        {
            for (int i = 0; i < BattleActorData.Spells.Length; i++)
            {
                BattleActorData.Spells[i].Reset();
            }
        }

        protected virtual void Update()
        {
            if(this is EnemyActor)
                return;
            
            if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                GetBattleComponent<CooldownComponent>().ResetCD();
            }
        }
    }

    [Serializable]
    public class BattleActorInfo
    {
        public bool isDeath;
        public bool isStun;
        public bool isTaunt;
        
        public int attackBonus;
        public int defenseBonus;
        public int cooldownBonus;
    }
}
