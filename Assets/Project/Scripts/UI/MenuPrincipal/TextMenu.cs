using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace TheFowler
{
    public class TextMenu : UISelectorElement
    {
        [SerializeField] private Color normalColor;
        [SerializeField] private Color selectedColor;

        [SerializeField] private UnityEvent OnSelect;
        
        public override void Select()
        {
            text.color = selectedColor;
        }

        public override void DeSelect()
        {
            text.color = normalColor;
        }

        public override void OnClick()
        {
            base.OnClick();
            OnSelect?.Invoke();
        }
    }
}
