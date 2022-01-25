using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using QRCode;
using UnityEngine;

namespace TheFowler
{
    public class CameraManager : MonoBehaviourSingleton<CameraManager>
    {
        public static Camera Camera => Camera.main;

        public CinemachineVirtualCameraBase current;
        public static CinemachineVirtualCameraBase Current => Instance.current;
    }
}
