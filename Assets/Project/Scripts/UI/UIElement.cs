using System;
using System.Collections;
using System.Collections.Generic;
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
