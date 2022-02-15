using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace TheFowler
{
    public class Stun : BattleActorComponent
    {
        [SerializeField] private int waitTurn;

        public UnityEvent OnStunStart, OnStunEnd;
        
        [Button]
        public void StunActor(int turnCount)
        {
            ReferedActor.BattleActorInfo.isStun = true;
            waitTurn = turnCount;
            OnStunStart?.Invoke();
        }

        [Button]
        public void EndStun()
        {
            waitTurn = 0;
            ReferedActor.BattleActorInfo.isStun = false;
            OnStunEnd?.Invoke();
        }

        public override void OnTurnStart()
        {
            base.OnTurnStart();

            if (waitTurn == 0)
            {
                EndStun();
            }
            else
            {
                waitTurn--;
            }
        }
    }
}
