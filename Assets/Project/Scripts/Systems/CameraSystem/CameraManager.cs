using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class CameraManager : MonoBehaviourSingleton<CameraManager>
    {
        public static Camera Camera => Camera.main;

        [SerializeField, ReadOnly] private CinemachineVirtualCameraBase current;
        public static CinemachineVirtualCameraBase Current => Instance.current;

        [SerializeField, ReadOnly] private Dictionary<string, CameraBatch> cameraBatches = new Dictionary<string, CameraBatch>();
        public static Dictionary<string, CameraBatch> CameraBatches => Instance.cameraBatches;

        public int cameraClosePriority = 0;
        public int currentCameraPriority = 50;
        
        [Button]
        public void SetCamera(string batchName, string cameraKey = "Default")
        {
            var cameraReference = cameraBatches[batchName].CameraReferences[cameraKey];
            ChangeCamera(cameraReference.virtualCamera);
        }

        private void ChangeCamera(CinemachineVirtualCameraBase newCamera)
        {
            if(Current.IsNotNull())
                Current.m_Priority = cameraClosePriority;
            
            newCamera.m_Priority = currentCameraPriority;
            current = newCamera;
        }
        
        public static void RegisterBatch(CameraBatch batch)
        {
            if(!CameraBatches.ContainsKey(batch.batchName))
                CameraBatches.Add(batch.batchName, batch);
        }

        public static void Unregister(CameraBatch batch)
        {
            if (CameraBatches.ContainsKey(batch.batchName))
                CameraBatches.Remove(batch.batchName);
        }
    }
}
