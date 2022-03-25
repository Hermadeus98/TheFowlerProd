using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class VFXDistanceScaler : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 offset;
    public Transform targetTransform;
    public float initialMeshLength = 1f;

    //private void Update()
    //{
    //    if (Application.isPlaying) return;
    //    HandleVFXTransform();
    //}

    public void SetTarget(Transform target)
    {
        this.targetTransform = target;
        HandleVFXTransform();
    }

    public void SetTarget(Vector3 targetPos)
    {
        this.targetTransform = null;    
        this.targetPosition = targetPos;
        HandleVFXTransform();
    }

    [OnInspectorGUI]
    private void HandleVFXTransform()
    {
        Vector3 pos = transform.position;

        Vector3 target = targetTransform != null ? targetTransform.position : targetPosition;
        target += offset;
        Vector3 dir = target - pos;

        float distance = Vector3.Distance(pos, target);
        transform.localScale = Vector3.one * (distance / initialMeshLength);

        Quaternion q = Quaternion.LookRotation(dir.normalized);
        transform.rotation = q;
    }
}
