using System;
using System.Linq;
using QRCode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using MoreMountains.Feedbacks;

namespace TheFowler
{
    public class HarmonisationView : UIView
    {
        [TabGroup("References")]
        [SerializeField] private GameObject choice, abigail, phoebe, abigailSolo;

        [TabGroup("References")]
        [SerializeField] private MMFeedbacks MMfadeIn, MMfadeOut;

        [SerializeField] private Image portrait, rappelInputFill;
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
                portrait.sprite = db.portraitBuste;
                speakerName.SetText(db.actorName);
                animatedText.SetText(cast.Dialogue.dialogueText);
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
