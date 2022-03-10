using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
namespace TheFowler
{
    public class TutorielView : UIView
    {
        [TabGroup("References"), SerializeField] private MMFeedbacks feedbackIn, feedbackOut;
        public override void Refresh(EventArgs args)
        {
            base.Refresh(args);
        }

        public override void Show()
        {
            base.Show();
            feedbackIn.PlayFeedbacks();
        }

        public  void Show(PanelTutoriel panel)
        {
            switch (panel)
            {
                case PanelTutoriel.BASICATTACK:
                    break;
            }

            Show();


        }

        public override void Hide()
        {
            base.Hide();
            feedbackOut.PlayFeedbacks();
        }
    }
}

