using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using MenuCommand = System.ComponentModel.Design.MenuCommand;

namespace TheFowlerEditor{
    public class CameraCreator
    {
     
        [MenuItem ("Assets/Add Custom Editor %#e", false, 10000)]
        public static void CreateNewCamera()
        {
            if (Selection.activeTransform.TryGetComponent<CinemachineVirtualCameraBase>(out var c))
            {
                var t = c.transform;
                t.position = SceneView.lastActiveSceneView.camera.transform.position;
                t.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
            }
            else
            {
                var obj = Resources.Load("Camera/Virtual Camera Default");
                var go = PrefabUtility.InstantiatePrefab(obj, Selection.activeTransform) as GameObject;
                go.transform.position = SceneView.lastActiveSceneView.camera.transform.position;
                go.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
                Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
                Selection.activeObject = go;
            }
        }
    }
}
