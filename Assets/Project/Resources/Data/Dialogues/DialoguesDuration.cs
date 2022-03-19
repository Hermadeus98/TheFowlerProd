using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TheFowler
{
    [CreateAssetMenu]
    public class DialoguesDuration : ScriptableObject
    {
        public BehaviourTree[] dialogues;
        public BehaviourTree[] battle;
        public BehaviourTree[] harmo;

        [Title("Durations")]
        public float durationDS;
        public float durationBattle;
        public float durationHarmo;
        public float durationAll;

        [Title("Porcentage")]
        public float twenty;
        public float thirty;

        private float GetDialoguesDuration()
        {
            durationDS = 0;
            for (int i = 0; i < dialogues.Length; i++)
            {
                for (int j = 0; j < dialogues[i].nodes.Count; j++)
                {
                    DialogueNode newNode = dialogues[i].nodes[j] as DialogueNode;
                    durationDS += newNode.dialogue.displayDuration;
                }
                
            }

            durationDS = durationDS / 60;
            return durationDS;
        }

        private float GetBattleDuration()
        {
            durationBattle = 0;
            for (int i = 0; i < battle.Length; i++)
            {
                for (int j = 0; j < battle[i].nodes.Count; j++)
                {
                    DialogueNode newNode = battle[i].nodes[j] as DialogueNode;
                    durationBattle += newNode.dialogue.displayDuration;
                }

            }

            durationBattle = durationBattle / 60;

            return durationBattle;
        }

        private float GetHarmoDuration()
        {
            durationHarmo = 0;
            for (int i = 0; i < harmo.Length; i++)
            {
                for (int j = 0; j < harmo[i].nodes.Count; j++)
                {
                    DialogueNode newNode = harmo[i].nodes[j] as DialogueNode;
                    durationHarmo += newNode.dialogue.displayDuration;
                }

            }

            durationHarmo = (durationHarmo / 60) *.5f;

            return durationHarmo;
        }

        [Button]
        public void GetData()
        {
            durationAll = GetDialoguesDuration() + GetBattleDuration() + GetHarmoDuration();
            twenty = durationAll * 100 / 20;
            thirty = durationAll * 100 / 30;

        }
    }
}

