using QRCode.Extensions;
using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace QRCode.Editor
{
    public class SceneSelectionEditor : EditorWindow
    {
        private static SceneDatabase SceneDatabase;
        private static SceneSelectionEditor window;

        private Vector2 scrollPos;

        [MenuItem("SceneSelection/OpenScene")]
        static void DoSomething()
        {
            window = (SceneSelectionEditor)EditorWindow.GetWindow(typeof(SceneSelectionEditor));
            SceneDatabase = SceneDatabase.Instance;
            window.Show();
        }

        private void OnGUI()
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos);

            GUILayout.Label("Open Scene");
            
            
            if(SceneDatabase.Instance.ScenesBatches.IsNullOrEmpty())
            {
                Debug.Log("There is no scene in SceneDatabase", SceneDatabase);
            }
            
            if (SceneDatabase.IsNotNull())
            {
                for (int i = 0; i < SceneDatabase.ScenesBatches.Length; i++)
                {
                    GUILayout.BeginVertical("box");
                    GUILayout.BeginHorizontal("box");
                    GUILayout.Label(SceneDatabase.ScenesBatches[i].batchName);
                    
                    if (GUILayout.Button("Open All Scene"))
                    {
                        for (int j = 0; j < SceneDatabase.ScenesBatches[i].sceneReferences.Length; j++)
                        {
                            OpenScene(SceneDatabase.ScenesBatches[i].sceneReferences[j]);
                        }
                    }
                    
                    if (GUILayout.Button("Close All Scene"))
                    {
                        EditorSceneManager.SaveOpenScenes();
                        
                        for (int j = 0; j < SceneDatabase.ScenesBatches[i].sceneReferences.Length; j++)
                        {
                            CloseScene(SceneDatabase.ScenesBatches[i].sceneReferences[j]);
                        }
                    }
                    GUILayout.EndHorizontal();

                    for (int j = 0; j < SceneDatabase.ScenesBatches[i].sceneReferences.Length; j++)
                    {
                        GUILayout.BeginHorizontal("box");
                        if (GUILayout.Button(SceneDatabase.ScenesBatches[i].sceneReferences[j].ScenePath))
                        {
                            OpenScene(SceneDatabase.ScenesBatches[i].sceneReferences[j]);
                        }
                    
                        if (GUILayout.Button("Select Scene Asset"))
                        {
                            Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(SceneDatabase.ScenesBatches[i].sceneReferences[j].ScenePath);
                        }
                        GUILayout.EndHorizontal();
                    }
                    
                    GUILayout.EndVertical();
                }
                
                GUILayout.EndScrollView();
            }
        }

        private void OpenScene(SceneReference sceneReference)
        {
            //EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            EditorSceneManager.OpenScene(sceneReference.ScenePath, OpenSceneMode.Additive);
        }

        private void CloseScene(SceneReference sceneReference)
        {
            EditorSceneManager.UnloadSceneAsync(EditorSceneManager.GetSceneByPath(sceneReference.ScenePath));
        }
    }
}
