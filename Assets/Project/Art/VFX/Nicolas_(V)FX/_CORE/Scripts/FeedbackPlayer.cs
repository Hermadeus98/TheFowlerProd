using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FeedbackPlayer : MonoBehaviour
{
    public MoreMountains.Feedbacks.MMFeedbacks fb;

#if UNITY_EDITOR
    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame) Play();
    }

#endif
    public void Play()
    {
        fb.PlayFeedbacks();
    }
}
