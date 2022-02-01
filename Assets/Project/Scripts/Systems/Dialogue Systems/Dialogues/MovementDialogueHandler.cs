using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class MovementDialogueHandler : SerializedMonoBehaviour, IDialoguePhase
    {
        [SerializeField] private string dialogue_id;
        private bool isActive = false;
        
        [SerializeField] private DialogueDatabase Dialogues;

        [SerializeField] private GameInstructions OnEnd;
        
        private int currentDialogueCount = 0;
        private float elapsedTime = 0;
        
        public string Dialogue_id { get => dialogue_id; set => dialogue_id = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        
        public void PlayDialoguePhase()
        {
            isActive = true;
            
            UI.OpenView("StaticDialogueView");
        }

        public void Next()
        {
            
        }
    }
}
