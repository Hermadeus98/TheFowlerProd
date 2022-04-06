using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class VFXLookAt : MonoBehaviour
{
    public Transform lookAt;
    public VFXDistanceScaler scaler;

    private void Update()
    {
        LookAt();
    }

    [OnInspectorGUI]
    private void LookAt()
    {
        Vector3 pos = Vector3.zero;
        if (lookAt != null)
        {
            pos = lookAt.position;
        }

        if (scaler != null)
        {
            pos = scaler.targetTransform.position + scaler.offset;
        }

        transform.LookAt(pos);
    }
}
