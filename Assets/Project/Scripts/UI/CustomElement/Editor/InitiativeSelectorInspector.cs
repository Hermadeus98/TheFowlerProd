using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace TheFowler
{
    [CustomEditor(typeof(InitiativeSelector))]
    public class InitiativeSelectorInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            InitiativeSelector initiativeSelector = (InitiativeSelector)target;

            // Show default inspector property editor
            DrawDefaultInspector();
        }
    }

}
