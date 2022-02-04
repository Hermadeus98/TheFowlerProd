using System.Collections;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace TheFowler
{
    [RequireComponent(typeof(PlayableDirector))]
    public class PlayablePhase : GameplayPhase
    {
        [TabGroup("References"), GetComponent] 
        [SerializeField] private PlayableDirector playableDirector;
        
        public override void PlayPhase()
        {
            base.PlayPhase();
            
            playableDirector.Play();
            playableDirector.stopped += EndPhase;
        }

        public override void EndPhase()
        {
            base.EndPhase();
        }

        public void EndPhase(PlayableDirector director)
        {
            EndPhase();
            playableDirector.stopped -= EndPhase;
        }

        protected override void OnPause()
        {
            base.OnPause();
            playableDirector.Pause();
        }

        protected override void OnUnpause()
        {
            base.OnUnpause();
            playableDirector.Resume();
        }
        
        //---<EDITOR>--------------------------------------------------------------------------------------------------<
#if UNITY_EDITOR
        [MenuItem("GameObject/LD/Cinematique", false, 20)]
        private static void CreateStaticDialogue(MenuCommand menuCommand)
        {
            var obj = Resources.Load("LD/Cinematique");
            var go = PrefabUtility.InstantiatePrefab(obj, Selection.activeTransform) as GameObject;
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            go.name = obj.name;
            Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
            Selection.activeObject = go;
        }
#endif
    }
}
