using System;
using System.Collections;
using System.Collections.Generic;
using CMF;
using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class CharacterControllerBase : SerializedMonoBehaviour, Istate
    {
        [SerializeField]
        private string stateName;
        [HideInInspector] public string StateName { get => stateName; set => stateName = value; }

        [SerializeField] protected Transform model;

        [SerializeField] protected ControllerPresets controllerPresets;
        [SerializeField] protected ControllerData controllerData;

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

        public virtual void OnSetControllerMovement(ControllerMovement controllerMovement)
        {
            this.controllerMovement = controllerMovement;
            controllerData = controllerPresets.GetElement(controllerMovement);
        }
    }
    
    public enum ControllerMovement{
        WALK = 0,
        RUN = 1,
    }
}
