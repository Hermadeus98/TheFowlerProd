using System;
using System.Collections;
using Cinemachine;
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
        [SerializeField] private CinemachineVirtualCameraBase TPS_Camera_VM;
        
        private const float GRAVITY_FORCE = -9.81f;

        private float horizontal, vertical;

        public bool useInput;
        public Vector2 input;

        [SerializeField] private bool resetCamera = true;
        [SerializeField] private cameraPath TPS_Camera;

        [SerializeField, Required] private bool applyMove = true;

        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            agent.enabled = true;
            
            if(resetCamera)
                SetCameraToTPSCamera();
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();
            
            if(!isActive)
                return;
            
            SetVelocity(savedVelocity);

            var moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            UpdateAnimatorController(moveAmount);
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

            if(applyMove) characterController.Move(velocity * Time.deltaTime);
            
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
            CameraManager.Instance.SetCamera(TPS_Camera);
        }

        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            //ChapterManager.onChapterChange += delegate(Chapter chapter) { applyMove = false; };
            //ChapterManager.onChapterLoaded += delegate(Chapter chapter) { StartCoroutine(ApplyMove()); };
        }

        IEnumerator ApplyMove()
        {
            yield return new WaitForSeconds(.5f);
            applyMove = true;
        }

        protected override void UnregisterEvent()
        {
            base.UnregisterEvent();
            //ChapterManager.onChapterChange -= delegate(Chapter chapter) { applyMove = false; };
            //ChapterManager.onChapterLoaded -= delegate(Chapter chapter) { StartCoroutine(ApplyMove()); };
        }
    }
}
