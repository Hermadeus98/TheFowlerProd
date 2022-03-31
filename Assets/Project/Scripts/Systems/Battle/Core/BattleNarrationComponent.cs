using System;
using System.Collections;
using System.Collections.Generic;
using QRCode.Utils;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
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
    }

    [Serializable]
    public class BattleNarrationEvent
    {
        public NarrativeEventCallMoment callMoment;
        [SerializeField] private string debug;
        
        public BattleActor deadActor;

        public int deathCount;

        
        public IEnumerator NarrativeEvent()
        {
            Debug.Log("NARRATION EVENT : " + debug);
            CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch);
            yield return new WaitForSeconds(2f);
        }
    }

    public enum NarrativeEventCallMoment
    {
        NULL,
        ON_BATTLE_START,
        ON_BATTLE_END,
        ON_DEATH_OF,
        ON_DEATH_COUNT,
        ON_WIN,
        ON_LOSE
    }
}
