using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class NavMeshController : CharacterControllerBase
    {
        [SerializeField] private NavMeshPresets NavMeshPresets;
        
        private Coroutine moveAlongWaypointsCoroutine;

        
        [Button]
        public void GoTo(Transform transform)
        {
            if (!isActive)
                return;

            if(agent.isActiveAndEnabled)
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
                while (Vector3.Distance(agent.transform.position, waypoints[i].position) > .5f)
                {
                    yield return null;
                }
            }
            
            OnComplete?.Invoke();
        }

        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            
            agent.enabled = true;

            if(agent.isActiveAndEnabled)
                agent.ResetPath();
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

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            
            if(agent.isActiveAndEnabled)
                agent.ResetPath();
            
            if(moveAlongWaypointsCoroutine != null)
                StopCoroutine(moveAlongWaypointsCoroutine);
        }

        public void ApplyNavMeshAgentPresset(ControllerMovement controllerMovement)
        {
            var presset = NavMeshPresets.GetElement(controllerMovement);

            DOTween.To(
                () => agent.speed,
                (x) => agent.speed = x,
                presset.Speed,
                2f
            ).SetEase(Ease.InOutSine);

            agent.angularSpeed = presset.AngularSpeed;
            agent.acceleration = presset.Acceleration;
        }

        public override void OnSetControllerMovement(ControllerMovement controllerMovement)
        {
            base.OnSetControllerMovement(controllerMovement);
            ApplyNavMeshAgentPresset(controllerMovement);
        }
    }
}
