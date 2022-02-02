using System;
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

        public bool textIsComplete => animatedText.isComplete;
        public AnimatedText AnimatedText => animatedText;
        
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (args is DialogueArg cast)
            {
                var dialogueToDisplay = cast.Dialogue;
                var db = actorDatabase.GetElement(cast.Dialogue.ActorEnum);
                portrait.sprite = db.portraitBuste;
                speakerName.SetText(db.actorName);
                animatedText.SetText(cast.Dialogue.dialogueText);
            }
        }
    }
}
