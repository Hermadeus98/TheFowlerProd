using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace TheFowler
{
    [Serializable]
    public class CameraBatch<T> : CameraBatchBase where T : Enum
    {
        public T batch;

        public Dictionary<T, CameraReference> cameras =
            new Dictionary<T, CameraReference>();

        public CameraReference GetCameraReference(T key)
        {
            return cameras[key];
        }
    }

    public class CameraBatchBase
    {
        public void Register()
        {
            CameraManager.RegisterBatch(this);
        }

        public void Unregister()
        {
            CameraManager.UnregisterBatch(this);
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
