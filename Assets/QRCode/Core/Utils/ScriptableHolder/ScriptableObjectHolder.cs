using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Utils
{
    public class ScriptableObjectHolder : SerializedMonoBehaviour
    {
        [SerializeField] private List<ScriptableObject> _objects = new List<ScriptableObject>();

    }
}

