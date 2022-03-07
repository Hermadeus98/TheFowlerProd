using System;
using System.Linq;
using QRCode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class DialogueStaticView : UIView
    {
        [SerializeField] private Image portrait;
        [SerializeField] private ActorDatabase actorDatabase;

        [SerializeField] private AnimatedText animatedText;
        [SerializeField] private TextMeshProUGUI speakerName;

        [SerializeField] private DialogueChoiceSelector choiceSelector;
        [SerializeField] private Image rappelInputFill, background;

        public bool textIsComplete => animatedText.isComplete;
        public AnimatedText AnimatedText => animatedText;
        public DialogueChoiceSelector ChoiceSelector => choiceSelector;
        
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (args is DialogueArg cast)
            {
                
                var db = actorDatabase.GetElement(cast.Dialogue.ActorEnum);
                portrait.sprite = db.portraitBuste;

                if(cast.Dialogue.dialogueText != "")
                {
                    background.enabled = true;
                    speakerName.enabled = true;
                    animatedText.enabled = true;
                    speakerName.SetText(db.actorName);
                    animatedText.SetText(cast.Dialogue.dialogueText);
                }
                else
                {
                    background.enabled = false;
                    speakerName.enabled = false;
                    animatedText.enabled = false;
                    animatedText.SetText(cast.Dialogue.dialogueText);
                }

            }
        }

        public void SetChoices(DialogueNode dialogueNode)
        {
            if (dialogueNode.hasMultipleChoices)
            {
                choiceSelector.Refresh(dialogueNode.children.Cast<DialogueNode>().ToArray());
                return;
            }
        }

        public void DisplaySentence(DialogueNode node)
        {
            
            speakerName.SetText(node.dialogue.ActorEnum.ToString());
            animatedText.SetText(node.dialogue.dialogueText);
        }

        public void RappelInputFeedback(float elapsedTime)
        {
            rappelInputFill.fillAmount = elapsedTime;
        }
    }
}
