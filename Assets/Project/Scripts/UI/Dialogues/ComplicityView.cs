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
        [TabGroup("References"), SerializeField] private GameObject Abi, Phoebe;
        public override void Show()
        {
            base.Show();

            MMComplicity.PlayFeedbacks();
        }

        public void Show(ComplicityActor actor)
        {
            Show();

            switch (actor)
            {
                case ComplicityActor.ABI:
                    Abi.SetActive(true);
                    break;
                case ComplicityActor.PHOEBE:
                    Phoebe.SetActive(true);
                    break;
            }
        }

        public override void Hide()
        {
            base.Hide();
            Abi.SetActive(false);
            Phoebe.SetActive(false);

        }
    }

    public enum ComplicityActor
    {
        ABI,
        PHOEBE
    }
}

