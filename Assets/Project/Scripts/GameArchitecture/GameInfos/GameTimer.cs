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
        
        public TextMeshProUGUI dialogueTimerText, combatTimerText;

        private float dialogueTimer, combatTimer;

        public bool incrementeDialogueTimer, incrementeCombatTimer;

        private void Update()
        {
            GlobalTimer();
            DialogueTimer();
            CombatTimer();
        }

        void DialogueTimer()
        {
            if(!incrementeDialogueTimer)
                return;
            
            dialogueTimer += Time.deltaTime;
            var ts = TimeSpan.FromSeconds(dialogueTimer);
            var toText = string.Format("Dialogs : {0}:{1:00}:{2:00}",
                (int)ts.TotalHours, ts.Minutes, ts.Seconds);
            dialogueTimerText.SetText(toText);
        }
        
        void CombatTimer()
        {
            if(!incrementeCombatTimer)
                return;
            
            combatTimer += Time.deltaTime;
            var ts = TimeSpan.FromSeconds(combatTimer);
            var toText = string.Format("Combat : {0}:{1:00}:{2:00}",
                (int)ts.TotalHours, ts.Minutes, ts.Seconds);
            combatTimerText.SetText(toText);
        }
        
        void GlobalTimer()
        {
            var t = Time.time;
            var ts = TimeSpan.FromSeconds(t);
            var toText = string.Format("Global : {0}:{1:00}:{2:00}",
                (int)ts.TotalHours, ts.Minutes, ts.Seconds);
            text.SetText(toText);
        }
    }
}
