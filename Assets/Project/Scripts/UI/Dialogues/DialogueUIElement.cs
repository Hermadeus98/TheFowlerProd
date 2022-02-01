using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace TheFowler
{
    public class DialogueUIElement : UIElement
    {
        public bool isOccupy = false;
        public float delayBeforeFade = 1f;
        public float fadeDuration = 1f;
        public float appearDuration = .5f;

        [SerializeField] private TextMeshProUGUI dialogueBox;

        public int pos;

        [GetComponent, SerializeField] private CanvasGroup canvasGroup;
        
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (args is DialogueArg cast)
            {
                dialogueBox.SetText(cast.Dialogue.dialogueText);
            }
        }

        public override void Show()
        {
            base.Show();
            RectTransform.DOAnchorPosX(0f, appearDuration);
        }

        public override void Hide()
        {
            base.Hide();
            canvasGroup.DOFade(0f, fadeDuration).SetDelay(delayBeforeFade);
        }
    }
}
