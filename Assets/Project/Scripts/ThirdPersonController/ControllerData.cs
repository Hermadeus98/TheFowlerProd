using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu(menuName = CreateAssetMenuPath.controller + "Third Person Controllers")]
    public class ControllerData : SerializedScriptableObject
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;

        public float MovementSpeed => movementSpeed;
        public float RotationSpeed => rotationSpeed;
    }
}
