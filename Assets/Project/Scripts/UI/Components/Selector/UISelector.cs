using System;
using System.Collections;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class UISelector : UIElement
    {
        [TabGroup("Main Settings")]
        [SerializeField] protected SelectorType selectorType;
        [TabGroup("Main Settings")]
        [SerializeField] protected List<UISelectorElement> all_elements = new List<UISelectorElement>();
        
        [TabGroup("Debug")]
        [SerializeField, ReadOnly] protected List<UISelectorElement> elements = new List<UISelectorElement>();

        [TabGroup("Main Settings")]
        [SerializeField] protected bool resetIndex;


        [TabGroup("References")]
        [SerializeField, GetComponent] protected CanvasGroup canvasGroup;

        [TabGroup("References")]
        [SerializeField, GetComponent] protected PlayerInput Inputs;
        
        [TabGroup("Debug")]
        [SerializeField, ReadOnly] protected int currentIndex;
        [TabGroup("Debug")]
        [SerializeField, ReadOnly] protected UISelectorElement currentSelectedElement;

        [SerializeField] protected int index;

        protected Action<UISelectorElement> OnSelect;

        protected virtual void Update()
        {
            if(!isActive)
                return;

            switch (selectorType)
            {
                case SelectorType.NAVIGATION:
                    Navigate();
                    break;

            }
        }

        private void Navigate()
        {
            if (Inputs.actions["NavigateDown"].WasPressedThisFrame())
            {
                SelectNext();
                OnNavigate();
            }

            if (Inputs.actions["NavigateUp"].WasPressedThisFrame())
            {
                SelectPrevious();
                OnNavigate();
            }

            if (Inputs.actions["A"].WasPressedThisFrame())
            {
                currentSelectedElement?.OnClick();
            }
        }

        protected virtual void OnNavigate()
        {
            
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

        protected virtual void SelectElement()
        {
            currentSelectedElement?.DeSelect();
            currentSelectedElement = elements[currentIndex];
            currentSelectedElement.Select();
            OnSelect?.Invoke(currentSelectedElement);
        }
        
        public override void Show()
        {
            base.Show();

            if (resetIndex)
                currentIndex = 0;

            canvasGroup.alpha = 1;
        }

        public override void Hide()
        {
            base.Hide();
            currentSelectedElement = null;
            canvasGroup.alpha = 0;
        }

        protected void DeselectedAll()
        {
            elements.ForEach(w => w.DeSelect());
        }

        protected void HideAllElements()
        {
            elements.ForEach(w => w.Hide());
        }

        protected void ResetElements()
        {
            elements = new List<UISelectorElement>(all_elements);
        }

        protected void SelectFirst()
        {
            SelectElement();
        }
    }

    public enum SelectorType
    {
        NAVIGATION,
        BUTTON
    }

}
