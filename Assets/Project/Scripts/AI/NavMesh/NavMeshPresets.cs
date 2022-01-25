using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu(menuName = CreateAssetMenuPath.presets_AI + "NavMeshPreset")]
    public class NavMeshPresets : Database<ControllerMovement, NavMeshPreset>
    {
        
    }

    [Serializable]
    public class NavMeshPreset
    {
        [Header("Steering")]
        public float Speed = 3.5f;
        public float AngularSpeed = 120f;
        public float Acceleration = 8f;
    }
}
