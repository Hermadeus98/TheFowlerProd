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

        private const string DefendName = "Defend";
        private static readonly int DefendTrigger = Animator.StringToHash(DefendName);
        
        private const string IdleBlendName = "IdleBlend";
        private static readonly int IdleBlend = Animator.StringToHash(IdleBlendName);
        private float IdleBlendValue;
        [SerializeField] private float IdleBlendDuration = .50f;
        
        private const string HitName = "Hit";
        private static readonly int HitTrigger = Animator.StringToHash(HitName);
        
        private const string ResurectName = "Resurect";
        private static readonly int ResurectTrigger = Animator.StringToHash(ResurectName);
        
        private const string DeathName = "Death";
        private static readonly int DeathTrigger = Animator.StringToHash(DeathName);

        private void ResetTriggers()
        {
            Animator.ResetTrigger(DefendTrigger);
        }

        [Button]
        public void StartDefend()
        {
            ResetTriggers();
            Animator.SetTrigger(DefendTrigger);
            IdleBlendValue = 1f;
            Animator.SetFloat(IdleBlend, IdleBlendValue);
        }

        [Button]
        public void EndDefend()
        {
            ResetTriggers();

            DOTween.To(
                () => IdleBlendValue,
                (x) => IdleBlendValue = x,
                0f,
                IdleBlendDuration
                ).OnUpdate(delegate
                    {
                        Animator.SetFloat(IdleBlend, IdleBlendValue);
                    });
        }
        
        [Button]
        public void Death()
        {
            ResetTriggers();
            Animator.SetTrigger(DeathTrigger);
        }

        [Button]
        public void Resurect()
        {
            ResetTriggers();
            Animator.SetTrigger(ResurectTrigger);
        }

        [Button]
        public void Hit()
        {
            ResetTriggers();
            Animator.SetTrigger(HitTrigger);
        }
    }
}
