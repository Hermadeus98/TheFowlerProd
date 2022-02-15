using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace TheFowler
{
    public class Debuff : BattleActorComponent
    {
        [SerializeField] private int waitTurn;
        [SerializeField, Range(0, 100)] private float debuffPercent = 25f;

        public UnityEvent OnDebuffStart, OnDebuffEnd;
        
        public override void Initialize()
        {
            base.Initialize();
            waitTurn = 0;
        }
        
        [Button]
        public void DebuffActor(int turnCount)
        {
            waitTurn = turnCount;
            ReferedActor.BattleActorInfo.debuffMalus = debuffPercent;
            OnDebuffStart?.Invoke();
        }

        [Button]
        public void EndDebuff()
        {
            waitTurn = 0;
            ReferedActor.BattleActorInfo.debuffMalus = 0;
            OnDebuffEnd?.Invoke();
        }

        public override void OnTurnStart()
        {
            base.OnTurnStart();

            if (waitTurn == 0)
            {
                EndDebuff();
            }
            else
            {
                waitTurn--;
            }
        }
    }
}
