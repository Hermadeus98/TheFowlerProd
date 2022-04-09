using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class InitiativeView : UIView
    {
        public override void Show()
        {
            base.Show();
            if (VolumesManager.Instance != null)
                VolumesManager.Instance.BlurryUI.enabled = true;

        }

        public override void Hide()
        {
            base.Hide();
            if (VolumesManager.Instance != null)
                VolumesManager.Instance.BlurryUI.enabled = false;
        }
    }
}

