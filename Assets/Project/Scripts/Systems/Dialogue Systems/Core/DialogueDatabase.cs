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
    }
}
