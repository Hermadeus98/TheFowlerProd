using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using MoreMountains.Feedbacks;
using QRCode.Utils;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TheFowler;
using UnityEngine;

namespace TheFowler
{
    public class BattleNarrationComponent : SerializedMonoBehaviour
    {
        public BattleNarrationEvent[] events;

        public BattleNarrationEvent TryGetEvent_OnStartBattle()
        {
            if (!events.IsNullOrEmpty())
            {
                for (int i = 0; i < events.Length; i++)
                {
                    if (events[i].callMoment == NarrativeEventCallMoment.ON_BATTLE_START)
                        return events[i];
                }
            }

            return null;
        }

        public BattleNarrationEvent TryGetEvent_OnEndBattle()
        {
            if (!events.IsNullOrEmpty())
            {
                for (int i = 0; i < events.Length; i++)
                {
                    if (events[i].callMoment == NarrativeEventCallMoment.ON_BATTLE_END)
                        return events[i];
                }
            }

            return null;
        }

        public BattleNarrationEvent TryGetEvent_OnWin()
        {
            if (!events.IsNullOrEmpty())
            {
                for (int i = 0; i < events.Length; i++)
                {
                    if (events[i].callMoment == NarrativeEventCallMoment.ON_WIN)
                        return events[i];
                }
            }

            return null;
        }

        public BattleNarrationEvent TryGetEvent_OnLose()
        {
            if (!events.IsNullOrEmpty())
            {
                for (int i = 0; i < events.Length; i++)
                {
                    if (events[i].callMoment == NarrativeEventCallMoment.ON_LOSE)
                        return events[i];
                }
            }

            return null;
        }

        public BattleNarrationEvent TryGetEvent_OnDeathOf()
        {
            if (!events.IsNullOrEmpty())
            {
                for (int i = 0; i < events.Length; i++)
                {
                    if (events[i].callMoment == NarrativeEventCallMoment.ON_DEATH_OF)
                    {
                        if (BattleManager.CurrentBattle.lastDeath == events[i].deadActor)
                        {
                            return events[i];
                        }
                    }
                }
            }

            return null;
        }

        public BattleNarrationEvent TryGetEvent_OnDeathCount()
        {
            if (!events.IsNullOrEmpty())
            {
                for (int i = 0; i < events.Length; i++)
                {
                    if (events[i].callMoment == NarrativeEventCallMoment.ON_DEATH_COUNT)
                    {
                        if (BattleManager.CurrentBattle.EnemyDeathCount == events[i].deathCount)
                        {
                            return events[i];
                        }
                    }
                }
            }

            return null;
        }

        public BattleNarrationEvent TryGetEvent_OnLife()
        {
            if (!events.IsNullOrEmpty() && !BattleManager.lastTouchedActors.IsNullOrEmpty())
            {
                for (int i = 0; i < events.Length; i++)
                {
                    if (events[i].callMoment == NarrativeEventCallMoment.ON_LIFE)
                    {
                        if (events[i].middle)
                        {
                            for (int j = 0; j < BattleManager.lastTouchedActors.Count; j++)
                            {
                                if (BattleManager.lastTouchedActors[j] == events[i].referedActor)
                                {
                                    if (BattleManager.lastTouchedActors[j].Health.IsMidLife())
                                    {
                                        return events[i];
                                    }
                                }
                            }
                        }
                        else if (events[i].quart)
                        {
                            for (int j = 0; j < BattleManager.lastTouchedActors.Count; j++)
                            {
                                if (BattleManager.lastTouchedActors[j] == events[i].referedActor)
                                {
                                    if (BattleManager.lastTouchedActors[j].Health.IsQuartLife())
                                    {
                                        return events[i];
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }

    [Serializable]
    public class BattleNarrationEvent
    {
        public NarrativeEventCallMoment callMoment;
        [SerializeField] private string debug;
        
        [ShowIf("@this.callMoment == NarrativeEventCallMoment.ON_DEATH_OF")] public BattleActor deadActor;

        [ShowIf("@this.callMoment == NarrativeEventCallMoment.ON_DEATH_COUNT")] public int deathCount;

        [ShowIf("@this.callMoment == NarrativeEventCallMoment.ON_LIFE")]
        public BattleActor referedActor;

        [ShowIf("@this.callMoment == NarrativeEventCallMoment.ON_LIFE")]
        public bool middle, quart;
        
        public BattleDialog[] Dialogues;

        private bool isPlayed = false;
        
        public IEnumerator NarrativeEvent()
        {
            if (isPlayed)
                yield break;

            isPlayed = true;
            
            Debug.Log("NARRATION EVENT : " + debug);

            UIBattleBatch.Instance.Hide();
            var battleDialogue = UI.OpenView<BattleDialogView>(UI.Views.BattleDialog);
            
            for (int i = 0; i < Dialogues.Length; i++)
            {
                if(Dialogues[i].CameraPath != null)
                    Dialogues[i].CameraPath.m_Priority = 1000;

                

                Dialogues[i].optionalFeedback?.PlayFeedbacks();

                if (!String.IsNullOrEmpty(Dialogues[i].dialogue))
                {
                    battleDialogue.Refresh(Dialogues[i]);

                    yield return new WaitForSeconds(Dialogues[i].displayDuration);
                }

                
                if(Dialogues[i].CameraPath != null)
                    Dialogues[i].CameraPath.m_Priority = 0;

                Dialogues[i].Event.Invoke();
            }

            battleDialogue.Hide();
            
            UIBattleBatch.Instance.Show();
        }
    }

    [Serializable]
    public class BattleDialog
    {
        public string speaker;
        [TextArea(3,5)] public string dialogue;
        public CinemachineVirtualCameraBase CameraPath;
        public float displayDuration = 2f;
        public AK.Wwise.Event voices;

        public MMFeedbacks optionalFeedback;

        public UnityEngine.Events.UnityEvent Event;
    }

    public enum NarrativeEventCallMoment
    {
        NULL,
        ON_BATTLE_START,
        ON_BATTLE_END,
        ON_DEATH_OF,
        ON_DEATH_COUNT,
        ON_WIN,
        ON_LOSE,
        ON_LIFE
    }
}
