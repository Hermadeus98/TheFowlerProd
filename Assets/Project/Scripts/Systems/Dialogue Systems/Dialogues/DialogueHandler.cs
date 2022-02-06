using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class DialogueHandler : GameplayPhase
    {
        public BehaviourTree BehaviourTree;

        [SerializeField] private DialogueNode currentDialogueNode;
        private Dialogue currentDialogue => currentDialogueNode.dialogue;
        
        [TabGroup("Debug")]
        [SerializeField, ReadOnly] private float elapsedTime = 0;

        [TitleGroup("General Settings")]
        [SerializeField] private DialogueType dialogueType;
        [TabGroup("References"), ShowIf("@this.dialogueType == DialogueType.STATIC")]
        [SerializeField] private ActorActivator actorActivator;
        
        [TabGroup("References")]
        [SerializeField] private PlayerInput Inputs;
        
        private bool waitInput;
        [SerializeField] private bool displayChoiceResult = true;
        
        public override void PlayPhase()
        {
            base.PlayPhase();
            
            switch (dialogueType)
            {
                case DialogueType.STATIC:
                    PlaceActor();
                    UI.OpenView("StaticDialogueView");
                    break;
                case DialogueType.MOVEMENT:
                    UI.OpenView("MovementDialogueView");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            currentDialogueNode = BehaviourTree.rootNode as DialogueNode;
            DisplayDialogue(currentDialogue);
        }

        private void Update()
        {
            if (isActive)
            {
                if (!waitInput)
                {
                    if (elapsedTime < currentDialogue.displayDuration)
                    {
                        elapsedTime += Time.deltaTime;
                    }
                    else
                    {
                        Next();
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (isActive)
            {
                if (Keyboard.current.spaceKey.wasPressedThisFrame && !waitInput)
                {
                    Next();
                }

                if (waitInput)
                {
                    MakeAChoice();
                }
            }
        }

        private void Next()
        {
            if (currentDialogueNode != null)
            {
                if (!currentDialogueNode.isLast)
                {
                    if (currentDialogueNode.hasMultipleChoices)
                    {
                        Debug.Log("choice");
                        waitInput = true;
                        switch (dialogueType)
                        {
                            case DialogueType.STATIC:
                                var view = UI.GetView<DialogueStaticView>("StaticDialogueView");
                                view.SetChoices(currentDialogueNode);
                                break;
                            case DialogueType.MOVEMENT:
                                //UI.CloseView("MovementDialogueView");
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    else
                    {
                        currentDialogueNode = currentDialogueNode.children[0] as DialogueNode;
                        DisplayDialogue(currentDialogue);
                    }
                }
                else
                {
                    currentDialogueNode = null;
                    elapsedTime = 0;
                    EndPhase();
                }
            }
            else
            {
                currentDialogueNode = null;
                elapsedTime = 0;
                EndPhase();
            }
        }

        private void MakeAChoice()
        {
            if (Keyboard.current.numpad1Key.wasPressedThisFrame)
            {
                if (displayChoiceResult)
                {
                    currentDialogueNode = currentDialogueNode.children[0] as DialogueNode;
                    DisplayDialogue(currentDialogue);
                }
                else
                {
                    Next();
                }
                
                waitInput = false;
            }
            if (Keyboard.current.numpad2Key.wasPressedThisFrame)
            {
                if (displayChoiceResult)
                {
                    currentDialogueNode = currentDialogueNode.children[1] as DialogueNode;
                    DisplayDialogue(currentDialogue);
                }
                else
                {
                    Next();
                }
                
                waitInput = false;
            }
        }

        public override void EndPhase()
        {
            if (dialogueType == DialogueType.STATIC)
            {
                ReplaceActor();
            }
            
            base.EndPhase();
            
            switch (dialogueType)
            {
                case DialogueType.STATIC:
                    UI.CloseView("StaticDialogueView");
                    break;
                case DialogueType.MOVEMENT:
                    UI.CloseView("MovementDialogueView");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DisplayDialogue(Dialogue dialogue)
        {
            CameraManager.Instance.SetCamera(dialogue.cameraPath);
            
            switch (dialogueType)
            {
                case DialogueType.STATIC:
                    UI.RefreshView("StaticDialogueView", new DialogueArg()
                    {
                        Dialogue = dialogue,
                        DialogueNode = currentDialogueNode,
                    });
                    break;
                case DialogueType.MOVEMENT:
                    UI.RefreshView("MovementDialogueView", new DialogueArg()
                    {
                        Dialogue = dialogue,
                        DialogueNode = currentDialogueNode,
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            Debug.Log(dialogue.dialogueText);
            elapsedTime = 0;
        }
        
        private void PlaceActor()
        {
            actorActivator?.ActivateActor();
        }

        private void ReplaceActor()
        {
            actorActivator?.DesactivateActor();
        }
    }

    public class DialogueArg : EventArgs
    {
        public DialogueNode DialogueNode;
        public Dialogue Dialogue;
    }

    [Flags]
    public enum ActorEnum
    {
        ROBYN,
        ABIGAEL,
        PHEOBE,
    }

    public enum DialogueType
    {
        STATIC = 0,
        MOVEMENT = 1,
    }
    
}


