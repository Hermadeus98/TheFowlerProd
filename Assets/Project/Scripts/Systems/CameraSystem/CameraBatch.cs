using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class CameraBatch : GameplayMonoBehaviour
    {
        public string batchName;
        [SerializeField] private bool registerBatch = true;
        
        public Dictionary<string, CameraReference> CameraReferences = new Dictionary<string, CameraReference>();

        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            if(registerBatch) CameraManager.RegisterBatch(this);
        }

        protected override void UnregisterEvent()
        {
            base.UnregisterEvent();
            if(registerBatch) CameraManager.Unregister(this);
        }

        [Button]
        public void Generate()
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

        [Button]
        private void Test(string key = "Default")
        {
            CameraManager.Instance.SetCamera(this, key);
        }
    }

    [Serializable]
    public class CameraReference
    {
        public CinemachineVirtualCameraBase virtualCamera;

        public bool isDollyTrackCamera = false;
        
        [ShowIf("@this.isDollyTrackCamera"), BoxGroup("Dolly Track Settings")]
        public bool lauchAtStart = true;
        [ShowIf("@this.isDollyTrackCamera"), BoxGroup("Dolly Track Settings")]
        public float delay = 0;
        [ShowIf("@this.isDollyTrackCamera"), BoxGroup("Dolly Track Settings")]
        public float moveDuration = 2f;
        [ShowIf("@this.isDollyTrackCamera"), BoxGroup("Dolly Track Settings")]
        public Ease moveEase = Ease.InOutSine;
    }
}
