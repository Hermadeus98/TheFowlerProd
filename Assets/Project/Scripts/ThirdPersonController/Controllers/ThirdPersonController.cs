using System;
using System.Collections;
using System.Collections.Generic;
using CMF;
using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class ThirdPersonController : CharacterControllerBase
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private ThirdPersonAnimatorController animatorController;

        private const float GRAVITY_FORCE = -9.81f;

        private float horizontal, vertical;

        public bool useInput;
        public Vector2 input;


        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();
            
            if(!isActive)
                return;
            
            SetVelocity(savedVelocity);

            var moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            switch (controllerMovement)
            {
                case ControllerMovement.WALK:
                    moveAmount = Mathf.Clamp(moveAmount, 0, .5f);
                    break;
                case ControllerMovement.RUN:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            animatorController.UpdateAnimatorValues(moveAmount);
        }
        
        private void FixedUpdate()
        {
            if(!isActive)
                return;
            
            if (useInput)
            {
                horizontal = playerInput.actions["move"].ReadValue<Vector2>().x;
                vertical = playerInput.actions["move"].ReadValue<Vector2>().y;
            }
            else
            {
                horizontal = input.x;
                vertical = input.y;
            }
            
            savedVelocity = CalculateMovementVelocity(horizontal, vertical);
        }

        private void LateUpdate()
        {
            if(!isActive)
                return;
            
            TurnModel();
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
        }

        private Vector3 CalculateMovementVelocity(float h, float v)
        {
            var vel = Vector3.zero;

            vel += Vector3.ProjectOnPlane(CameraManager.Camera.transform.right, transform.up).normalized *
                   h;
            vel += Vector3.ProjectOnPlane(CameraManager.Camera.transform.forward, transform.up).normalized *
                   v;

            if (vel.magnitude > 1f)
                vel.Normalize();

            vel *= controllerData.MovementSpeed;
            
            return vel;
        }

        private Vector3 SetVelocity(Vector3 velocity)
        {
            if (characterController.isGrounded)
                velocity.y = 0f;
            
            velocity.y += GRAVITY_FORCE;

            characterController.Move(velocity * Time.deltaTime);
            
            return velocity;
        }
    }
}
