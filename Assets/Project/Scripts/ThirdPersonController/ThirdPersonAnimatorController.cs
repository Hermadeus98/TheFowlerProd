using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class ThirdPersonAnimatorController : SerializedMonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float smoothValue = 3f;
        [SerializeField] private float smoothTolerance = .05f;
        
        [ReadOnly, SerializeField] private float moveAmount;
        [ReadOnly, SerializeField] private float smoothMoveAmount;
        
        private void Update()
        {
            if (Math.Abs(smoothMoveAmount - moveAmount) > smoothTolerance)
            {
                if (smoothMoveAmount < moveAmount)
                {
                    smoothMoveAmount += Time.deltaTime * smoothValue;
                }
                else if (smoothMoveAmount > moveAmount)
                {
                    smoothMoveAmount -= Time.deltaTime * smoothValue;
                }
            }

            if (moveAmount == 0)
            {
                smoothMoveAmount -= Time.deltaTime * smoothValue;
                Mathf.Clamp01(smoothMoveAmount);
            }
            
            UpdateAnimator(smoothMoveAmount);
        }

        public void UpdateAnimatorValues(float moveAmount)
        {
            this.moveAmount = moveAmount;
        }

        private void UpdateAnimator(float moveAmount)
        {
            animator.SetFloat("MovementBlend", moveAmount);
        }
    }
}
