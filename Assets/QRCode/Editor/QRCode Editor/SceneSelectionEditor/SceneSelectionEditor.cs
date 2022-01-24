using QRCode.Extensions;
using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace QRCode.Editor
{
    public class SceneSelectionEditor : EditorWindow
    {
        private static SceneDatabase SceneDatabase;
        private static SceneSelectionEditor window;

        [MenuItem("SceneSelection/OpenScene")]
        static void DoSomething()
        {
            window = (SceneSelectionEditor)EditorWindow.GetWindow(typeof(SceneSelectionEditor));
            SceneDatabase = SceneDatabase.Instance;
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Open Scene");
            
            if(SceneDatabase.Instance.sceneReferences.IsNullOrEmpty())
            {
                Debug.Log("There is no scene in SceneDatabase", SceneDatabase);
            }
            
            if (SceneDatabase.IsNotNull())
            {
                for (int i = 0; i < SceneDatabase.sceneReferences.Length; i++)
                {
                    GUILayout.BeginHorizontal("box");
                    if (GUILayout.Button(SceneDatabase.sceneReferences[i].ScenePath))
                    {
                        OpenScene(SceneDatabase.sceneReferences[i]);
                        window.Close();
                    }
                    
                    if (GUILayout.Button("Select Scene Asset"))
                    {
                        Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(SceneDatabase.sceneReferences[i].ScenePath);
                    }
                    GUILayout.EndHorizontal();
                }
            }
            
        }

        private void OpenScene(SceneReference sceneReference)
        {
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            EditorSceneManager.OpenScene(sceneReference.ScenePath);
        }
    }
}
