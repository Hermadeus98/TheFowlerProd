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

        [SerializeField] private List<CameraBatch> cameraBatches = new List<CameraBatch>();
        public static List<CameraBatch> CameraBatches => Instance.cameraBatches;

        public static void RegisterBatch(CameraBatch batch)
        {
            if (!CameraBatches.Contains(batch))
            {
                CameraBatches.Add(batch);
            }
        }

        public static void UnregisterBatch(CameraBatch batch)
        {
            if (CameraBatches.Contains(batch))
            {
                CameraBatches.Remove(batch);
            }
        }
    }
}
