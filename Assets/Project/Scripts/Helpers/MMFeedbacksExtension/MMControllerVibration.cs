using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using QRCode.Utils;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    [AddComponentMenu("")]
    [FeedbackPath("TheFowler/Manette Vibration")]
    public class MMControllerVibration : MMFeedback
    {
        public float lowFrequency, highFrequency, duration;
        
        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            GamepadVibration.Rumble(lowFrequency, highFrequency, duration);
        }
    }

    public static class GamepadVibration
    {
        private static IEnumerator rumble;
        
        
        public static void Rumble(float lowFrequency, float highFrequency, float duration)
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += delegate(PlayModeStateChange change)
            {
                if (change == PlayModeStateChange.ExitingPlayMode)
                {
                    Gamepad.current?.SetMotorSpeeds(0f, 0f);
                }
            };
#endif

            if(rumble != null) Coroutiner.Stop(rumble);
            rumble = RumbleIE(lowFrequency, highFrequency, duration);
            Coroutiner.Play(rumble);
        }

        private static IEnumerator RumbleIE(float lowFrequency, float highFrequency, float duration)
        {
            Gamepad.current?.SetMotorSpeeds(lowFrequency, highFrequency);
            yield return new WaitForSeconds(duration);
            Gamepad.current?.SetMotorSpeeds(0f, 0f);
        }
    }
}
