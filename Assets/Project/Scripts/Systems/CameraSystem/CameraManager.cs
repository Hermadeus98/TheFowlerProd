using System;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
#if UNITY_EDITOR
using UnityEditor;
#endif
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

        public void SetCamera(CinemachineVirtualCameraBase newCamera)
        {
            if(newCamera == current)
                return;
            
            ChangeCamera(newCamera);
        }
        
        public void SetCamera(string batchName, string cameraKey = "Default")
        {
            if (!cameraBatches.ContainsKey(batchName))
            {
                Debug.LogError($"cameraBatches don't contain {batchName}");
                return;
            }
            
            var cameraReference = cameraBatches[batchName];
            ChangeCamera(cameraReference, cameraKey);
        }

        public void SetCamera(cameraPath cameraPath)
        {
            if (cameraPath == null)
            {
                Debug.LogError("CAMERA PATH IS NULL");
                return;
            }
            
            if (cameraPath.genericKey == CameraGenericKeyEnum.NULL)
            {
                if (cameraPath.batchName.IsNullOrWhitespace())
                    return;
                
                SetCamera(cameraPath.batchName, cameraPath.cameraName);
            }
            else
            {
                var batchName = CameraGenericKey.GetCameraGenericKey(cameraPath.genericKey);
                SetCamera(batchName, cameraPath.cameraName);
            }
        }

        public void SetCamera(CameraBatch cameraBatch, string key = "Default")
        {
            ChangeCamera(cameraBatch, key);
        }

        public T GetCamera<T>(cameraPath cameraPath) where T : CinemachineVirtualCameraBase
        {
            return cameraBatches[cameraPath.batchName].CameraReferences[cameraPath.cameraName].virtualCamera as T;
        }
        
        private void ChangeCamera(CameraBatch cameraBatch, string key = "Default")
        {
            if (!cameraBatch.CameraReferences.ContainsKey(key)){
                Debug.LogWarning("MISSING CAMERA KEY " + key);
                return;
            }

            var newCamera = cameraBatch.CameraReferences[key].virtualCamera;
            
            if(Current.IsNotNull())
                Current.m_Priority = cameraClosePriority;
            
            ChangeCamera(newCamera);

            if (cameraBatch.CameraReferences[key].isDollyTrackCamera)
            {
                DollyTrackActivation(cameraBatch.CameraReferences[key]);
            }
        }

        private void ChangeCamera(CinemachineVirtualCameraBase newCamera)
        {
            newCamera.m_Priority = currentCameraPriority;
            current = newCamera;
        }

        private void DollyTrackActivation(CameraReference reference)
        {
            if (reference.virtualCamera is CinemachineVirtualCamera VM)
            {
                var c = VM.GetCinemachineComponent<CinemachineTrackedDolly>();
                c.m_PathPosition = 0;
                c.DoTrackDollyPath(1f, reference.moveDuration, reference.moveEase).SetDelay(reference.delay);
            }
        }
        
        public static void RegisterBatch(CameraBatch batch)
        {
            batch.Generate();

            if(!CameraBatches.ContainsKey(batch.batchName))
                CameraBatches.Add(batch.batchName, batch);
        }

        public static void Unregister(CameraBatch batch)
        {
            if (CameraBatches.ContainsKey(batch.batchName))
                CameraBatches.Remove(batch.batchName);
        }
        
        //---<EDITOR>--------------------------------------------------------------------------------------------------<
#if UNITY_EDITOR
        [MenuItem("GameObject/Cameras/Default Virtual Camera", false, 20)]
        private static void CreateCamera(MenuCommand menuCommand)
        {
            var obj = Resources.Load("Camera/Virtual Camera Default");
            var go = PrefabUtility.InstantiatePrefab(obj, Selection.activeTransform) as GameObject;
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            go.name = obj.name;
            Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
            Selection.activeObject = go;
        }
#endif
    }

    [Serializable]
    public class cameraPath
    {
        [HorizontalGroup()] 
        public CameraGenericKeyEnum genericKey = CameraGenericKeyEnum.NULL;
        [HorizontalGroup()] 
        public string cameraName = "Default";
        [ShowIf("@this.genericKey == CameraGenericKeyEnum.NULL")]
        public string batchName;

        [Button]
        public void Target()
        {
            CameraManager.Instance.SetCamera(this);
#if UNITY_EDITOR
            var cam = GameObject.FindObjectOfType<CinemachineBrain>().ActiveVirtualCamera;
            if (cam is CinemachineVirtualCameraBase cast)
            {
                Selection.activeObject = cast;
            }
#endif
        }
    }
}
