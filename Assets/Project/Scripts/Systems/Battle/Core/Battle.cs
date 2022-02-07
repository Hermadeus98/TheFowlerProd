using System;
using System.Collections.Generic;
using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace TheFowler
{
    public class Battle : GameplayPhase
    {
        [TabGroup("References")]
        [SerializeField] private Transform alliesBatch, enemiesBatch;

        [TitleGroup("General Settings")] [SerializeField]
        private StateMachine battleState;

        [TabGroup("References")] [SerializeField]
        private Istate[] battleStates;
        
        [TabGroup("Debug")] [SerializeField] private bool finishDirectly = false;
        [TabGroup("Debug")]
        [SerializeField, ReadOnly] private List<BattleActor> allies = new List<BattleActor>();
        [TabGroup("Debug")]
        [SerializeField, ReadOnly] private List<BattleActor> enemies = new List<BattleActor>();

        public StateMachine BattleState => battleState;
        public List<BattleActor> Allies => allies;
        public List<BattleActor> Enemies => enemies;
        public bool FinishDirectly => finishDirectly;
        
        public override void PlayPhase()
        {
            BattleManager.CurrentBattle = this;
            
            RegisterActors();
            InitializeTurnSystem();
            
            StartBattle();
        }

        private void StartBattle()
        {
            ChangeBattleState(BattleStateEnum.START_BATTLE);
        }
        
        private void RegisterActors()
        {
            Player.Robyn?.gameObject.SetActive(false);
            Player.Abigael?.gameObject.SetActive(false);
            Player.Pheobe?.gameObject.SetActive(false);

            for (var i = 0; i < alliesBatch.childCount; i++)
            {
                alliesBatch.GetChild(i).gameObject.SetActive(true);
                allies.Add(alliesBatch.GetChild(i).GetComponent<BattleActor>());
            }

            for (var i = 0; i < enemiesBatch.childCount; i++)
            {
                enemiesBatch.GetChild(i).gameObject.SetActive(true);
                enemies.Add(enemiesBatch.GetChild(i).GetComponent<BattleActor>());
            }
        }

        private void InitializeTurnSystem()
        {
            battleState = new StateMachine(battleStates, UpdateMode.Update, EventArgs.Empty);
        }

        public void ChangeBattleState(BattleStateEnum key)
        {
            battleState.SetState(GetBattleStateKey(key), EventArgs.Empty);
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
                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
            };
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
        END_BATTLE,
    }
}
