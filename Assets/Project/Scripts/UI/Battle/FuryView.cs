using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class FuryView : UIView
    {
        public Image fury;

        public Image furyFill;

        private bool isFilling;
        
        public void FeedbackFury(bool state)
        {
            fury.gameObject.SetActive(state);
        }

        public void SetFuryFill(int current, int max)
        {
            float percent = (float) current / (float) max;

            isFilling = true;

            furyFill.DOFillAmount(percent, 1f).OnComplete(() => isFilling = false);
        }

        private void Update()
        {
            if (isFilling)
            {
                AkSoundEngine.SetRTPCValue("Game_Fury", furyFill.fillAmount);
            }
        }
    }
}
