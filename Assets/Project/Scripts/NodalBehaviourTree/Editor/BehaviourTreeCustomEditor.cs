using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace TheFowler.Editor
{
    [CustomEditor(typeof(BehaviourTree))]
    public class BehaviourTreeCustomEditor : OdinEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Open Graph Editor"))
            {
                OpenInGraphEditor();
            }
        }

        private void OpenInGraphEditor()
        {
            BehaviourTreeEditorWindow.OpenWindow();
        }
    }
}
