using System;
using System.Collections;
using System.Collections.Generic;
using QRCode.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace TheFowler
{
    public class NavMeshFollower : CharacterControllerBase
    {
        [SerializeField] private NavMeshPresets NavMeshPresets;
        private NavMeshPreset currentNavMeshPresset => NavMeshPresets.GetElement(controllerMovement);


        [SerializeField] private CharacterPlugger characterPlugger;
        private Follower Follower;
        

        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            
            agent.enabled = true;

            Follower = Follower.GetFollower(characterPlugger);
            
            if(agent.isActiveAndEnabled)
                agent.ResetPath();
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();
            
            if (!isActive)
                return;
            
            if(!agent.isActiveAndEnabled)
                return;
            
            Follow();
            
            savedVelocity = agent.velocity;
            var moveAmount = Mathf.Abs(savedVelocity.x) + Mathf.Abs(savedVelocity.z);
            UpdateAnimatorController(moveAmount);
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            
            if(agent.isActiveAndEnabled)
                agent.ResetPath();
        }

        private void Follow()
        {
            if (Follower == null)
            {
                Follower = Follower.GetFollower(characterPlugger);
                return;
            }
            
            if(Vector3.Distance(agent.transform.position, Follower.transform.position) > currentNavMeshPresset.MinimalDistanceFollow)
            {
                agent.SetDestination(Follower.Target.position);
            }

            if (Vector3.Distance(agent.transform.position, Follower.transform.position) < currentNavMeshPresset.DistanceOfWalking)
            {
                OnSetControllerMovement(ControllerMovement.WALK);
            }
            else
            {
                OnSetControllerMovement(ControllerMovement.RUN);
            }
            
            if (Vector3.Distance(agent.transform.position, Follower.transform.position) > currentNavMeshPresset.MaxDistanceFollow)
            {
                if(currentNavMeshPresset.TeleportIfBigDistance) 
                    TeleportToFollower();
            }
        }

        private void TeleportToFollower()
        {
            agent.transform.position = Follower.transform.position;
        }
        
        private void ApplyNavMeshAgentPresset(ControllerMovement controllerMovement)
        {
            var presset = NavMeshPresets.GetElement(controllerMovement);

            agent.speed = presset.Speed;
            agent.angularSpeed = presset.AngularSpeed;
            agent.acceleration = presset.Acceleration;
        }
        
        public override void OnSetControllerMovement(ControllerMovement controllerMovement)
        {
            base.OnSetControllerMovement(controllerMovement);
            if (this.controllerMovement != controllerMovement)
            {
                ApplyNavMeshAgentPresset(controllerMovement);
            }
        }

        public override void OnChangeController()
        {
            base.OnChangeController();
        }
    }
}
