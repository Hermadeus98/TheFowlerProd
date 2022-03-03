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

        public float BuffPercent
        {
            get => buffPercent;
            set
            {
                if (waitTurn > 0)
                {
                    if (value > buffPercent)
                    {
                        buffPercent = value;
                    }
                }
                else
                {
                    buffPercent = value;
                }
            }
        }
        
        public override void Initialize()
        {
            base.Initialize();
            waitTurn = 0;
        }

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
                if(waitTurn == 0)
                    EndBuff();
            }
        }
    }
}
