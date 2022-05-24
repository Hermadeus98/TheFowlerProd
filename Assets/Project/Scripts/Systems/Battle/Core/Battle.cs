using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class Battle : GameplayPhase
    {
        [TabGroup("References")]
        [SerializeField] private Transform alliesBatch, enemiesBatch;

        [TitleGroup("General Settings")]
        private StateMachine battleState;
        [TitleGroup("General Settings")]
        public bool replaceActorAtTheEnd = true;

        [TitleGroup("General Settings")]
        [SerializeField]
        public bool enableProgression = true;
        [TitleGroup("General Settings")] [ShowIf("enableProgression")]
        [SerializeField]
        public bool showSkillTree = true;
        [TitleGroup("General Settings")]
        [ShowIf("enableProgression")]
        [SerializeField]
        public int  numberOfAllies = 2;

        private bool hasPlayed = false;

        [SerializeField] private UnityEngine.Events.UnityEvent OnStartProgression;


        [TabGroup("References")] [SerializeField]
        private Istate[] battleStates;

        [TabGroup("References")] public CameraBatch BattleCameraBatch;
        [TabGroup("References")] public PlayerInput Inputs;

        [TabGroup("Debug")] [SerializeField] private bool finishDirectly = false;
        [TabGroup("Debug")]
        [SerializeField, ReadOnly] private List<BattleActor> allies = new List<BattleActor>();
        [TabGroup("Debug")]
        [SerializeField, ReadOnly] private List<BattleActor> enemies = new List<BattleActor>();

        [TabGroup("Debug")] [SerializeField] private bool playAtStart = false;

        public StateMachine BattleState => battleState;
        public List<BattleActor> Allies => allies;
        public List<BattleActor> Enemies => enemies;
        public bool FinishDirectly => finishDirectly;

        public TurnSystem TurnSystem;

        public AllyActor robyn, abi, phoebe;

        public bool StartWithSavedData = false;

        public BattleNarrationComponent BattleNarrationComponent;
        public BattleGameLogComponent BattleGameLogComponent;
        
        public bool HasRestart { get; set; }
        public bool IsFinish { get; set; }

        public int EnemyDeathCount { get; set; } = 0;
        
        [Sirenix.OdinInspector.FilePath] public string referenceBattlePath;

        public BattleActor lastDeath { get; set; }

        [SerializeField] private Battle BattleToRestart;
        [SerializeField] private Battle[] BattleToReset;

        public bool useUIOnPivot = false;
        public Transform UIPivot;
        public GameObject battleStateObj;
        
        private void FixedUpdate()
        {
            if (isActive)
            {
                if(Keyboard.current.rightArrowKey.wasPressedThisFrame)
                    NextTurn();
                
                if(Keyboard.current.rKey.wasPressedThisFrame)
                    TurnSystem.CurrentRound.OverrideTurn(robyn);
                
                if(Keyboard.current.qKey.wasPressedThisFrame)
                    TurnSystem.CurrentRound.OverrideTurn(abi);
            }
        }

        protected override void OnAwake()
        {
            base.OnAwake();

            battleStates = battleStateObj.GetComponentsInChildren<Istate>();
            
            SetActorState(false);
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            
            if (playAtStart)
            {
                if (enableProgression)
                {
                    if (showSkillTree)
                    {

                        OnStartProgression.Invoke();
                        UI.GetView<MenuCharactersView>(UI.Views.MenuCharacters).Show(this);
                        hasPlayed = true;
                    }
                    else
                    {
                        PlayPhase();
                    }

                }
                else
                {
                    PlayPhase();
                }
            }

            yield break;
        }

        public override void PlayPhase()
        {
            FindObjectOfType<GameTimer>().incrementeCombatTimer = true;
            
            if (!enableProgression)
            {
                if (!showSkillTree)
                {
                    for (int i = 0; i < allies.Count; i++)
                    {
                        allies[i].BattleActorData = allies[i].BattleActorData.defaultData;
                    }
                }
            }
            else 
            {
                if (!hasPlayed)
                {
                    if (showSkillTree)
                    {
                        OnStartProgression.Invoke();
                        UI.GetView<MenuCharactersView>(UI.Views.MenuCharacters).Show(this);
                        hasPlayed = true;
                        return;
                    }

                }
            }

            base.PlayPhase();

            BattleManager.CurrentBattle = this;

            SortByInitiative();
            RegisterActors();
            InitializeTurnSystem();

            StartCoroutine(StartBattle());

        }

        private IEnumerator StartBattle()
        {
            BattleManager.IsReducingCD = false;
            Fury.IsInBreakdown = false;

            IsFinish = false;

            //Event On Start
            Debug.Log("EVENT - ON_BATTLE_START");
            if (BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnStartBattle() != null)
            {
                yield return BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnStartBattle()
                    .NarrativeEvent();
            }
            
            InitializeUI();

            ChangeBattleState(BattleStateEnum.START_BATTLE);
            CreateTurnSystem();

            yield break;
        }

        private void CreateTurnSystem()
        {
            var turnActors = new List<ITurnActor>(allies);
            turnActors.AddRange(enemies);
            TurnSystem = new TurnSystem(turnActors);
            TurnSystem.StartTurnSystem();
        }
        
        private void InitializeUI()
        {
            UIBattleBatch.Instance.CanvasGroup.alpha = 1;
            
            var alliesDataView = UI.GetView<AlliesDataView>("AlliesDataView");
            alliesDataView.Initialize(robyn, abi, phoebe);
            alliesDataView.Show();
        }
        
        private void RegisterActors()
        {
            Player.Robyn?.gameObject.SetActive(false);
            Player.Abigael?.gameObject.SetActive(false);
            Player.Pheobe?.gameObject.SetActive(false);

            SetActorState(true);
        }

        [Button]
        public bool CheckVictory()
        {
            if (allies.All(w => w.BattleActorInfo.isDeath))
            {
                Debug.Log("DEFEAT");
                Lose();
                return true;
            }
            if (enemies.All(w => w.BattleActorInfo.isDeath))
            {
                Debug.Log("VICTORY");
                StartCoroutine(OnWin());
                return true;
            }

            return false;
        }

        IEnumerator OnWin()
        {
            Debug.Log("EVENT : ON_WIN");
            if (BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnWin() != null)
            {
                yield return BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnWin()
                    .NarrativeEvent();
            }
            
            StopBattle();
        }

        [Button]
        public void StopBattle()
        {
            StartCoroutine(StopBattleCoroutine());

            hasPlayed = false;
        }

        private IEnumerator StopBattleCoroutine()
        {
            IsFinish = true;
            FindObjectOfType<GameTimer>().incrementeCombatTimer = false;

            ChangeBattleState(BattleStateEnum.END_BATTLE);
            
            SaveData();
            yield break;
        }

        private void SaveData()
        {
            if (robyn != null)
            {
                Player.RobynSavedData.health = robyn.Health.CurrentHealth;
                Player.RobynSavedData.mana = robyn.Mana.CurrentMana;
                Player.RobynSavedData.spellHandler = robyn.GetBattleComponent<SpellHandler>();
                Player.RobynSavedData.attackBonus = robyn.BattleActorInfo.attackBonus;
                Player.RobynSavedData.defenseBonus = robyn.BattleActorInfo.defenseBonus;
            }

            if (abi != null)
            {
                Player.AbiSavedData.health = abi.Health.CurrentHealth;
                Player.AbiSavedData.mana = abi.Mana.CurrentMana;
                Player.AbiSavedData.spellHandler = abi.GetBattleComponent<SpellHandler>();
                Player.AbiSavedData.attackBonus = abi.BattleActorInfo.attackBonus;
                Player.AbiSavedData.defenseBonus = abi.BattleActorInfo.defenseBonus;
            }

            if (phoebe != null)
            {
                Player.PhoebeSavedData.health = phoebe.Health.CurrentHealth;
                Player.PhoebeSavedData.mana = phoebe.Mana.CurrentMana;
                Player.PhoebeSavedData.spellHandler = phoebe.GetBattleComponent<SpellHandler>();
                Player.PhoebeSavedData.attackBonus = phoebe.BattleActorInfo.attackBonus;
                Player.PhoebeSavedData.defenseBonus = phoebe.BattleActorInfo.defenseBonus;
            }
        }

        [Button]
        public void NextTurn()
        {
            TurnSystem.NextTurn();
        }
        
        public void ResetTurn()
        {
            TurnSystem.ResetTurn();
        }

        private void InitializeTurnSystem()
        {
            battleState = new StateMachine(battleStates, UpdateMode.Update, EventArgs.Empty);
        }

        public void ChangeBattleState(BattleStateEnum key)
        {
            battleState.SetState(GetBattleStateKey(key), EventArgs.Empty);
            BattleManager.OnBattleStateChange?.Invoke(key);

            Player.Robyn?.gameObject.SetActive(replaceActorAtTheEnd);
            Player.Abigael?.gameObject.SetActive(replaceActorAtTheEnd);
            Player.Pheobe?.gameObject.SetActive(replaceActorAtTheEnd);
        }
        
        public T ChangeBattleState<T>(BattleStateEnum key) where T : class, Istate
        {
            ChangeBattleState(key);
            return battleState.GetState(GetBattleStateKey(key)) as T;
        }

        private void SetActorState(bool state)
        {
            allies.Clear();
            enemies.Clear();

            for (var i = 0; i < alliesBatch.childCount; i++)
            {
                if (alliesBatch.GetChild(i).GetComponent<BattleActor>().isParticipant)
                {
                    allies.Add(alliesBatch.GetChild(i).GetComponent<BattleActor>());
                    alliesBatch.GetChild(i).gameObject.SetActive(state);
                }
            }

            for (var i = 0; i < enemiesBatch.childCount; i++)
            {
                if (enemiesBatch.GetChild(i).GetComponent<BattleActor>().isParticipant)
                {
                    enemiesBatch.GetChild(i).gameObject.SetActive(state);
                    enemies.Add(enemiesBatch.GetChild(i).GetComponent<BattleActor>());
                }
            }

            enemies = new List<BattleActor>(enemies.OrderBy(w => w.orderInBattle));
        }

        private void SortByInitiative()
        {
            Player.useInitiative = true;

            robyn.Initiative = Player.RobynSavedData.initiative;
            abi.Initiative = Player.AbiSavedData.initiative;
            phoebe.Initiative = Player.PhoebeSavedData.initiative;

            var initiativeList = new List<BattleActor>();
            initiativeList.Add(robyn);
            initiativeList.Add(abi);
            if(phoebe != null) initiativeList.Add(phoebe);

            var orderedEnumerable = initiativeList.OrderBy(w => w.Initiative);

            if (Player.useInitiative)
            {
                for (int i = 0; i < orderedEnumerable.Count(); i++)
                {
                    orderedEnumerable.ElementAt(i).transform.SetSiblingIndex(i);
                }
                
                UI.GetView<AlliesDataView>(UI.Views.AlliesDataView).Sort(orderedEnumerable);
            }
        }

        private string GetBattleStateKey(BattleStateEnum key)
        {
            return key switch
            {
                BattleStateEnum.START_BATTLE => "StartBattle",
                BattleStateEnum.ACTION_PICKING => "ActionPicking",
                BattleStateEnum.SKILL_PICKING => "SkillPicking",
                BattleStateEnum.TARGET_PICKING => "TargetPicking",
                BattleStateEnum.SKILL_EXECUTION => "SkillExecution",
                BattleStateEnum.END_BATTLE => "EndBattle",
                BattleStateEnum.FURY => "Fury",
                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
            };
        }

        [Button]
        public void Lose()
        {
            StartCoroutine(LoseIE());
        }

        private IEnumerator LoseIE()
        {
            callOnEndEvent = false;

            Debug.Log("EVENT : ON_LOSE");
            if (BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnLose() != null)
            {
                yield return BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnLose()
                    .NarrativeEvent();
            }
            
            StopBattle();
            UI.OpenView("LoseView");
        }

        [Button]
        public void Restart()
        {
            StartCoroutine(RestartIE());
        }

        IEnumerator RestartIE()
        {
            callOnEndEvent = false;
            HasRestart = true;
            StopBattle();

            yield return new WaitForSeconds(1f);

            BattleToReset.ForEach(w => w.ResetBattle());

            BattleToRestart.PlayPhase();
        }

        public void ResetBattle()
        {
            //Reset Health
            allies.ForEach(w => w.Health.ResetHealth());
            enemies.ForEach(w => w.Health.ResetHealth());
            UIBattleBatch.SetUIGuardsVisibility(true);
            
            //Reset CoolDown
            //
            
            //Reset Status
            //

            HasRestart = false;
            callOnEndEvent = true;
        }
        
        /*public void Restart()
        {
            var battle = this;
            battle.gameObject.SetActive(false);

            var newBattle = Instantiate(Resources.Load<Battle>(referenceBattlePath), battle.transform.position, battle.transform.rotation);
            
            DestroyImmediate(battle);

            newBattle.HasRestart = true;
            newBattle.PlayPhase();
        }*/

        //---<EDITOR>--------------------------------------------------------------------------------------------------<
#if UNITY_EDITOR
        [MenuItem("GameObject/LD/Battle/Simple Battle", false, 20)]
        private static void CreateStaticDialogue(MenuCommand menuCommand)
        {
            var obj = Resources.Load("LD/Battle");
            var go = PrefabUtility.InstantiatePrefab(obj, Selection.activeTransform) as GameObject;
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            go.name = obj.name;
            Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
            Selection.activeObject = go;
        }
#endif
    }
    
    public enum BattleStateEnum
    {
        START_BATTLE,
        ACTION_PICKING,
        SKILL_PICKING,
        TARGET_PICKING,
        SKILL_EXECUTION,
        FURY,
        END_BATTLE,
        
    }

}
