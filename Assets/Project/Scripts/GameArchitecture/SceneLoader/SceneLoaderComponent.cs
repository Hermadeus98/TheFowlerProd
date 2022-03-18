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
        [SerializeField] private ChapterData chapterdata;

        [SerializeField] private VideoHandler video;
        [Button]
        public void ChangeChapter()
        {
            StartCoroutine(WaitTransition());
        }

        private void EndChapterLoaded()
        {
            SoundManager.PlaySound(chapterdata.audioEventOnChapterStart, gameObject);
            ChapterManager.ChangeChapter(chapterToLoad);


        }

        IEnumerator WaitTransition()
        {
            BlackPanel.Instance.Show();
            SoundManager.PlaySound(chapterdata.audioEventOnChapterEnded, gameObject);
            yield return new WaitForSeconds(.5f);
            video.PlayPhase(EndChapterLoaded); ;
            
            BlackPanel.Instance.Hide(.5f);

            yield return null;

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
