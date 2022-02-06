using System;
using System.Collections;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using QRCode;
using UnityEngine;

namespace TheFowler
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DialogueChoiceSelector : UISelector
    {
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);
        }

        public void Refresh(DialogueNode[] nodes)
        {
            elements.ForEach(w => w.Hide());
            
            for (int i = 0; i < nodes.Length; i++)
            {
                elements[i].Show();
                elements[i].Refresh(new WrapperArgs<string>(nodes[i].dialogue.choiceText));
            }
        }

        public override void Show()
        {
            base.Show();
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            base.Hide();
            gameObject.SetActive(false);
        }
    }
}
