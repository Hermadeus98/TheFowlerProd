using System;
using CMF;
using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace TheFowler
{
    public class CharacterControllerBase : GameplayMonoBehaviour, Istate
    {
        [SerializeField]
        private string stateName;
        public string StateName { get => stateName; set => stateName = value; }

        [SerializeField] protected Character character;
        [SerializeField] protected Transform model;
        [SerializeField] protected NavMeshAgent agent;
        [SerializeField] protected ThirdPersonAnimatorController animatorController;

        [SerializeField] protected ControllerPresets controllerPresets;
        [SerializeField, ReadOnly] protected ControllerData controllerData;

        protected bool isActive = false;
        protected ControllerMovement controllerMovement;
        protected Vector3 savedVelocity;
        protected float currentYRot;
        
        public virtual void OnStateEnter(EventArgs arg)
        {
            isActive = true;
        }

        public virtual void OnStateExecute()
        {
        }

        public virtual void OnStateExit(EventArgs arg)
        {
            isActive = false;
        }
        
        protected void TurnModel()
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
        
        protected void UpdateAnimatorController(float moveAmount)
        {
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

        public virtual void OnSetControllerMovement(ControllerMovement controllerMovement)
        {
            this.controllerMovement = controllerMovement;
            controllerData = controllerPresets.GetElement(controllerMovement);
        }

        public virtual void OnChangeController()
        {
            
        }
    }
    
    public enum ControllerMovement{
        WALK = 0,
        RUN = 1,
    }
}
