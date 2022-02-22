using System;
using Cinemachine;
using DG.Tweening;
using PathCreation;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class Waypoints : SerializedMonoBehaviour
    {
        [SerializeField] private PathCreator path;
        
        [SerializeField] private Transform[] waypoints;
        public Transform[] GetWayPoints => waypoints;

        public GameInstructions OnCompleteInstructions;

        private PathController bindedPathController;
        [SerializeField] private PathController.PathControllerType PathControllerType;
        [SerializeField] private float duration = 4f;
        [SerializeField] private AnimationCurve ease;

        private CinemachineVirtualCameraBase initialVirtualCamera;
        public class CameraPathSetter
        {
            public float percent;
            public CinemachineVirtualCamera camera;
        }

        public CameraPathSetter[] cameras;

        private bool isActive = false;
        
        [Button]
        public void Follow(int characters)
        {
            var character = Player.GetCharacters((CharacterEnum)characters);
            var controller = character.Controller.SetController<NavMeshController>(ControllerEnum.NAV_MESH_CONTROLLER);
            controller.MoveAlongWayPoints(this, OnCompleteInstructions.Call);
        }

        [Button]
        public void FollowPath(int characters)
        {
            isActive = true;
            initialVirtualCamera = CameraManager.Current;
            
            var character = Player.GetCharacters((CharacterEnum)characters);
            bindedPathController = character.Controller.SetController<PathController>(ControllerEnum.PATH_CONTROLLER);
            bindedPathController.path = path;
            bindedPathController.MoveAlongWayPath(path, delegate
            {
                OnCompleteInstructions.Call();
                CameraManager.Instance.SetCamera(initialVirtualCamera);
                isActive = false;
            });
            
            bindedPathController.pathControllerType = PathControllerType;

            if (PathControllerType == PathController.PathControllerType.Auto)
            {
                DOTween.To(
                    () => bindedPathController.verticalBinding,
                    (x) => bindedPathController.verticalBinding = x,
                    1f,
                    duration
                ).SetEase(ease);
            }
        }

        private void Update()
        {
            if (isActive && bindedPathController.IsNotNull())
            {
                var v = bindedPathController.PathPercent / path.path.length;
                Debug.Log(v);
                if (!cameras.IsNullOrEmpty())
                {
                    for (int i = 0; i < cameras.Length; i++)
                    {
                        if (v >= cameras[i].percent)
                        {
                            CameraManager.Instance.SetCamera(cameras[i].camera);
                        }
                    }
                }
            }
        }
    }
}
