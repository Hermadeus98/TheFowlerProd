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
        [SerializeField] protected TextMeshProUGUI text;
        [TabGroup("References")]
        [SerializeField, GetComponent] public CanvasGroup canvasGroup;
        [TabGroup("References")]
        [SerializeField, GetComponent] protected LayoutElement layoutElement;
        
        [TabGroup("Debug")]
        [ReadOnly] public bool interactable = true;

        public virtual void Select()
        {
            transform.localScale = Vector3.one * 1.2f;
        }

        public virtual void DeSelect()
        {
            transform.localScale = Vector3.one;
        }

        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (args is WrapperArgs<string> cast)
            {
                text.SetText(cast.Arg);
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

        public virtual void OnClick()
        {
            
        }
    }
}
