using System;
using System.Collections;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class UISelector : UIElement
    {
        [SerializeField] protected List<UISelectorElement> elements = new List<UISelectorElement>();

        [SerializeField] protected bool resetIndex;

        [SerializeField, GetComponent] private CanvasGroup canvasGroup;

        [SerializeField, GetComponent] private PlayerInput Inputs;
        
        private int currentIndex;
        private UISelectorElement currentSelectedElement;

        private void FixedUpdate()
        {
            if(!isActive)
                return;
            
            Navigate();
        }

        private void Navigate()
        {
            if (Inputs.actions["NavigateDown"].WasPressedThisFrame())
            {
                SelectNext();
            }
            
            if (Inputs.actions["NavigateUp"].WasPressedThisFrame())
            {
                SelectPrevious();
            }
        }

        private void SelectNext()
        {
            currentIndex++;
            if (currentIndex >= elements.Count)
            {
                currentIndex = elements.Count - 1;
            }

            SelectElement();
        }

        private void SelectPrevious()
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = 0;
            }

            SelectElement();
        }

        private void SelectElement()
        {
            currentSelectedElement?.DeSelect();
            currentSelectedElement = elements[currentIndex];
            currentSelectedElement.Select();
        }
        
        public override void Show()
        {
            base.Show();

            if (resetIndex)
                currentIndex = 0;
            
            SelectElement();
        }

        public override void Hide()
        {
            base.Hide();
            currentSelectedElement = null;
        }
    }
}
