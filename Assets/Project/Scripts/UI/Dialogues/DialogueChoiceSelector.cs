using System.Collections.Generic;
using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DialogueChoiceSelector : UISelector
    {
        [TabGroup("Debug")]
        [SerializeField, ReadOnly] private List<DialogueNode> dialogueNodes = new List<DialogueNode>();
        
        public void Refresh(DialogueNode[] nodes)
        {
            ResetElements();

            HideAllElements();
            DeselectedAll();
            elements.ForEach(w => w.Hide());
            elements.Clear();
            dialogueNodes = new List<DialogueNode>(nodes);
            
            for (int i = 0; i < dialogueNodes.Count; i++)
            {
                elements.Add(transform.GetChild(i).GetComponent<UISelectorElement>());
                elements[i].Show();
                elements[i].Refresh(new WrapperArgs<string>(nodes[i].dialogue.choiceText));
            }
            
            SelectElement();
        }

        public override void Show()
        {
            base.Show();
            canvasGroup.alpha = 1;
        }

        public override void Hide()
        {
            base.Hide();
            canvasGroup.alpha = 0f;
        }


        public bool WaitChoice(out DialogueNode dialogueNode, out int choiceNumber)
        {
            var node = dialogueNodes[currentIndex];

            if(selectorType == SelectorType.NAVIGATION)
            {
                if (Inputs.actions["Select"].WasPressedThisFrame())
                {
                    Hide();
                    dialogueNode = node;
                    choiceNumber = -1;
                    return true;
                }

            }
            else if (selectorType == SelectorType.BUTTON)
            {
                switch (dialogueNodes.Count)
                {
                    case 1:
                        if (Inputs.actions["A"].WasPressedThisFrame())
                        {
                            currentIndex = 0;

                            Hide();
                            dialogueNode = dialogueNodes[currentIndex];
                            choiceNumber = 0;
                            return true;
                        }
                        break;
                    case 2:
                        if (Inputs.actions["A"].WasPressedThisFrame())
                        {
                            currentIndex = 0;

                            Hide();
                            dialogueNode = dialogueNodes[currentIndex];
                            choiceNumber = 0;
                            return true;
                        }
                        else if (Inputs.actions["B"].WasPressedThisFrame())
                        {
                            currentIndex = 1;

                            Hide();
                            dialogueNode = dialogueNodes[currentIndex];
                            choiceNumber = 1;
                            return true;
                        }
                        break;
                    case 3:
                        if (Inputs.actions["A"].WasPressedThisFrame())
                        {
                            currentIndex = 0;

                            Hide();
                            dialogueNode = dialogueNodes[currentIndex];
                            choiceNumber = 0;
                            return true;
                        }
                        else if (Inputs.actions["B"].WasPressedThisFrame())
                        {
                            currentIndex = 1;

                            Hide();
                            dialogueNode = dialogueNodes[currentIndex];
                            choiceNumber = 1;
                            return true;
                        }
                        else if (Inputs.actions["C"].WasPressedThisFrame())
                        {

                            currentIndex = 2;

                            Hide();
                            dialogueNode = dialogueNodes[currentIndex];
                            choiceNumber = 2;
                            return true;
                        }

                        
                        break;
                }


            }

            dialogueNode = null;
            choiceNumber = -1;
            return false;
        }


    }
}
