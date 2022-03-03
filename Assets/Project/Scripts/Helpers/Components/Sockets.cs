using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class Sockets : SerializedMonoBehaviour
    {
        public Transform body_Middle;
        public Transform hand_Right;
        public Transform hand_Left;

        [FoldoutGroup("ABI")] public Transform drum_Cymbal_L,drum_Cymbal_R, drum_StandTom;
    }
}
