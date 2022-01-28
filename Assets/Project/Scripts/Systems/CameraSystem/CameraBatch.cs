using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class CameraBatch : GameplayMonoBehaviour
    {
        public string batchName;
        //public CameraBatchEnum cameraBatchEnum;
        
        public Dictionary<string, CameraReference> CameraReferences = new Dictionary<string, CameraReference>();

        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            CameraManager.RegisterBatch(this);
        }

        protected override void UnregisterEvent()
        {
            base.UnregisterEvent();
            CameraManager.Unregister(this);
        }

        [Button]
        private void Generate()
        {
            CameraReferences.Clear();
            foreach (CinemachineVirtualCameraBase VM in transform.GetComponentsInChildren<CinemachineVirtualCameraBase>())
            {
                CameraReferences.Add(VM.gameObject.name, new CameraReference()
                {
                    virtualCamera = VM
                });
            }
        }
    }

    [Serializable]
    public class CameraReference
    {
        public CinemachineVirtualCameraBase virtualCamera;
    }

    public enum Camera_Dialogue_Statique
    {
        
    }
}
