using System;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class UIElement : GameplayMonoBehaviour
    {
        private RectTransform rectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (rectTransform.IsNull())
                    rectTransform = GetComponent<RectTransform>();
                return rectTransform;
            }
        }

        protected bool isActive;

        private Canvas canvas;

        public Canvas Canvas
        {
            get
            {
                if (canvas.IsNull())
                    canvas = GetComponentInParent<Canvas>();

                return canvas;
            }
        }
        
        [Button]
        public virtual void Show()
        {
            isActive = true;
        }

        [Button]
        public virtual void Hide()
        {
            isActive = false;
        }

        public virtual void Refresh(EventArgs args)
        {
            
        }
    }
}
