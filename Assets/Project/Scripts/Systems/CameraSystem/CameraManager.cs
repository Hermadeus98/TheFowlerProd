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

        [SerializeField] private List<CameraBatchBase> cameraBatches = new List<CameraBatchBase>();
        public static List<CameraBatchBase> CameraBatches => Instance.cameraBatches;

        public static void RegisterBatch(CameraBatchBase batch)
        {
            if (!CameraBatches.Contains(batch))
            {
                CameraBatches.Add(batch);
            }
        }

        public static void UnregisterBatch(CameraBatchBase batch)
        {
            if (CameraBatches.Contains(batch))
            {
                CameraBatches.Remove(batch);
            }
        }
    }
}
