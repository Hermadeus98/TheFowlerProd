using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

public class CinemachineDebugger : SerializedMonoBehaviour
{
    [Button]
    private void RevertCamPriority()
    {
#if UNITY_EDITOR
        

        var list = FindObjectsOfType<CinemachineVirtualCameraBase>();
        foreach (var camera in list)
        {
            camera.m_Priority = 0;
            EditorUtility.SetDirty(camera);
        }
#endif
    }
}
