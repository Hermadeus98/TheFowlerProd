using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace TheFowler
{
    [Serializable]
    public class CameraBatch
    {
        public CameraBatchEnum batch;

        public Dictionary<string, CinemachineVirtualCameraBase> cameras =
            new Dictionary<string, CinemachineVirtualCameraBase>();

        public void Register()
        {
            CameraManager.RegisterBatch(this);
        }

        public void Unregister()
        {
            CameraManager.UnregisterBatch(this);
        }
    }
}
