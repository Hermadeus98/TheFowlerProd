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
        public BattleNarrationElement[] Elements;

        public void RegisterEvents(Battle battle)
        {
            if(Elements.IsNullOrEmpty())
                return;

            for (int i = 0; i < Elements.Length; i++)
            {
                Elements[i].RegisterElement(battle);
            }
        }
    }

    [Serializable]
    public class BattleNarrationElement
    {
        public NarrationType NarrationType;
        public NarrationCallState NarrationCallState;

        public string debug;

        public BattleActor deathActor;
        
        public void RegisterElement(Battle battle)
        {
            switch (NarrationType)
            {
                case NarrationType.NULL:
                    break;
                case NarrationType.ON_START_BATTLE:
                    battle.BattleEvents.OnStartBattle = NarrationEvent();
                    break;
                case NarrationType.ON_END_BATTLE:
                    battle.BattleEvents.OnEndBattle = NarrationEvent();
                    break;
                case NarrationType.ON_DEATH_OF:
                    battle.BattleEvents.OnDeath = NarrationEvent();
                    break;
                case NarrationType.ON_TURN_OF:
                    break;
                case NarrationType.ON_WIN:
                    break;
                case NarrationType.ON_LOSE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public virtual IEnumerator NarrationEvent()
        {
            if (NarrationType == NarrationType.ON_DEATH_OF)
            {
                Debug.Log(BattleManager.CurrentBattle.lastDeath);
                if (deathActor == BattleManager.CurrentBattle.lastDeath)
                {
                    Debug.Log("NARRATION EVENT : " + debug);
                    CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch);
                    yield return new WaitForSeconds(2f);
                    yield break;
                }
            }
            else
            {
                Debug.Log("NARRATION EVENT : " + debug);
                CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch);
                yield return new WaitForSeconds(2f);
            }

            yield break;
        }
    }

    public enum NarrationType
    {
        NULL,
        ON_START_BATTLE, //
        ON_END_BATTLE, //
        ON_DEATH_OF, //
        ON_TURN_OF,
        ON_WIN,
        ON_LOSE,
    }

    public enum NarrationCallState
    {
        ON_START_TURN,
        ON_BEFORE_CAST,
        ON_AFTER_CAST
    }
}
