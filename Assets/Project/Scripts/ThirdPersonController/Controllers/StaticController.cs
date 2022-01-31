using System;
using UnityEngine;

namespace TheFowler
{
    public class StaticController : CharacterControllerBase
    {
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            
            agent.enabled = false;
            
            if (agent.isActiveAndEnabled)
            {
                agent.ResetPath();
                agent.isStopped = true;
            }
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();
            var moveAmount = 0;
            UpdateAnimatorController(moveAmount);
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);
            
            if (agent.isActiveAndEnabled)
            {
                agent.ResetPath();
                agent.isStopped = false;
            }
        }

        public void TeleportTo(Transform teleportTransform)
        {
            if(!isActive)
                return;

            character.pawnTransform.transform.position = teleportTransform.position;
            character.pawnTransform.transform.rotation = teleportTransform.rotation;
            model.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
