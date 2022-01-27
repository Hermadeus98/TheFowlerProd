using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace TheFowler
{
    public class NavMeshController : CharacterControllerBase
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private NavMeshPresets NavMeshPresets;
        
        private Coroutine moveAlongWaypointsCoroutine;
        
        [Button]
        public void GoTo(Transform transform)
        {
            if (!isActive)
                return;

            agent.SetDestination(transform.position);
        }

        [Button]
        public void MoveAlongWayPoints(Waypoints waypoint, Action OnComplete = null)
        {
            if(!isActive)
                return;
            
            if(moveAlongWaypointsCoroutine != null)
                StopCoroutine(moveAlongWaypointsCoroutine);
                
            moveAlongWaypointsCoroutine = StartCoroutine(IEMoveAlongWayPoints(waypoint.GetWayPoints, OnComplete));
        }

        IEnumerator IEMoveAlongWayPoints(Transform[] waypoints, Action OnComplete)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                GoTo(waypoints[i]);
                while (Vector3.Distance(agent.transform.position, waypoints[i].position) > .2f)
                {
                    yield return null;
                }
            }
            
            OnComplete?.Invoke();
        }

        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();
            
            if (!isActive)
                return;

            savedVelocity = agent.velocity;
            
            var moveAmount = Mathf.Abs(savedVelocity.x) + Mathf.Abs(savedVelocity.z);
            UpdateAnimatorController(moveAmount);
        }

        private void LateUpdate()
        {
            if(!isActive)
                return;
            TurnModel();
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
            ApplyNavMeshAgentPresset(controllerMovement);
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
        }
    }
}
