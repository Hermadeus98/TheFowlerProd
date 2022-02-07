using System;
using Nrjwolf.Tools.AttachAttributes;
using QRCode;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class UISelectorElement : UIElement
    {
        [TabGroup("References")]
        [SerializeField] private TextMeshProUGUI text;
        [TabGroup("References")]
        [SerializeField, GetComponent] protected CanvasGroup canvasGroup;
        [TabGroup("References")]
        [SerializeField, GetComponent] protected LayoutElement layoutElement;
        
        [TabGroup("Debug")]
        [ReadOnly] public bool interactable = true;
        
        public void Select()
        {
            transform.localScale = Vector3.one * 1.2f;
        }

        public void DeSelect()
        {
            transform.localScale = Vector3.one;
        }

        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (args is IWrapper<string> cast)
            {
                text.SetText(cast.Value);
            }
        }

        public override void Show()
        {
            base.Show();
            canvasGroup.alpha = 1;
            layoutElement.ignoreLayout = false;
            interactable = true;
        }

        public override void Hide()
        {
            base.Hide();
            canvasGroup.alpha = 0;
            layoutElement.ignoreLayout = true;
            interactable = false;
        }
    }
}
