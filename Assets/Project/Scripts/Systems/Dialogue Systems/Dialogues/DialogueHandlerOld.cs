using System;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class DialogueHandlerTest : GameplayPhase
    {
        [TitleGroup("General Settings")]
        [SerializeField] private DialogueType dialogueType;
        [TitleGroup("General Settings")]
        [SerializeField] private DialogueDatabase Dialogues;
        
        [TabGroup("References")]
        [SerializeField] private PlayerInput Inputs;
        [TabGroup("References"), ShowIf("@this.dialogueType == DialogueType.STATIC")]
        [SerializeField] private ActorActivator actorActivator;

        [TabGroup("Debug")]
        [SerializeField, ReadOnly] private int currentDialogueCount = 0;
        [TabGroup("Debug")]
        [SerializeField, ReadOnly] private float elapsedTime = 0;

        public override void PlayPhase()
        {
            base.PlayPhase();
            PlayDialoguePhase();
        }

        public void PlayDialoguePhase()
        {
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

        public void Next()
        {
            var staticDialogueView = UI.GetView<DialogueStaticView>("StaticDialogueView");
            var movementDialogueView = UI.GetView<DialogueMovementView>("MovementDialogueView");

            if (dialogueType == DialogueType.STATIC && !staticDialogueView.textIsComplete)
            {
                staticDialogueView.AnimatedText.Complete();
                return;
            }

            if (movementDialogueView.currentDialogueElement != null)
            {
                if (dialogueType == DialogueType.MOVEMENT &&
                    !movementDialogueView.currentDialogueElement.textIsComplete)
                {
                    movementDialogueView.currentDialogueElement.AnimatedText.Complete();
                    return;
                }
            }

            currentDialogueCount++;

            if (currentDialogueCount < Dialogues.Dialogues.Length)
            {            
                var currentDialogue = Dialogues.Dialogues[currentDialogueCount];
                DisplayDialogue(currentDialogue);
                elapsedTime = 0;
            }
            else
            {
                EndPhase();
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

        public void DisplayDialogue(Dialogue dialogue)
        {
            CameraManager.Instance.SetCamera(dialogue.cameraPath);
            switch (dialogueType)
            {
                case DialogueType.STATIC:
                    UI.RefreshView("StaticDialogueView", new DialogueArg()
                    {
                        Dialogue = dialogue,
                    });
                    break;
                case DialogueType.MOVEMENT:
                    UI.RefreshView("MovementDialogueView", new DialogueArg()
                    {
                        Dialogue = dialogue,
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
            actorActivator?.ActivateActor();
        }

        private void ReplaceActor()
        {
            actorActivator?.DesactivateActor();
        }
        
        //---<EDITOR>--------------------------------------------------------------------------------------------------<
#if UNITY_EDITOR
        [MenuItem("GameObject/LD/Dialogue/Dialogue Statique", false, 20)]
        private static void CreateStaticDialogue(MenuCommand menuCommand)
        {
            var obj = Resources.Load("Dialogues/Dialogue Statique");
            var go = PrefabUtility.InstantiatePrefab(obj, Selection.activeTransform) as GameObject;
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            go.name = obj.name;
            Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
            Selection.activeObject = go;
        }
        
        [MenuItem("GameObject/LD/Dialogue/Dialogue Movement", false, 20)]
        private static void CreateMovementDialogue(MenuCommand menuCommand)
        {
            var obj = Resources.Load("Dialogues/Dialogue Movement");
            var go = PrefabUtility.InstantiatePrefab(obj, Selection.activeTransform) as GameObject;
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            go.name = obj.name;
            Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
            Selection.activeObject = go;
        }
#endif
    }
}