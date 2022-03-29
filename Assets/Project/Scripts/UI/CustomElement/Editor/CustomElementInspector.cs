using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace TheFowler
{
    [CustomEditor(typeof(CustomElement))]
    public class CustomElementInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            CustomElement customElement = (CustomElement)target;

            // Show default inspector property editor
            DrawDefaultInspector();
        }
    }
}

