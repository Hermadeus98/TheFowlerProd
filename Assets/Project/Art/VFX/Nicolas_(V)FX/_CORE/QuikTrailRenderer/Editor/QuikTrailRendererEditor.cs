using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuikTrailRenderer))]
public class QuikTrailRendererEditor : Editor
{
    private QuikTrailRenderer t;
    private MaterialEditor _materialEditor;

    private void OnEnable()
    {
        t = target as QuikTrailRenderer;

        if (t.material != null)
        {
            // Create an instance of the default MaterialEditor
            _materialEditor = (MaterialEditor)CreateEditor(t.material);
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        //CustomEditorUtility.DrawScriptField<QuikTrailRenderer>(target);
        EditorGUILayout.Space();

        float fW = EditorGUIUtility.fieldWidth;
        float lW = EditorGUIUtility.labelWidth;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("renderTrail"));

        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.fieldWidth = 20;
        EditorGUIUtility.labelWidth = 40;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("width"));
        EditorGUIUtility.labelWidth = 0.01f;
        EditorGUIUtility.fieldWidth = 250f;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("curve"));
        EditorGUILayout.EndHorizontal();
        EditorGUIUtility.labelWidth = lW;
        EditorGUIUtility.fieldWidth = fW;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("time"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("minVertexDistance"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("color"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("alignment"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("material"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("castShadows"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("receiveShadows"));

        EditorGUILayout.PropertyField(serializedObject.FindProperty("drawGizmos"));
        if (t.drawGizmos)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("debugCol"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("debugRadius"));
        }

        EditorGUILayout.Space();

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();

            if (_materialEditor != null)
            {
                // Free the memory used by the previous MaterialEditor
                DestroyImmediate(_materialEditor);
            }

            if (t.material != null)
            {
                // Create a new instance of the default MaterialEditor
                _materialEditor = (MaterialEditor)CreateEditor(t.material);

            }
        }


        if (_materialEditor != null)
        {
            // Draw the material's foldout and the material shader field
            // Required to call _materialEditor.OnInspectorGUI ();
            _materialEditor.DrawHeader();

            //  We need to prevent the user to edit Unity default materials
            bool isDefaultMaterial = !AssetDatabase.GetAssetPath(t.material).StartsWith("Assets");

            using (new EditorGUI.DisabledGroupScope(isDefaultMaterial))
            {

                // Draw the material properties
                // Works only if the foldout of _materialEditor.DrawHeader () is open
                _materialEditor.OnInspectorGUI();
            }
        }
    }

    private void OnDisable()
    {
        if (_materialEditor != null)
        {
            // Free the memory used by default MaterialEditor
            DestroyImmediate(_materialEditor);
        }
    }

    [MenuItem("GameObject/Effects/Quik Trail", false, 10)]
    public static void CreateTrailGameobject()
    {
        GameObject trailGo = new GameObject("Quik Trail");
        trailGo.AddComponent<QuikTrailRenderer>();
    }
}