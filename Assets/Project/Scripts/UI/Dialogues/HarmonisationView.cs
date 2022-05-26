using System;
using System.Linq;
using QRCode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using MoreMountains.Feedbacks;
using DG.Tweening;

namespace TheFowler
{
    public class HarmonisationView : UIView
    {
        [TabGroup("References")]
        [SerializeField] private GameObject choice, abigail, phoebe, abigailSolo;

        [TabGroup("References")]
        [SerializeField] private MMFeedbacks MMfadeIn, MMfadeOut;

        [SerializeField] private Image portraitLeft,portraitRight, rappelInputFill;

        [SerializeField] private ActorDatabase actorDatabase;

        [SerializeField] private AnimatedText animatedText;
        [SerializeField] private TextMeshProUGUI speakerName;
        [SerializeField] private DialogueChoiceSelector choiceSelector;
        
        public bool textIsComplete => animatedText.isComplete;
        public AnimatedText AnimatedText => animatedText;
        public DialogueChoiceSelector ChoiceSelector => choiceSelector;

        [ReadOnly]
        public bool isChosing, onAbi, onPhoebe;
        [ReadOnly]
        public bool isAbigailSolo;
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (args is DialogueArg cast)
            {
                var db = actorDatabase.GetElement(cast.Dialogue.ActorEnum);


                portraitLeft.sprite = db.portraitBuste;

                if (LocalisationManager.language == Language.ENGLISH)
                {
                    if (cast.DialogueNode.dialogue.dialogueText.Length >= 20)
                    {
                        animatedText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                    }
                    else
                    {
                        animatedText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                    }
                }
                else
                {
                    if (cast.DialogueNode.dialogue.dialogueTextFrench.Length >= 20)
                    {
                        animatedText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                    }
                    else
                    {
                        animatedText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                    }
                }
               

                speakerName.SetText(db.actorName);

                if (LocalisationManager.language == Language.ENGLISH)
                {
                    animatedText.SetText(cast.Dialogue.dialogueText);
                }
                else
                {
                    animatedText.SetText(cast.Dialogue.dialogueTextFrench);
                }
                
            }
        }

        public void SetChoices(DialogueNode dialogueNode)
        {
            DialogueNode newDN = dialogueNode.children[0] as DialogueNode;
            var db = actorDatabase.GetElement(newDN.dialogue.ActorEnum);

            if (dialogueNode.hasMultipleChoices)
            {
                choiceSelector.Refresh(dialogueNode.children.Cast<DialogueNode>().ToArray());
                speakerName.SetText(db.actorName);
                portraitLeft.sprite = db.portraitBuste;
                return;
            }
        }
        public void EndDialog(string text)
        {
            animatedText.TextComponent.text = text;
            animatedText.StopAllCoroutines();
        }


        public void DisplaySentence(DialogueNode node)
        {

            if (LocalisationManager.language == Language.ENGLISH)
            {
                if (node.dialogue.dialogueText.Length >= 10)
                {
                    animatedText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                }
                else
                {
                    animatedText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                }
            }
            else
            {
                if (node.dialogue.dialogueTextFrench.Length >= 10)
                {
                    animatedText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                }
                else
                {
                    animatedText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                }
            }

           
            speakerName.SetText(node.dialogue.ActorEnum.ToString());

            if (LocalisationManager.language == Language.ENGLISH)
            {
                animatedText.SetText(node.dialogue.dialogueText);
            }
            else
            {
                animatedText.SetText(node.dialogue.dialogueTextFrench);
            }

            

        }



        public override void Show()
        {
            base.Show();
            MMfadeIn.PlayFeedbacks();
           
            //SetupShow();
        }

        public void SetupShow()
        {
            if (!isAbigailSolo)
            {
                ShowChoice();
            }
            else
            {
                ShowAbiSolo();
            }
        }

        public override void Hide()
        {
            base.Hide();
            MMfadeOut.PlayFeedbacks();
            //choice.SetActive(false);
            //abigail.SetActive(false);
            //phoebe.SetActive(false);
            //abigailSolo.SetActive(false);
            //isChosing = false;
            //onAbi = false;
            //onPhoebe = false;
        }

        public void ShowAbi()
        {
            choice.SetActive(false);
            abigail.SetActive(true);
            phoebe.SetActive(false);
            isChosing = false;
            onAbi = true;
            onPhoebe = false;
        }

        public void ShowAbiSolo()
        {
            choice.SetActive(false);
            abigail.SetActive(false);
            abigailSolo.SetActive(true);
            phoebe.SetActive(false);
            isChosing = false;
            onAbi = true;
            onPhoebe = false;
        }

        public void ShowPhoebe()
        {
            choice.SetActive(false);
            abigail.SetActive(false);
            phoebe.SetActive(true);
            isChosing = false;
            onAbi = false;
            onPhoebe = true;

        }

        public void ShowChoice()
        {
            choice.SetActive(true);
            abigail.SetActive(false);
            phoebe.SetActive(false);
            isChosing = true;
            onAbi = false;
            onPhoebe = false;
        }



    }

}
