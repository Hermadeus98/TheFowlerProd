using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class BattleActorAnimator : SerializedMonoBehaviour
    {
        public Animator Animator;

        [SerializeField] private AnimationClip attackClip;

        public void Death()
        {
            ResetTriggers();
            Animator.SetTrigger("Death");
        }

        public void ResetAnimator()
        {
            ResetTriggers();
            Animator.SetTrigger("Reset");
        }

        public void Idle()
        {
            ResetTriggers();
            Animator.SetTrigger("Idle");
        }

        public void Parry()
        {
            ResetTriggers();
            Animator.SetTrigger("Parry");
        }

        public void AttackPreview()
        {
            ResetTriggers();
            Animator.SetTrigger("AttackPreview");
        }

        public float AttackCastDuration() => attackClip.length;
        public void AttackCast()
        {
            ResetTriggers();
            Animator.SetTrigger("AttackCast");
        }

        public void Hit()
        {
            ResetTriggers();
            Animator.SetTrigger("Hit");
        }

        private void ResetTriggers()
        {
            Animator.ResetTrigger("Death");
            Animator.ResetTrigger("AttackCast");
            Animator.ResetTrigger("AttackPreview");
            Animator.ResetTrigger("Idle");
            Animator.ResetTrigger("Reset");
            Animator.ResetTrigger("Hit");
            Animator.ResetTrigger("Parry");
        }
    }
}
