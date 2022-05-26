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

        public void Refresh(string text, string speaker)
        {
            animatedText.SetText(text);
            speakerName.text = speaker;
        }
        
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (args is DialogueArg cast)
            {
                
                var db = actorDatabase.GetElement(cast.Dialogue.ActorEnum);
                portrait.sprite = db.portraitBuste;

                if (LocalisationManager.language == Language.ENGLISH)
                {
                    if (!String.IsNullOrEmpty(cast.Dialogue.dialogueText))
                    {
                        background.enabled = true;
                        speakerName.enabled = true;
                        animatedText.GetComponent<TextMeshProUGUI>().enabled = true;
                        if (cast.DialogueNode.dialogue.dialogueText.Length >= 20)
                        {
                            animatedText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                        }
                        else
                        {
                            animatedText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                        }
                        animatedText.SetText(cast.Dialogue.dialogueText);
                        speakerName.SetText(db.actorName);
                        animatedText.SetText(cast.Dialogue.dialogueText);
                    }
                    else
                    {
                        background.enabled = false;
                        speakerName.enabled = false;
                        animatedText.GetComponent<TextMeshProUGUI>().enabled = false;

                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(cast.Dialogue.dialogueTextFrench))
                    {
                        background.enabled = true;
                        speakerName.enabled = true;
                        animatedText.GetComponent<TextMeshProUGUI>().enabled = true;
                        if (cast.DialogueNode.dialogue.dialogueTextFrench.Length >= 20)
                        {
                            animatedText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                        }
                        else
                        {
                            animatedText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                        }
                        animatedText.SetText(cast.Dialogue.dialogueTextFrench);
                        speakerName.SetText(db.actorName);
                        animatedText.SetText(cast.Dialogue.dialogueTextFrench);
                    }
                    else
                    {
                        background.enabled = false;
                        speakerName.enabled = false;
                        animatedText.GetComponent<TextMeshProUGUI>().enabled = false;

                    }
                }

                

            }
        }

        public void EndDialog(string text)
        {
            animatedText.TextComponent.text = text;
            animatedText.StopAllCoroutines();
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

            if(LocalisationManager.language == Language.ENGLISH)
            {
                animatedText.SetText(node.dialogue.dialogueText);
            }
            else
            {
                animatedText.SetText(node.dialogue.dialogueTextFrench);
            }


        }

    }
}
