using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class AttackPreviewStateMachine : StateMachineBehaviour
    {
        private FeedbackReferences references;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            references = animator.gameObject.GetComponent<FeedbackReferences>();
            references.actionPreviewIn?.PlayFeedbacks();
        }


        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            
            
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            references.actionPreviewOut?.PlayFeedbacks();
        }

    }

}
