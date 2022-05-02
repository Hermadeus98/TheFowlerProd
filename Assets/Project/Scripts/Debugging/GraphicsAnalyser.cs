using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GraphicsAnalyser : MonoBehaviour
{
    public int minVertices = 100000;
    public List<MeshFilter> selectedMeshFilters = new List<MeshFilter>();

    
    [Button]
    void AnalyzeVertices()
    {
        var meshFilters = FindObjectsOfType<MeshFilter>();
        selectedMeshFilters.Clear();

        for (int i = 0; i < meshFilters.Length; i++)
        {
            if(meshFilters[i].mesh.triangles.Length > minVertices)
                selectedMeshFilters.Add(meshFilters[i]);
        }
    }
}
