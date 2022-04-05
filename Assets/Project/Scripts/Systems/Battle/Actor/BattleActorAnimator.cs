using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class BattleActorAnimator : SerializedMonoBehaviour
    {
        public Animator Animator;

        [SerializeField] private AnimationClip attackClip;
        
        
        private static readonly int DefendBlend = Animator.StringToHash("DefendBlend");

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

        public void SetDefend(bool state)
        {
            if (state)
            {
                float x = 0f;
                DOTween.To(
                    () => x,
                    (x) => x = x,
                    1f,
                    .5f
                    ).OnUpdate(delegate
                {
                    Animator.SetFloat(DefendBlend, x);
                });
            }
            else
            {
                float x = 1f;
                DOTween.To(
                    () => x,
                    (x) => x = x,
                    0f,
                    .5f
                ).OnUpdate(delegate
                {
                    Animator.SetFloat(DefendBlend, x);
                });
            }
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
