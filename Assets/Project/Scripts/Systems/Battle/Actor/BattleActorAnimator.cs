using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class BattleActorAnimator : SerializedMonoBehaviour
    {
        public Animator Animator;

        private bool isDefending = false;
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
        
        [CanBeNull] private const string SpellExecution1Name= "SpellExecution1";
        private static readonly int SpellExecution1Trigger = Animator.StringToHash(SpellExecution1Name);

        private void ResetTriggers()
        {
            Animator.ResetTrigger(DefendTrigger);
            Animator.ResetTrigger(ResurectTrigger);
            Animator.ResetTrigger(HitTrigger);
            Animator.ResetTrigger(DeathTrigger);
            Animator.ResetTrigger(SpellExecution1Trigger);
        }

        [Button]
        public void StartDefend()
        {
            if(isDefending)
                return;

            isDefending = true;
            
            ResetTriggers();
            Animator.SetTrigger(DefendTrigger);
            
            DOTween.To(() => IdleBlendValue,
                (x) => IdleBlendValue = x,
                1f,
                IdleBlendDuration)
                    .SetDelay(.25f)
                    .OnUpdate(delegate
                        {
                            Animator.SetFloat(IdleBlend, IdleBlendValue);
                        });
        }

        [Button]
        public void EndDefend()
        {
            if(!isDefending)
                return;

            isDefending = false;
            
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
            Debug.Log("DEATH ANIMATION");
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

        public void SpellExecution1()
        {
            ResetTriggers();
            Animator.SetTrigger(SpellExecution1Trigger);
        }

        public float GetCurrentClipDuration()
        {
            var currentClip = Animator.GetCurrentAnimatorClipInfo(0)[0];
            return currentClip.clip.length;
        }
    }
}
