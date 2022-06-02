using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class GraphicsAnalyser : SerializedMonoBehaviour
{
    public int minVertices = 100000;
    public Dictionary<MeshFilter, int> selectedMeshFilters = new Dictionary<MeshFilter, int>();

    
    [Button]
    void AnalyzeVertices()
    {
        var meshFilters = FindObjectsOfType<MeshFilter>();
        selectedMeshFilters.Clear();
        var dic = new Dictionary<MeshFilter, int>();

        for (int i = 0; i < meshFilters.Length; i++)
        {
            if(meshFilters[i].mesh.triangles.Length > minVertices)
                dic.Add(meshFilters[i], meshFilters[i].mesh.triangles.Length);
        }

        var ordered = dic.OrderBy((w => w.Value));
        for (int i = 0; i < ordered.Count(); i++)
        {
            selectedMeshFilters.Add(ordered.ElementAt(i).Key, ordered.ElementAt(i).Value);
        }
    }

    [ReadOnly] public int camerasCount;

    [Button]
    public void CountCameras()
    {
        var cameras = FindObjectsOfType<CinemachineVirtualCameraBase>();
        camerasCount = cameras.Length;
    }
}
