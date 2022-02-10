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

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (Gamepad.current.xButton.wasPressedThisFrame)
            {
                CurrentIndex = 2;
            }
            else if (Gamepad.current.aButton.wasPressedThisFrame)
            {
                CurrentIndex = 1;
            }
            else if (Gamepad.current.bButton.wasPressedThisFrame)
            {
                CurrentIndex = 0;
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

            if (Gamepad.current.aButton.wasReleasedThisFrame
                || Gamepad.current.xButton.wasReleasedThisFrame
                || Gamepad.current.bButton.wasReleasedThisFrame)
            {
                Hide();
                dialogueNode = node;
                return true;
            }
            
            dialogueNode = null;
            return false;
        }
    }
}
