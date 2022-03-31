using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;

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
        [TabGroup("References")] [SerializeField]
        private Istate[] battleStates;
        [TitleGroup("General Settings")] [SerializeField]
        public bool enableProgression = true;

        private bool wantProgression = false;

        [TabGroup("References")] public CameraBatch BattleCameraBatch;
        
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
        
        public bool HasRestart { get; set; }
        public bool IsFinish { get; set; }
        
        [Sirenix.OdinInspector.FilePath] public string referenceBattlePath;

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
            SetActorState(false);
        }

        private IEnumerator Start()
        {
            wantProgression = false;

            yield return new WaitForSeconds(1f);

            if (playAtStart)
            {
                if (enableProgression)
                {
                    UI.GetView<SkillTreeView>(UI.Views.SkillTree).Show(this);
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
            if (enableProgression)
            {
                UI.GetView<SkillTreeView>(UI.Views.SkillTree).Show(this);
                enableProgression = false;
                wantProgression = true;
                return;
            }

            base.PlayPhase();

            BattleManager.CurrentBattle = this;

            if (!StartWithSavedData)
                Fury.FuryPoint = 0;
            
            InitializeUI();

            RegisterActors();
            InitializeTurnSystem();
            
            StartBattle();

            enableProgression = wantProgression;

        }

        private void StartBattle()
        {
            IsFinish = false;
            ChangeBattleState(BattleStateEnum.START_BATTLE);
            CreateTurnSystem();
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
                StopBattle();
                return true;
            }

            return false;
        }

        [Button]
        public void StopBattle()
        {
            IsFinish = true;
            ChangeBattleState(BattleStateEnum.END_BATTLE);
            
            SaveData();

            for (int i = 0; i < allies.Count; i++)
            {
                allies[i].BattleActorData.AddComplicity(1);
            }
        }

        private void SaveData()
        {
            if (robyn != null)
            {
                Player.RobynSavedData.health = robyn.Health.CurrentHealth;
                Player.RobynSavedData.mana = robyn.Mana.CurrentMana;
            }

            if (abi != null)
            {
                Player.AbiSavedData.health = abi.Health.CurrentHealth;
                Player.AbiSavedData.mana = abi.Mana.CurrentMana;
            }

            if (phoebe != null)
            {
                Player.PhoebeSavedData.health = phoebe.Health.CurrentHealth;
                Player.PhoebeSavedData.mana = phoebe.Mana.CurrentMana;
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
            callOnEnd = false;

            StopBattle();
            UI.OpenView("LoseView");
        }
        
        [Button]
        public void Restart()
        {
            var battle = this;
            battle.gameObject.SetActive(false);

            var newBattle = Instantiate(Resources.Load<Battle>(referenceBattlePath), battle.transform.position, battle.transform.rotation);
            
            DestroyImmediate(battle);

            newBattle.HasRestart = true;
            newBattle.PlayPhase();
        }


        
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
