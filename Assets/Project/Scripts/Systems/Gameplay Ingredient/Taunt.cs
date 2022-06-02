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
            taunter = null;
            ReferedActor.BattleActorInfo.isTaunt = false;
        }
        
        [Button]
        public void TauntActor(int turnCount, BattleActor taunter)
        {
            ReferedActor.BattleActorInfo.isTaunt = true;
            this.taunter = taunter;
            waitTurn = turnCount;
            OnTauntStart?.Invoke();
            ReferedActor.StateIcons?.taunt?.Show();
        }

        [Button]
        public void EndTaunt()
        {
            ReferedActor.BattleActorInfo.isTaunt = false;
            taunter = null;
            waitTurn = 0;
            OnTauntEnd?.Invoke();
            ReferedActor.StateIcons.taunt.Hide();
        }

        public override void OnTurnStart()
        {
            base.OnTurnStart();

            if (Fury.IsInFury)
                return;

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
