using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace TheFowler
{
    public class BattleDialogView : UIView
    {
        public TextMeshProUGUI speaker;
        public TextMeshProUGUI dialogue;

        public override void Show()
        {
            CanvasGroup.DOFade(1f, .2f);
        }

        public override void Hide()
        {
            CanvasGroup.DOFade(0f, .2f);
        }

        public void Refresh(BattleDialog dialog)
        {
            speaker.SetText(dialog.speaker);
            dialogue.SetText(dialog.dialogue);
        }
    }
}
