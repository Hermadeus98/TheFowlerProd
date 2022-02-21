using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class PathController : CharacterControllerBase
    {
        [SerializeField] private PlayerInput playerInput;
        
        private Coroutine moveAlongWaypointsCoroutine;
        
        float vertical = 0;
        [SerializeField, ReadOnly] float pathpercent = 0;
        
        public PathCreator path;

        [SerializeField]
        private float speedCoef;

        private bool calculate = false;
        private bool hasReachTheEnd;

        private float position;
        [SerializeField] private float movementLerp = .2f;
        
        public enum PathControllerType
        {
            Auto,
            Manual,
        }

        public PathControllerType pathControllerType;
        public float verticalBinding = 0;

        [Button]
        public void MoveAlongWayPath(PathCreator path, Action OnComplete = null)
        {
            if(!isActive)
                return;

            if(moveAlongWaypointsCoroutine != null)
                StopCoroutine(moveAlongWaypointsCoroutine);
                
            moveAlongWaypointsCoroutine = StartCoroutine(IEMoveAlongWayPath(path, OnComplete));
        }

        IEnumerator IEMoveAlongWayPath(PathCreator path, Action OnComplete)
        {
            hasReachTheEnd = false;
            pathpercent = 0;
            calculate = true;
            
            while (!hasReachTheEnd)
            {
                yield return null;
            }

            calculate = false;
            OnComplete?.Invoke();
            Debug.Log("Complete");
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();
            
            if(!isActive)
                return;
            
            if (pathpercent >= path.path.length - 0.1f)
                hasReachTheEnd = true;

            switch (pathControllerType)
            {
                case PathControllerType.Auto:
                    vertical = verticalBinding;
                    break;
                case PathControllerType.Manual:
                    vertical = Mathf.Clamp01(Mathf.Abs(playerInput.actions["move"].ReadValue<Vector2>().y));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            savedVelocity.x = vertical;
                
            pathpercent += (vertical * controllerData.MovementSpeed) / speedCoef * Time.deltaTime;
            pathpercent = Mathf.Clamp(pathpercent, 0, path.path.length);

            position = Mathf.Lerp(position, pathpercent, movementLerp);
             
            character.pawnTransform.position = path.path.GetPointAtDistance(position);
                
            var rot = path.path.GetRotationAtDistance(pathpercent).eulerAngles;
            currentYRot = rot.y;
            model.localRotation = Quaternion.Euler(0f, currentYRot, 0f);

            var moveAmount = Mathf.Abs(savedVelocity.x) + Mathf.Abs(savedVelocity.z);
            UpdateAnimatorController(moveAmount);
        }
    }
}
