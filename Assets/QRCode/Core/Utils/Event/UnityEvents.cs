using System;

using UnityEngine;
using UnityEngine.Events;

namespace QRCode.Utils
{
    [Serializable]
    public class FloatUnityEvent : UnityEvent<float> { }

    [Serializable]
    public class IntUnityEvent : UnityEvent<int> { }

    [Serializable]
    public class BoolUnityEvent : UnityEvent<bool> { }

    [Serializable]
    public class Vector2UnityEvent : UnityEvent<Vector2> { }

    [Serializable]
    public class Vector2IntUnityEvent : UnityEvent<Vector2Int> { }

    [Serializable]
    public class Vector3UnityEvent : UnityEvent<Vector3> { }

    [Serializable]
    public class Vector3IntUnityEvent : UnityEvent<Vector3Int> { }

    [Serializable]
    public class StringUnityEvent : UnityEvent<string> { }

    [Serializable]
    public class GenericUnityEvent : UnityEvent<EventArgs> { }
}