using System;
using System.Collections;
using Cinemachine;
using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace TheFowler
{
    public class ThirdPersonController : CharacterControllerBase
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private PlayerInput playerInput;
        
        private const float GRAVITY_FORCE = -9.81f;

        private float horizontal, vertical;

        public bool useInput;
        public Vector2 input;

        [SerializeField] private bool resetCamera = true;

        [SerializeField, Required] private bool applyMove = true;
        
        [ShowInInspector, ReadOnly] public float SpeedModifier { get; set; }

        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            agent.enabled = true;
            
            if(resetCamera)
                SetCameraToTPSCamera();

            SpeedModifier = 1f;
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();
            
            if(!isActive)
                return;
            
            SetVelocity(savedVelocity);

            var moveAmount = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            UpdateAnimatorController(moveAmount * SpeedModifier);
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

            if(applyMove && characterController.gameObject.activeInHierarchy) 
                characterController.Move(velocity * SpeedModifier * Time.deltaTime);
            
            return velocity;
        }

        public override void OnSetControllerMovement(ControllerMovement controllerMovement)
        {
            base.OnSetControllerMovement(controllerMovement);
            switch (controllerMovement)
            {
                case ControllerMovement.WALK:
                    //CameraManager.Instance.SetCamera("Robyn_TPS","TPS_Walk");
                    break;
                case ControllerMovement.RUN:
                    //CameraManager.Instance.SetCamera("Robyn_TPS","TPS_Run");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(controllerMovement), controllerMovement, null);
            }
        }
        
        public void SetCameraToTPSCamera()
        {
            CameraManager.Instance.SetCamera(CameraGenericKey.GetCameraGenericKey(CameraGenericKeyEnum.TPS_CAMERA));
            CameraManager.Instance.SetCamera("Robyn_TPS", "CM TPS Explo");
            SpeedModifier = 1f;
        }
    }
}
