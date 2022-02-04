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
        
        [TabGroup("Debug")]
        [SerializeField, ReadOnly] private List<BattleActor> allies = new List<BattleActor>();
        [TabGroup("Debug")]
        [SerializeField, ReadOnly] private List<BattleActor> enemies = new List<BattleActor>();

        public override void PlayPhase()
        {
            RegisterActors();
            StartBattle();
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

        private void StartBattle()
        {
            //FOR DEBUG

            CameraManager.Instance.SetCamera(allies[0].CameraBatchBattle);
            
            Invoke(nameof(EndPhase), 2f);
        }

        public override void EndPhase()
        {
            FinishBattle();
            base.EndPhase();
        }

        private void FinishBattle()
        {
            Player.Robyn?.gameObject.SetActive(true);
            Player.Abigael?.gameObject.SetActive(true);
            Player.Pheobe?.gameObject.SetActive(true);
            
            enemies.ForEach(w => w.gameObject.SetActive(false));
            allies.ForEach(w => w.gameObject.SetActive(false));
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
}
