using System;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Collections;
using UnityEngine.Playables;

namespace TheFowler
{
    public class DialogueHandler : GameplayPhase
    {
        [TitleGroup("General Settings"), Required]
        public BehaviourTree BehaviourTree;
        [TitleGroup("General Settings")]
        public ReplacementActors replacementActors;
        [TitleGroup("General Settings")]
        public bool playDialogueAnimations = false;


        [TitleGroup("General Settings")]
        [SerializeField] private bool displayChoiceResult = true;
        [TitleGroup("General Settings")]
        [SerializeField] private bool hasChoices = false;

        [TitleGroup("General Settings"), ShowIf("hasChoices")]
        [SerializeField] private UnityEngine.Events.UnityEvent eventChoice1, eventChoice2;
        private int choiceNumber;


        [TabGroup("Debug")]
        [SerializeField, ReadOnly] private DialogueNode currentDialogueNode;
        private Dialogue currentDialogue => currentDialogueNode.dialogue;
        
        [TabGroup("Debug")]
        [SerializeField, ReadOnly] private float elapsedTime = 0, elapsedTimePassCutscene = 0;

        [TitleGroup("General Settings")]
        [SerializeField] private DialogueType dialogueType;
        [TabGroup("References")]
        [SerializeField] private ActorActivator actorActivator;
        
        [TabGroup("References")]
        [SerializeField] private PlayerInput Inputs;
        [TabGroup("References")]
        [SerializeField] private PlayableDirector Timeline;

        [TabGroup("References")]
        [SerializeField] Animator robynAnim, phoebeAnim, abiAnim;
        [TabGroup("References"), ShowIf("@this.dialogueType == DialogueType.HARMONISATION")]
        [SerializeField] private Cinemachine.CinemachineVirtualCamera harmoVCam;

        public Animator currentAnim;
        private AK.Wwise.Event currentSound;

        private UIView currentView;

        [TabGroup("Debug")]
        private bool waitInput;

        private bool hasPassedDialogue = false;
        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            ChapterManager.onChapterChange += delegate(Chapter chapter) { EndPhase(); };
        }

        protected override void UnregisterEvent()
        {
            base.UnregisterEvent();
            ChapterManager.onChapterChange -= delegate(Chapter chapter) { EndPhase(); };
        }

