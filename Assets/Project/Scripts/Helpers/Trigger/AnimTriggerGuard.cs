using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class AnimTriggerGuard : AnimTriggerBase
    {
        public void VFX_Slap()
        {
            GamepadVibration.Rumble(.0f, 1f, 0.2f);
        }
    }
}
