using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TheFowler
{
    public class Harmonisation : GameplayPhase
    {
        
        //---<EDITOR>--------------------------------------------------------------------------------------------------<
#if UNITY_EDITOR
        [MenuItem("GameObject/LD/Harmonisation", false, 20)]
        private static void CreateStaticDialogue(MenuCommand menuCommand)
        {
            var obj = Resources.Load("Dialogues/Harmonisation");
            var go = PrefabUtility.InstantiatePrefab(obj, Selection.activeTransform) as GameObject;
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            go.name = obj.name;
            Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
            Selection.activeObject = go;
        }
#endif
    }
}
