using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace TheFowler
{
    public class GameTimer : SerializedMonoBehaviour
    {
        public TextMeshProUGUI text;

        private void Update()
        {
            var t = Time.time;
            var ts = TimeSpan.FromSeconds(t);
            var toText = string.Format("{0}:{1:00}:{2:00}",
                (int)ts.TotalHours, ts.Minutes, ts.Seconds);
            text.SetText(toText);
        }
    }
}
