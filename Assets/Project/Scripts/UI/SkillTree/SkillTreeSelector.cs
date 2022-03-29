using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

namespace TheFowler
{
    public class SkillTreeSelector : MonoBehaviour
    {
        [SerializeField] private GameObject Picken, Unactive, Disable, Clickable;
        public Spell linkedSpell;
        public int complicityLevel = 0;
        public bool isPassive = false;
        public bool isClickable = false;

        public MMFeedbacks UnlockFeedback;
        public MMFeedbacks ClickableFeedback;
        public void SetPicken()
        {
            Picken.gameObject.SetActive(true);
            Unactive.gameObject.SetActive(false);
            Disable.gameObject.SetActive(false);

            if(isClickable)
            {
                Clickable.SetActive(true);
                if(!ClickableFeedback.IsPlaying)
                    ClickableFeedback.PlayFeedbacks();
            }
            else
            {
                Clickable.SetActive(false);
            }
        }

        public void SetUnactive()
        {
            Picken.gameObject.SetActive(false);
            Unactive.gameObject.SetActive(true);
            Disable.gameObject.SetActive(false);
            if (isClickable)
            {
                Clickable.SetActive(true);
                if (!ClickableFeedback.IsPlaying)
                    ClickableFeedback.PlayFeedbacks();
            }
            else
            {
                Clickable.SetActive(false);
            }
        }

        public void SetDisable()
        {
            Picken.gameObject.SetActive(false);
            Unactive.gameObject.SetActive(false);
            Disable.gameObject.SetActive(true);
            Clickable.SetActive(false);
        }

    }
}

