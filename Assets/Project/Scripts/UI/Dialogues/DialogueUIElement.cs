using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class DialogueUIElement : UIElement
    {
        public float delayBeforeFade = 1f;
        public float fadeDuration = 1f;
        public float appearDuration = .5f;

        [SerializeField] private TextMeshProUGUI speakerName;
        [SerializeField] private AnimatedText animatedText;

        [SerializeField] private Image portrait;

        [GetComponent, SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private ActorDatabase actorDatabase;

        public bool textIsComplete => animatedText.isComplete;
        public AnimatedText AnimatedText => animatedText;
        
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);
            
            if (args is DialogueArg cast)
            {
                if (cast.Dialogue == null)
                {
                    canvasGroup.alpha = 0;
                    return;
                }
                
                animatedText.TextComponent.SetText(string.Empty);
                StartCoroutine(Display(cast.Dialogue.dialogueText));
                var db = actorDatabase.GetElement(cast.Dialogue.ActorEnum);
                speakerName.SetText(db.actorName);
                portrait.sprite = db.portrait;
            }
        }

        private IEnumerator Display(string text)
        {
            yield return new WaitForSeconds(appearDuration);
            animatedText.SetText(text);
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

        public IEnumerator DestroyElement(float duration)
        {
            canvasGroup.DOFade(0f, duration);
            yield return new WaitForSeconds(duration);
            Destroy(gameObject);
        }
    }
}
