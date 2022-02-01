using System;
using System.Collections;
using System.Collections.Generic;
using QRCode.Extensions;
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
        
        public virtual void Show()
        {
            
        }

        public virtual void Hide()
        {
            
        }

        public virtual void Refresh(EventArgs args)
        {
            
        }
    }
}
