using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ExecuteAlways]
public class VFXVelocityOriented : MonoBehaviour
{
    Vector3 prevPos;
    Vector3 currPos;

    Vector3 velocity => currPos - prevPos;

    private void Update()
    {
        Process();
    }

    private void Process()
    {
        prevPos = currPos;
        currPos = transform.position;
        transform.forward = velocity.normalized;
    }
}
