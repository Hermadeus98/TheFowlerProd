using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class CreditView : UIView
    {
        [SerializeField] private Animator animator;

        [SerializeField] private string play = "play", stop = "stop";

        public static CreditView Instance;

        public AnimationClip clip;

        public UnityEvent onEnd;

        private Coroutine wait;
        
        protected override void OnStart()
        {
            Instance = this;
            base.OnStart();
        }

        public override void Show()
        {
            CanvasGroup.alpha = 1f;
            isActive = true;
            animator.SetTrigger(play);
            wait = StartCoroutine(Wait());
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(clip.length);
            Hide();
        }

        public override void Hide()
        {
            CanvasGroup.alpha = 0f;
            isActive = false;
            animator.SetTrigger(stop);
            
            onEnd?.RemoveAllListeners();
            StopCoroutine(wait);
        }
    }
}
