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

        public void Show()
        {
            CanvasGroup.alpha = 1f;
            isActive = true;
            animator.SetTrigger(play);
            wait = StartCoroutine(Wait());
        }

        IEnumerator Wait()
        {
            var t = clip.length;
            while (t > 0)
            {
                t -= Time.deltaTime;
                if (Gamepad.current.bButton.wasPressedThisFrame)
                {
                    Hide();
                    t = 0;
                }
                
                yield return null;
            }
            Hide();
        }

        public override void Hide()
        {
            CanvasGroup.alpha = 0f;
            isActive = false;
            animator.SetTrigger(stop);
            
            onEnd?.Invoke();
            onEnd?.RemoveAllListeners();
            StopCoroutine(wait);
        }
    }
}
