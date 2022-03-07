using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class Sockets : SerializedMonoBehaviour
    {
        [FoldoutGroup("Generic")] public Transform
            body_Middle,
            hand_Right,
            hand_Left,
            foot_Right,
            foot_Left;

        [FoldoutGroup("ABI")] public Transform 
            drum_Cymbal_L,
            drum_Cymbal_R, 
            drum_StandTom;
    }
}
