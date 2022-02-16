using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace TheFowler
{
    public class Taunt : BattleActorComponent
    {
        [SerializeField] private int waitTurn;
        
        public UnityEvent OnTauntStart, OnTauntEnd;

        public BattleActor taunter;
        
        public override void Initialize()
        {
            base.Initialize();
            waitTurn = 0;
        }
        
        [Button]
        public void TauntActor(int turnCount, BattleActor taunter)
        {
            ReferedActor.BattleActorInfo.isTaunt = true;
            this.taunter = taunter;
            waitTurn = turnCount;
            OnTauntStart?.Invoke();
        }

        [Button]
        public void EndTaunt()
        {
            ReferedActor.BattleActorInfo.isTaunt = false;
            taunter = null;
            waitTurn = 0;
            OnTauntEnd?.Invoke();
        }

        public override void OnTurnStart()
        {
            base.OnTurnStart();

            if (waitTurn == 0)
            {
                EndTaunt();
            }
            else
            {
                waitTurn--;
                if(waitTurn == 0)
                    EndTaunt();
            }
        }
    }
}
