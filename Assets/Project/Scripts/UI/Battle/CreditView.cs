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

        public bool isEndCredit;
        private bool canQuit;
        public GameObject quitSigns;
        
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

            if (isEndCredit)
            {
                wait = StartCoroutine(WaitEnd());

            }
            else
            {

                wait = StartCoroutine(Wait());
            }


        }

        public void Update()
        {
            if(isActive && canQuit)
            {
                if (Gamepad.current.aButton.wasPressedThisFrame)
                {
                    //System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe"));
                    Application.Quit();
                    Debug.Log("Quit");
                }
            }
        }

        IEnumerator WaitEnd()
        {
            var t = clip.length;
            while (t > 0)
            {
                t -= Time.deltaTime;
                if (t <= 0)
                {
                    ShowQuit();
                    t = 0;
                }

                yield return null;
            }
        }

        private void ShowQuit()
        {
            canQuit = true;
            quitSigns.SetActive(true);
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
