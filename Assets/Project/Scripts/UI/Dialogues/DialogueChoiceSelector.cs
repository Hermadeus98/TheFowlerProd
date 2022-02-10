using System;
using System.Collections;
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

        protected void Update()
        {

            switch (dialogueNodes.Count)
            {
                case 1:
                    if (Inputs.actions["A"].IsPressed())
                    {
                        CurrentIndex = 0;
                    }
                    break;
                case 2:
                    if (Inputs.actions["A"].IsPressed())
                    {
                        CurrentIndex = 0;
                    }
                    else if (Inputs.actions["B"].IsPressed())
                    {
                        CurrentIndex = 1;
                    }
                    break;
                case 3:
                    if (Inputs.actions["A"].IsPressed())
                    {
                        CurrentIndex = 0;
                    }
                    else if (Inputs.actions["B"].IsPressed())
                    {
                        CurrentIndex = 2;
                    }
                    else if (Inputs.actions["C"].IsPressed())
                    {
                        CurrentIndex = 1;
                    }
                    break;
            }

            
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


        public bool WaitChoice(out DialogueNode dialogueNode)
        {
            Debug.Log("WaitChoice");
            var node = dialogueNodes[currentIndex];

            if(selectorType == SelectorType.NAVIGATION)
            {
                if (Inputs.actions["Select"].WasPressedThisFrame())
                {
                    Hide();
                    dialogueNode = node;
                    return true;
                }

            }
            else if (selectorType == SelectorType.BUTTON)
            {
                switch (dialogueNodes.Count)
                {
                    case 1:
                        if (Inputs.actions["A"].WasReleasedThisFrame())
                        {
                            Hide();
                            dialogueNode = node;
                            return true;
                        }
                        break;
                    case 2:
                        if (Inputs.actions["A"].WasReleasedThisFrame() || Inputs.actions["B"].WasPressedThisFrame())
                        {
                            Hide();
                            dialogueNode = node;
                            return true;
                        }
                        break;
                    case 3:
                        if (Inputs.actions["A"].WasReleasedThisFrame() ||
                            Inputs.actions["B"].WasPressedThisFrame() ||
                            Inputs.actions["C"].WasPressedThisFrame())
                        {
                            Hide();
                            dialogueNode = node;
                            return true;
                        }
                        break;
                }

            }

            dialogueNode = null;
            return false;
        }
    }
}
