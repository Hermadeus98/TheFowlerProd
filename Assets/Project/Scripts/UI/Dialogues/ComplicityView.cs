using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
namespace TheFowler
{
    public class ComplicityView : UIView
    {
        [TabGroup("References"), SerializeField] private MMFeedbacks MMComplicity;
        public override void Show()
        {
            base.Show();

            MMComplicity.PlayFeedbacks();
        }

        public override void Hide()
        {
            base.Hide();
        }
    }
}

