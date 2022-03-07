using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class StateIcons : UIElement
    {
        public StateIcon buff, debuff, stun, defend;

        public void HideAll()
        {
            buff.Hide();
            debuff.Hide();
            stun.Hide();
            defend.Hide();
        }
    }
}
