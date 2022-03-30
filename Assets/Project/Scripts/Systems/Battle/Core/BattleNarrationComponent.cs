using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class BattleNarrationComponent : SerializedMonoBehaviour
    {
        [SerializeField] private Battle battle;
        
        public void RegisterEvents()
        {
            
        }
    }

    [Serializable]
    public class BattleNarrationElement
    {
        public NarrationType NarrationType;
        public NarrationCallState NarrationCallState;

        
        
        public virtual IEnumerator NarrationEvent()
        {
            yield break;
        }
    }

    public enum NarrationType
    {
        NULL,
        ON_START_BATTLE,
        ON_END_BATTLE,
        ON_DEATH_OF,
        ON_TURN_COUNT,
        ON_WIN,
        ON_LOSE
    }

    public enum NarrationCallState
    {
        ON_START_TURN,
        ON_END_TURN,
        ON_CHOOSE_SPELL,
        ON_BEFORE_CAST,
        ON_AFTER_CAST,
        ON_TARGET
    }
}
