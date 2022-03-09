using System.Collections;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class StateIcon : UIElement
    {
        [SerializeField, GetComponent] protected Image icon;
        
        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
