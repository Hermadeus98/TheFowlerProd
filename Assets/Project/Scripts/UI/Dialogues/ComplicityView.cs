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

        public void Show(ActorEnum actorEnum)
        {
            Show();

            switch (actorEnum)
            {
                case ActorEnum.ABIGAEL:
                    Abi.gameObject.SetActive(true);
                    break;
                case ActorEnum.PHEOBE:
                    Phoebe.gameObject.SetActive(true);
                    break;
            }

            MMComplicity.PlayFeedbacks();
        }

        public override void Hide()
        {
            base.Hide();

            Abi.gameObject.SetActive(false);
            Phoebe.gameObject.SetActive(false);
        }
    }
}

