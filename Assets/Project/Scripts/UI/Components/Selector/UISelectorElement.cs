using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using TMPro;
using UnityEngine;

namespace TheFowler
{
    public class UISelectorElement : UIElement
    {
        [SerializeField] private TextMeshProUGUI text;
        
        public bool interactable = true;
        
        public void Select()
        {
            
        }

        public void DeSelect()
        {
            
        }

        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (args is IWrapper<string> cast)
            {
                text.SetText(cast.Value);
            }
        }
    }
}
