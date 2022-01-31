using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class DialogueStaticView : UIView
    {
        [SerializeField] private Image portrait;
        [SerializeField] private StringSpriteDatabase portraitDatabase;

        [SerializeField] private TextMeshProUGUI dialogueBox;
        [SerializeField] private TextMeshProUGUI speakerName;
        
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);

            if (args is DialogueArg cast)
            {
                var dialogueToDisplay = cast.Dialogue;
                switch (cast.Dialogue.ActorEnum)
                {
                    case ActorEnum.ROBYN:
                        UpdatePortrait("Robyn");
                        UpdateSpeakerName("Robyn");
                        break;
                    case ActorEnum.ABIGAEL:
                        UpdatePortrait("Abigael");
                        UpdateSpeakerName("Abigael");
                        break;
                    case ActorEnum.PHEOBE:
                        UpdatePortrait("Phoebe");
                        UpdateSpeakerName("Phoebe");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                dialogueBox.SetText(cast.Dialogue.dialogueText);
            }
        }

        private void UpdateSpeakerName(string name)
        {
            speakerName.SetText(name);
        }

        private void UpdatePortrait(string key)
        {
            portrait.sprite = portraitDatabase.GetElement(key);
        }
    }
}
