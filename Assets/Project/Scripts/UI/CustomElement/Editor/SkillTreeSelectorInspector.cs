using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace TheFowler
{
    [CustomEditor(typeof(SkillTreeSelector))]
    public class SkillTreeSelectorInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SkillTreeSelector skillTreeSelector= (SkillTreeSelector)target;

            // Show default inspector property editor
            DrawDefaultInspector();
        }
    }
}

