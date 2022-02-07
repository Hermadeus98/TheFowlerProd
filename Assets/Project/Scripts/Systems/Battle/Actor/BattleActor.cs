using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class BattleActor : GameplayMonoBehaviour, ITurnActor
    {
        [SerializeField] private CameraBatch cameraBatchBattle;
        public CameraBatch CameraBatchBattle => cameraBatchBattle;

        public BattleActorInfo BattleActorInfo;
        
        public virtual void OnTurnStart()
        {
            Debug.Log(gameObject.name + " start turn");
        }

        public virtual void OnTurnEnd()
        {
            Debug.Log(gameObject.name + " end turn");
        }

        public virtual bool SkipTurn()
        {
            if (BattleActorInfo.isDeath)
            {
                Debug.Log(gameObject.name + " skip turn");
                return true;
            }

            return false;
        }

        public virtual bool IsAvailable()
        {
            if (BattleActorInfo.isDeath)
            {
                return false;
            }

            return true;
        }
    }

    [Serializable]
    public class BattleActorInfo
    {
        public bool isDeath;
    }
}
