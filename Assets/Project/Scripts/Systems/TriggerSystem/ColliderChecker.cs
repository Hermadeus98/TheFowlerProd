using System;

using Sirenix.OdinInspector;

using UnityEngine;

namespace TheFowler
{
    public class ColliderChecker : SerializedMonoBehaviour
    {
        [SerializeField] private ColliderFlag Flag;

        public bool CompareFlag(ColliderFlag flag)
        {
            return flag == Flag;
        }
    }

    [Flags]
    public enum ColliderFlag
    {
        NONE = 0,
        PLAYER = 1,
    }
}
