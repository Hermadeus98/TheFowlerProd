using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace TheFowler
{
    public class SceneLoaderComponent : SerializedMonoBehaviour
    {
        [SerializeField] private ChapterEnum chapterToLoad;
        [SerializeField] private VideoHandler video;
        [Button]
        public void ChangeChapter()
        {
            TransitionView view = UI.GetView<TransitionView>(UI.Views.TransitionView);
            view.chapterType = chapterToLoad;
            view.Show(TransitionType.CHAPTER, () => ChapterManager.ChangeChapter(chapterToLoad));

            video.PlayPhase();
            
        }
        
        //---<EDITOR>--------------------------------------------------------------------------------------------------<
#if UNITY_EDITOR
        [MenuItem("GameObject/LD/Chapter Loader", false, 20)]
        private static void Create(MenuCommand menuCommand)
        {
            var obj = Resources.Load("LD/Chapter Loader");
            var go = PrefabUtility.InstantiatePrefab(obj, Selection.activeTransform) as GameObject;
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            go.name = obj.name;
            Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
            Selection.activeObject = go;
        }
#endif
    }
}
