using System;
using Cinemachine;
using QRCode;
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu(menuName = CreateAssetMenuPath.cameraTransitionData)]
    public class CameraTransitions : Database<string, CinemachineBlendPresset>
    {
        
    }

    [Serializable]
    public class CinemachineBlendPresset
    {
        public float Duration = 2f;
    }
}
