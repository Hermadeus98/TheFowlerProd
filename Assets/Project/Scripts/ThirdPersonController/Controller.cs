using System;
using System.Collections;
using System.Collections.Generic;
using CMF;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class Controller : SerializedMonoBehaviour
    {
        [SerializeField] private ControllerData controllerData;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform model;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private ThirdPersonAnimatorController animatorController;
        
        private const float GRAVITY_FORCE = -9.81f;
        private Vector3 savedVelocity;
        private float currentYRot;

        private float horizontal, vertical;
        
        private void Update()
        {
            SetVelocity(savedVelocity);
            animatorController.UpdateAnimatorValues(
                Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical))
                );
        }

        private void FixedUpdate()
        {
            savedVelocity = CalculateMovementVelocity();
        }

        private void LateUpdate()
        {
            TurnModel();
        }

        

        private void TurnModel()
        {
            var vel = savedVelocity;
            vel = Vector3.ProjectOnPlane(vel, transform.up);

            var magnitudeThreshold = 0.001f;
            if(vel.magnitude < magnitudeThreshold)
                return;

            vel.Normalize();
            
            var curForward = model.forward;
            var angleDif = VectorMath.GetAngle(curForward, vel, transform.up);
            var factor = Mathf.InverseLerp(0f, 90f, Mathf.Abs(angleDif));
            float step = Mathf.Sign(angleDif) * factor * Time.deltaTime * controllerData.RotationSpeed;
            
            if (angleDif < 0f && step < angleDif)
                step = angleDif;
            else if (angleDif > 0f && step > angleDif)
                step = angleDif;
            
            currentYRot += step;
            
            if(currentYRot > 360f)
                currentYRot -= 360f;
            if(currentYRot < -360f)
                currentYRot += 360f;
            
            model.localRotation = Quaternion.Euler(0f, currentYRot, 0f);
        }

        private Vector3 CalculateMovementVelocity()
        {
            var vel = Vector3.zero;

            horizontal = playerInput.actions["move"].ReadValue<Vector2>().x;
            vertical = playerInput.actions["move"].ReadValue<Vector2>().y;

            vel += Vector3.ProjectOnPlane(CameraManager.Camera.transform.right, transform.up).normalized *
                   horizontal;
            vel += Vector3.ProjectOnPlane(CameraManager.Camera.transform.forward, transform.up).normalized *
                   vertical;

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