        public override void PlayPhase()
        {
            if(Timeline != null)
            {
                Timeline.Play();

            }


            if (BehaviourTree.IsNull())
            {
                Debug.LogError($"Behaviour tree is missing on {GameplayPhaseID} !");
                return;
            }

            if (BehaviourTree.rootNode.IsNull())
            {
                BehaviourTree.SearchRootNode();
                
                if(BehaviourTree.rootNode.IsNotNull())
                    Debug.Log($"Root Node repared on {BehaviourTree.name} !");
                else
                {
                    Debug.LogError($"Root Node is missing on {BehaviourTree.name} !");
                }
            }
            
            base.PlayPhase();
            
            switch (dialogueType)
            {
                case DialogueType.STATIC:
                    //UI.OpenView(UI.Views.StaticDialogs);
                    PlaceActor();
                    break;
                case DialogueType.MOVEMENT:
                    //UI.OpenView(UI.Views.MovementDialogs);
                    UI.OpenView(UI.Views.StaticDialogs);
                    break;
                case DialogueType.HARMONISATION:
                    UI.OpenView(UI.Views.Harmo);
                    harmoVCam.gameObject.SetActive(true);
                    harmoVCam.Priority = 1000;
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
                    if(currentDialogueNode == null)
                        return;
                    
                    if (elapsedTime < currentDialogue.displayDuration)
                    {
                        elapsedTime += Time.deltaTime;
                    }
                    else
                    {
                        Next();
                        //Timeline.time = currentDialogue.displayDuration;
                        //Timeline.Pause();

                        //Next();
                    }
                }
                
                CheckInputs();
            }
        }

        private void CheckInputs()
        {
            if (isActive)
            {
                if (Inputs.actions["Next"].WasPressedThisFrame() && !waitInput)
                {
                    Next();
                    //if (hasPassedDialogue)
                    //{

                    //    Next();
                    //    Timeline.Play();
                    //    hasPassedDialogue = false;
                    //}
                    //else
                    //{

                    //    hasPassedDialogue = true;
                    //    var view = UI.GetView<HarmonisationView>(UI.Views.Harmo);
                    //    view.EndDialog(currentDialogue.dialogueText);

                    //    var view2 = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs);
                    //    view2.EndDialog(currentDialogue.dialogueText);
                    //}
                }
                if(dialogueType == DialogueType.STATIC)
                {
                    if (Inputs.actions["Return"].IsPressed() && !waitInput)
                    {
                        elapsedTimePassCutscene += Time.deltaTime;
                        if (currentView.GetType() == typeof(HarmonisationView))
                        {
                            var view = UI.GetView<HarmonisationView>(UI.Views.Harmo);
                            view.RappelInputFeedback(elapsedTimePassCutscene);
                        }

                        else if (currentView.GetType() == typeof(DialogueStaticView))
                        {
                            var view = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs);
                            view.RappelInputFeedback(elapsedTimePassCutscene);
                        }

                        if (elapsedTimePassCutscene >= 1)
                        {
                            
                            elapsedTimePassCutscene = 0;
                            var view = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs);
                            view.RappelInputFeedback(elapsedTimePassCutscene);
                            var view2 = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs);
                            view2.RappelInputFeedback(elapsedTimePassCutscene);
                            EndPhase();
                        }
                    }
                    if (Inputs.actions["Return"].WasReleasedThisFrame())
                    {
                        elapsedTimePassCutscene = 0;
                        var view = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs);
                        view.RappelInputFeedback(elapsedTimePassCutscene);
                        var view2 = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs);
                        view2.RappelInputFeedback(elapsedTimePassCutscene);
                    }
                }


                if (waitInput)
                {
                    if (displayChoiceResult)
                    {
                        switch (dialogueType)
                        {
                            case DialogueType.STATIC:
                            {
                                     
                                    if (currentView.GetType() == typeof(HarmonisationView))
                                    {
                                        var view = UI.GetView<HarmonisationView>(UI.Views.Harmo);
                                        var hasChoice = view.ChoiceSelector.WaitChoice(out currentDialogueNode, out choiceNumber);


                                        if (hasChoice)
                                        {
                                            DisplayDialogue(currentDialogue);
                                            switch (choiceNumber)
                                            {
                                                case 0:
                                                    eventChoice1.Invoke();
                                                    break;
                                                case 1:
                                                    eventChoice2.Invoke();
                                                    break;
                                            }
                                            waitInput = false;
                                        }
                                    }
                                    else
                                    {
                                        var view = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs);
                                        var hasChoice = view.ChoiceSelector.WaitChoice(out currentDialogueNode, out choiceNumber);


                                        if (hasChoice)
                                        {
                                            DisplayDialogue(currentDialogue);
                                            switch (choiceNumber)
                                            {
                                                case 0:
                                                    eventChoice1.Invoke();
                                                    break;
                                                case 1:
                                                    eventChoice2.Invoke();
                                                    break;
                                            }
                                            waitInput = false;
                                        }
                                    }
                                

                            }
                                break;
                            case DialogueType.MOVEMENT:
                            {
                                //var view = UI.GetView<DialogueMovementView>(UI.Views.MovementDialogs);
                                var view = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs);
                                var hasChoice = view.ChoiceSelector.WaitChoice(out currentDialogueNode, out choiceNumber);

                                if (hasChoice)
                                {
                                    DisplayDialogue(currentDialogue);

                                    waitInput = false;
                                }
                            }
                                break;
                            case DialogueType.HARMONISATION:
                                {
                                    var view = UI.GetView<HarmonisationView>(UI.Views.Harmo);
                                    var hasChoice = view.ChoiceSelector.WaitChoice(out currentDialogueNode, out choiceNumber);
                                    if (hasChoice)
                                    {
                                        DisplayDialogue(currentDialogue);
                                        switch (choiceNumber)
                                        {
                                            case 0:
                                                eventChoice1.Invoke();
                                                break;
                                            case 1:
                                                eventChoice2.Invoke();
                                                break;
                                        }
                                        waitInput = false;
                                    }
                                }
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    else
                    {
                        Next();
                    }
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
                        waitInput = true;
                        switch (dialogueType)
                        {
                            case DialogueType.STATIC:
                            {
                                    if (currentDialogue.isHarmonisation)
                                    {
                                        var view = UI.GetView<HarmonisationView>(UI.Views.Harmo);
                                        view.ChoiceSelector.Show();
                                        view.SetChoices(currentDialogueNode);
                                    }
                                    else
                                    {
                                        var view = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs);
                                        view.ChoiceSelector.Show();
                                        view.SetChoices(currentDialogueNode);
                                    }



                            }
                                break;
                            case DialogueType.MOVEMENT:
                            {
                                //var view = UI.GetView<DialogueMovementView>(UI.Views.MovementDialogs);
                                var view = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs);
                                view.ChoiceSelector.Show();
                                view.SetChoices(currentDialogueNode);
                            }
                                break;
                            case DialogueType.HARMONISATION:
                                {
                                    var view = UI.GetView<HarmonisationView>(UI.Views.Harmo);
                                    view.ChoiceSelector.Show();
                                    view.SetChoices(currentDialogueNode);


                                }
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    else
                    {
                        if(Timeline!=null)
                            Timeline.time += (currentDialogue.displayDuration - elapsedTime);

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

        public override void EndPhase()
        {

            if (dialogueType == DialogueType.STATIC && IsActive)
            {
                
                ReplaceActor(replacementActors.timeOfReplacement);
                SoundManager.StopSound(currentSound, gameObject);
            }


            if (Timeline != null)
            {
                Timeline.time = Timeline.duration;
            }

            base.EndPhase();
            
            switch (dialogueType)
            {
                case DialogueType.STATIC:
                    UI.CloseView(UI.Views.StaticDialogs);
                    UI.CloseView(UI.Views.Harmo);
                    break;
                case DialogueType.MOVEMENT:
                    //UI.CloseView(UI.Views.MovementDialogs);
                    UI.CloseView(UI.Views.StaticDialogs);
                    break;
                case DialogueType.HARMONISATION:
                    //UI.CloseView(UI.Views.MovementDialogs);
                    UI.CloseView(UI.Views.Harmo);
                    if(replacementActors.replaceActorAtTheEnd)
                        ReplaceActor(replacementActors.timeOfReplacement);

                    harmoVCam.Priority = -1;
                    harmoVCam.gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void DisplayDialogue(Dialogue dialogue)
        {
            
            
            switch (dialogueType)
            {
                case DialogueType.STATIC:
                    CameraManager.Instance.SetCamera(dialogue.cameraPath);

                    if (dialogue.isHarmonisation)
                    {
                       
                        
                        HarmonisationView view = UI.GetView<HarmonisationView>(UI.Views.Harmo);
                        
                        if (!view.isActive)
                        {
                            currentView = UI.GetView<HarmonisationView>(UI.Views.Harmo);
                            UI.CloseView(UI.Views.StaticDialogs);
                            UI.OpenView(UI.Views.Harmo);
                        }


                        UI.RefreshView(UI.Views.Harmo, new DialogueArg()
                        {
                            Dialogue = dialogue,
                            DialogueNode = currentDialogueNode,
                            Tree = BehaviourTree,
                        });
                    }
                    else
                    {
                        DialogueStaticView view = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs);
                        if (!view.isActive)
                        {
                            currentView = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs);
                            UI.CloseView(UI.Views.Harmo);
                            UI.OpenView(UI.Views.StaticDialogs);
                        }
                        UI.RefreshView(UI.Views.StaticDialogs, new DialogueArg()
                        {
                            Dialogue = dialogue,
                            DialogueNode = currentDialogueNode,
                            Tree = BehaviourTree,
                        });
                    }
                    
                    SoundManager.PlaySound(dialogue.voice, gameObject);

                    switch (dialogue.ActorEnum)
                    {
                        case ActorEnum.ROBYN:
                            currentAnim = robynAnim;
                            break;
                        case ActorEnum.PHEOBE:
                            currentAnim = phoebeAnim;
                            break;
                        case ActorEnum.ABIGAEL:
                            currentAnim = abiAnim;
                            break;
                    }

                    if(currentAnim != null && playDialogueAnimations)
                    {
                        currentAnim.SetTrigger(dialogue.animationTrigger.ToString());
                    }

                    currentSound = dialogue.voice;
                    break;
                case DialogueType.MOVEMENT:
                    //UI.RefreshView(UI.Views.MovementDialogs, new DialogueArg()
                    //{
                    //    Dialogue = dialogue,
                    //    DialogueNode = currentDialogueNode,
                    //});
                    CameraManager.Instance.SetCamera(dialogue.cameraPath);
                    UI.RefreshView(UI.Views.StaticDialogs, new DialogueArg()
                    {
                        Dialogue = dialogue,
                        DialogueNode = currentDialogueNode,
                    });
                    SoundManager.PlaySound(dialogue.voice, gameObject);
                    break;
                case DialogueType.HARMONISATION:
                    UI.RefreshView(UI.Views.Harmo, new DialogueArg()
                    {
                        Dialogue = dialogue,
                        DialogueNode = currentDialogueNode,
                    });
                    SoundManager.PlaySound(dialogue.voice, gameObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            elapsedTime = 0;
        }
        
        private void PlaceActor()
        {
            actorActivator?.ActivateActor();
        }

        public void ReplaceActor()
        {
            actorActivator?.DesactivateActor(replacementActors.replaceActorAtTheEnd);
        }


        private void ReplaceActor(float timer)
        {
            StartCoroutine(WaitReplaceActor(timer));
        }

        private IEnumerator WaitReplaceActor(float timer)
        {
            yield return new WaitForSeconds(timer);
            ReplaceActor();
            yield break;
        }

        public void PlayComplicityView()
        {
            ComplicityView view = UI.GetView<ComplicityView>(UI.Views.Complicity);
            view.Show();
        }



    }

    

    public class DialogueArg : EventArgs
    {
        public DialogueNode DialogueNode;
        public Dialogue Dialogue;
        public BehaviourTree Tree;
    }


    public enum ActorEnum
    {
        ROBYN,
        ABIGAEL,
        PHEOBE,
        GUARD,
        LIEUTENANT,
        EMPTY,
    }

    public enum DialogueType
    {
        STATIC = 0,
        MOVEMENT = 1,
        HARMONISATION = 2,
    }

    [Serializable]
    public struct ReplacementActors
    {
        public bool replaceActorAtTheEnd;
        public float timeOfReplacement;

    }
    
}


