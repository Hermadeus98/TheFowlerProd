using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace TheFowler
{
    public class Buff : BattleActorComponent
    {
        [SerializeField] private int waitTurn;
        [SerializeField, Range(0,100)] private float buffPercent = 25f;

        public UnityEvent OnBuffStart, OnBuffEnd;
        
        [Button]
        public void BuffActor(int turnCount)
        {
            waitTurn = turnCount;
            ReferedActor.BattleActorInfo.buffBonus = buffPercent;
            OnBuffStart?.Invoke();
        }

        [Button]
        public void EndBuff()
        {
            waitTurn = 0;
            ReferedActor.BattleActorInfo.buffBonus = 0;
            OnBuffEnd?.Invoke();
        }
        
        public override void OnTurnStart()
        {
            base.OnTurnStart();

            if (waitTurn == 0)
            {
                EndBuff();
            }
            else
            {
                waitTurn--;
            }
        }
    }
}
