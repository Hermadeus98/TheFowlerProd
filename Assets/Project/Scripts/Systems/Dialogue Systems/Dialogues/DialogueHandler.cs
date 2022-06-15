using System;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;

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
        [TabGroup("References")]
        [SerializeField] Sockets[] guardsSockets;
        [TabGroup("References")]
        [SerializeField] TimelineCutscene timelineCutscene;
        [TabGroup("References"), ShowIf("@this.dialogueType == DialogueType.HARMONISATION")]
        [SerializeField] private Cinemachine.CinemachineVirtualCamera harmoVCam;

        public Animator currentAnim;
        private AK.Wwise.Event currentSound;
        private GameObject currentSoundEmitter;

        private UIView currentView;

        [SerializeField] private int idGuards = 0;
        private bool hasPassChoices;
        [TabGroup("Debug")]
        private bool waitInput;

        private bool hasPassedDialogue = false;
        private bool canPass = false;
        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            ChapterManager.onChapterChange += delegate(Chapter chapter) { Finish(); };
        }

        protected override void UnregisterEvent()
        {
            base.UnregisterEvent();
            ChapterManager.onChapterChange -= delegate(Chapter chapter) { Finish(); };
        }

        private void Finish()
        {
            if (isActive) { EndPhase(); }
        }

        public override void PlayPhase()
        {
            Player.canOpenPauseMenu = false;

            FindObjectOfType<GameTimer>().incrementeDialogueTimer = true;



            if (Timeline != null)
            {
                CinemachineBrain cineCam =  CameraManager.Camera.GetComponent<CinemachineBrain>();


                foreach (var playableAssetOutput in Timeline.playableAsset.outputs)
                {
                    if (playableAssetOutput.outputTargetType == typeof(CinemachineBrain))
                    {
                        Timeline.SetGenericBinding(playableAssetOutput.sourceObject, cineCam);
                        
                    }

                }



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
                        //if (currentDialogue.dialogueText.Length <= 20)
                        //{
                        //    Next();
                        //}
                        //else
                        //{
                            
                        //    //Timeline.Pause();
                        //}


                    }
                }
                
                CheckInputs();
            }
        }

        private void CheckInputs()
        {
            if (isActive)
            {
                if (Inputs.actions["Next"].WasReleasedThisFrame() && !waitInput && canPass)
                {
                    if(elapsedTimePassCutscene < 0.3f)
                    {
                        if (currentView.GetType() == typeof(HarmonisationView))
                        {
                            if (LocalisationManager.language == Language.ENGLISH)
                            {
                                if (!hasPassedDialogue &&
                               !String.IsNullOrEmpty(currentDialogue.dialogueText) &&
                               UI.GetView<HarmonisationView>(UI.Views.Harmo).AnimatedText.TextComponent.text != currentDialogue.dialogueText)
                                {

                                    UI.GetView<HarmonisationView>(UI.Views.Harmo).EndDialog(currentDialogue.dialogueText);
                                    hasPassedDialogue = true;
                                }
                                else
                                {
                                    Next();
                                }
                            }
                            else
                            {


                                if (!hasPassedDialogue &&
                               !String.IsNullOrEmpty(currentDialogue.dialogueTextFrench) &&
                               UI.GetView<HarmonisationView>(UI.Views.Harmo).AnimatedText.TextComponent.text != currentDialogue.dialogueTextFrench)
                                {

                                    UI.GetView<HarmonisationView>(UI.Views.Harmo).EndDialog(currentDialogue.dialogueTextFrench);
                                    hasPassedDialogue = true;
                                }
                                else
                                {
                                    Next();
                                }
                            }

                           

                            
                        }
                        else if (currentView.GetType() == typeof(DialogueStaticView))
                        {
                            if (LocalisationManager.language == Language.ENGLISH)
                            {
                                if (!hasPassedDialogue &&
                               !String.IsNullOrEmpty(currentDialogue.dialogueText) &&
                               UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs).AnimatedText.TextComponent.text != currentDialogue.dialogueText)
                                {

                                    UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs).EndDialog(currentDialogue.dialogueText);
                                    hasPassedDialogue = true;
                                }
                                else
                                {
                                    Next();
                                }

                            }
                            else
                            {
                                if (!hasPassedDialogue &&
                               !String.IsNullOrEmpty(currentDialogue.dialogueTextFrench) &&
                               UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs).AnimatedText.TextComponent.text != currentDialogue.dialogueTextFrench)
                                {

                                    UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs).EndDialog(currentDialogue.dialogueTextFrench);
                                    hasPassedDialogue = true;
                                }
                                else
                                {
                                    Next();
                                }

                            }


                        }

                       

                    }
                    
                }

                CallRappelInput();


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

        private void CallRappelInput()
        {
            var view = currentView;

            if (currentView == null) return;

            view = UI.GetView<DialogueStaticView>(UI.Views.StaticDialogs);

            if (Inputs.actions["Select"].IsPressed() && !waitInput && canPass)
            {
                elapsedTimePassCutscene += Time.deltaTime;
                

                if (elapsedTimePassCutscene >= 1)
                {
                    elapsedTimePassCutscene = 0;
                    if(view.rappelInput != null)
                        view.rappelInput?.RappelInputFeedback(elapsedTimePassCutscene);

                    if(BehaviourTree.dialogueType == DialogueType.HARMONISATION)
                    {
                        if (!hasPassChoices)
                        {
                            for (int i = 0; i < BehaviourTree.nodes.Count; i++)
                            {


                                DialogueNode newNode = BehaviourTree.nodes[i] as DialogueNode;

                                 if (!newNode.hasMultipleChoices)
                                {
                                    Next();

                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            EndPhase();
                        }

                    }

                    else if (BehaviourTree.dialogueType == DialogueType.STATIC)
                    {
                        EndPhase();
                    }


                }

                else if(elapsedTimePassCutscene > .2f && elapsedTimePassCutscene < 1)
                {
                    if (view.rappelInput != null)
                        view.rappelInput?.RappelInputFeedback(elapsedTimePassCutscene);
                }
            }
            else if (Inputs.actions["Select"].WasReleasedThisFrame())
            {
                elapsedTimePassCutscene = 0;
                if (view.rappelInput != null)
                    view.rappelInput?.RappelInputFeedback(elapsedTimePassCutscene);
            }

            else if (Inputs.actions["Select"].WasPressedThisFrame())
            {
                canPass = true;
            }



        }
        private void Next()
        {
            hasPassedDialogue = false;
            if (currentDialogueNode != null)
            {
                if (!currentDialogueNode.isLast)
                {
                    if (currentDialogueNode.hasMultipleChoices)
                    {
                        
                        hasPassChoices = true;
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

            //if (Timeline != null)
            //{
            //    Timeline.
            //    Timeline.time = BehaviourTree.GetDuration() - 0.001f;
            //    Timeline.Pause();
            //}


            FindObjectOfType<GameTimer>().incrementeDialogueTimer = false;
            
            hasPassChoices = false;


            if (dialogueType == DialogueType.STATIC && IsActive)
            {
                
                ReplaceActor(replacementActors.timeOfReplacement);
                SoundManager.StopSound(currentSound, currentSoundEmitter);
            }

            if(timelineCutscene != null)
            {
                timelineCutscene.HideTwoDCutscene();

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
                default:
                    throw new ArgumentOutOfRangeException();
            }
            idGuards = 0;

        }

  

        private void DisplayDialogue(Dialogue dialogue)
        {
            if(Timeline != null)
            {
                Debug.Log("PLAYY");
                Timeline.Play();
            }


            switch (dialogueType)
            {
                case DialogueType.STATIC:
                    //CameraManager.Instance.SetCamera(dialogue.cameraPath);

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

                    switch (dialogue.ActorEnum)
                    {
                        
                        case ActorEnum.ROBYN:
                            if(robynAnim != null)
                            {
                                SoundManager.PlaySound(dialogue.voice, robynAnim.GetComponent<AnimTriggerBase>().Sockets.body_Middle.gameObject);
                                currentSoundEmitter = robynAnim.GetComponent<AnimTriggerBase>().Sockets.body_Middle.gameObject;
                            }
                            else
                            {
                                SoundManager.PlaySound(dialogue.voice, Player.Robyn.pawnTransform.gameObject);
                                currentSoundEmitter = Player.Robyn.pawnTransform.gameObject;
                            }

                            currentAnim = robynAnim;
                            break;
                        case ActorEnum.PHEOBE:
                            if (phoebeAnim != null)
                            {
                                SoundManager.PlaySound(dialogue.voice, phoebeAnim.GetComponent<AnimTriggerBase>().Sockets.body_Middle.gameObject);
                                currentSoundEmitter = phoebeAnim.GetComponent<AnimTriggerBase>().Sockets.body_Middle.gameObject;
                            }
                            else
                            {
                                SoundManager.PlaySound(dialogue.voice, Player.Pheobe.pawnTransform.gameObject);
                                currentSoundEmitter = Player.Pheobe.pawnTransform.gameObject;

                            }
                            
                            currentAnim = phoebeAnim;
                            break;
                        case ActorEnum.ABIGAEL:
                            if (abiAnim != null)
                            {
                                SoundManager.PlaySound(dialogue.voice, abiAnim.GetComponent<AnimTriggerBase>().Sockets.body_Middle.gameObject);
                                currentSoundEmitter = abiAnim.GetComponent<AnimTriggerBase>().Sockets.body_Middle.gameObject;
                            }
                            else
                            {
                                SoundManager.PlaySound(dialogue.voice, Player.Abigael.pawnTransform.gameObject);
                                currentSoundEmitter = Player.Abigael.pawnTransform.gameObject;

                            }

                            currentAnim = abiAnim;
                            break;
                        case ActorEnum.GUARD:
                            
                            if(guardsSockets.Length> 0 && guardsSockets[idGuards] != null)
                            {
                                SoundManager.PlaySound(dialogue.voice, guardsSockets[idGuards].body_Middle.gameObject);
                                currentSoundEmitter = guardsSockets[idGuards].body_Middle.gameObject;
                                idGuards++;
                            }

                            break;
                        case ActorEnum.LIEUTENANT:
                            SoundManager.PlaySound(dialogue.voice, gameObject);
                            currentSoundEmitter = gameObject;
                            break;
                        default:
                            SoundManager.PlaySound(dialogue.voice, gameObject);
                            currentSoundEmitter = gameObject;
                            break;
                    }

                    if(currentAnim != null && playDialogueAnimations)
                    {
                        currentAnim.SetTrigger(dialogue.animationTrigger.ToString());
                    }

                    currentSound = dialogue.voice;
                    break;
                case DialogueType.MOVEMENT:
                    UI.RefreshView(UI.Views.StaticDialogs, new DialogueArg()
                    {
                        Dialogue = dialogue,
                        DialogueNode = currentDialogueNode,
                    });
                    switch (dialogue.ActorEnum)
                    {

                        case ActorEnum.ROBYN:
                            if (robynAnim != null)
                            {
                                SoundManager.PlaySound(dialogue.voice, robynAnim.GetComponent<AnimTriggerBase>().Sockets.body_Middle.gameObject);
                            }
                            else
                            {
                                SoundManager.PlaySound(dialogue.voice, Player.Robyn.pawnTransform.gameObject);
                            }

                            currentAnim = robynAnim;
                            break;
                        case ActorEnum.PHEOBE:
                            if (phoebeAnim != null)
                            {
                                SoundManager.PlaySound(dialogue.voice, phoebeAnim.GetComponent<AnimTriggerBase>().Sockets.body_Middle.gameObject);
                            }
                            else
                            {
                                SoundManager.PlaySound(dialogue.voice, Player.Pheobe.pawnTransform.gameObject);

                            }

                            currentAnim = phoebeAnim;
                            break;
                        case ActorEnum.ABIGAEL:
                            if (abiAnim != null)
                            {
                                SoundManager.PlaySound(dialogue.voice, abiAnim.GetComponent<AnimTriggerBase>().Sockets.body_Middle.gameObject);
                            }
                            else
                            {
                                SoundManager.PlaySound(dialogue.voice, Player.Abigael.pawnTransform.gameObject);

                            }

                            currentAnim = abiAnim;
                            break;
                        case ActorEnum.GUARD:

                            if (guardsSockets[idGuards] != null)
                            {
                                
                                SoundManager.PlaySound(dialogue.voice, guardsSockets[idGuards].body_Middle.gameObject);
                                idGuards++;
                            }
                            else
                            {
                                SoundManager.PlaySound(dialogue.voice, gameObject);
  
                            }

                            break;
                        case ActorEnum.LIEUTENANT:
                            SoundManager.PlaySound(dialogue.voice, gameObject);
                            break;
                        default:
                            SoundManager.PlaySound(dialogue.voice, gameObject);
                            break;
                    }

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
            if (Timeline != null)
            {
                Timeline.Pause();
            }
            yield return new WaitForSeconds(timer);
            if (Timeline != null)
            {
                Timeline.Stop();
            }

            ReplaceActor();
            yield break;
        }

        public void PlayComplicityView(string actor)
        {
            ComplicityView view = UI.GetView<ComplicityView>(UI.Views.Complicity);
            view.Show((ActorEnum)Enum.Parse(typeof(ActorEnum), actor));
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


