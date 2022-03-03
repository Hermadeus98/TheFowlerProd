using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu(menuName = CreateAssetMenuPath.dialogueData)]
    public class DialogueDatabase : SerializedScriptableObject
    {
        public Dialogue[] Dialogues;
    }

    [Serializable]
    public class Dialogue
    {
        public ActorEnum ActorEnum;
        [TextArea(3,5)] public string dialogueText;
        public cameraPath cameraPath;
        public float displayDuration = 2f;
        public string choiceText;
        public bool isHarmonisation = false;
        public AK.Wwise.Event voice;
        public AnimationTriggerName animationTrigger;
    }

    [System.Serializable]
    public enum AnimationTriggerName
    {
            IDLE,
            Walk,
            Attack,
            IDLE_Combat
    }
}
