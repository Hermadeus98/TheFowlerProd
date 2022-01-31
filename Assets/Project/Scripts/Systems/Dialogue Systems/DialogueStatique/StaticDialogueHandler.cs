using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class StaticDialogueHandler : GameplayMonoBehaviour, IDialoguePhase
    {
        [SerializeField] private string dialogue_id;
        
        [SerializeField] private PlayerInput Inputs;
        
        //Initialisation
        [SerializeField] private ActorPositionner actorPositionner;
        [SerializeField] private InitilizationMode initilizationMode = InitilizationMode.TELEPORT;
        [SerializeField] private DialogueDatabase Dialogues;

        //End
        [SerializeField] private GameInstructions OnEnd;

        private bool isActive = false;

        private int currentDialogueCount = 0;
        private float elapsedTime = 0;

        public string Dialogue_id { get => dialogue_id; set => dialogue_id = value; }
        public bool IsActive { get => isActive; set => isActive = value; }

        public void PlayDialoguePhase()
        {
            if (initilizationMode == InitilizationMode.TELEPORT)
            {
                PlaceActor();
            }
            
            UI.OpenView("StaticDialogueView");
            isActive = true;

            elapsedTime = 0;
            currentDialogueCount = 0;
            DisplayDialogue(Dialogues.Dialogues[currentDialogueCount]);
        }

        private void Update()
        {
            if(!isActive)
                return;
            var currentDialogue = Dialogues.Dialogues[currentDialogueCount];

            if (elapsedTime < currentDialogue.displayDuration)
            {
                elapsedTime += Time.deltaTime;
            }
            else
            {
                Next();
            }
        }

        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            DialogueManager.RegisterDialoguePhase(this);
        }

        protected override void UnregisterEvent()
        {
            base.UnregisterEvent();
            DialogueManager.UnregisterDialoguePhase(this);
        }

        public void Next()
        {
            currentDialogueCount++;

            if (currentDialogueCount < Dialogues.Dialogues.Length)
            {            
                var currentDialogue = Dialogues.Dialogues[currentDialogueCount];
                DisplayDialogue(currentDialogue);
                elapsedTime = 0;
            }
            else
            {
                isActive = false;
                OnEnd.Call();
                UI.CloseView("StaticDialogueView");
            }
        }

        public void DisplayDialogue(Dialogue dialogue)
        {
            CameraManager.Instance.SetCamera(dialogue.cameraPath);
            UI.RefreshView("StaticDialogueView", new DialogueArg()
            {
                Dialogue = dialogue,
            });
        }

        private void FixedUpdate()
        {
            if(!isActive)
                return;
            
            if (Inputs.actions["Next"].WasPressedThisFrame())
            {
                Next();
            }
        }
        
        private void PlaceActor()
        {
            actorPositionner.UpdatePosition();
        }
    }

    public interface IDialoguePhase
    {
        public string Dialogue_id { get; set; }
        public bool IsActive { get; set; }
        public void PlayDialoguePhase();

        public void Next();
    }

    public class DialogueArg : EventArgs
    {
        public Dialogue Dialogue;
    }

    [Flags]
    public enum ActorEnum
    {
        ROBYN,
        ABIGAEL,
        PHEOBE,
    }

    public enum InitilizationMode
    {
        MOVE_TO,
        TELEPORT
    }
}