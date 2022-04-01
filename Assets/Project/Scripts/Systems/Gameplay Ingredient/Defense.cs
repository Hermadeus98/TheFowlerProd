using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TheFowler
{
    public class Defense : BattleActorComponent
    {
        [SerializeField] private int waitTurn;

        public UnityEvent OnDefendStart, OnDefendEnd;

        [HideInInspector] public UnityEvent RestaureMana;
        
        public override void Initialize()
        {
            base.Initialize();
            waitTurn = 0;
        }

        public void DefendActor(int turnCount, float bonus)
        {
            waitTurn = turnCount;
            ReferedActor.BattleActorInfo.defenseBonus = bonus;
            OnDefendStart?.Invoke();
            
            ReferedActor.StateIcons?.buff_def.Show();
            ReferedActor.StateIcons?.RefreshBuff_Def(ReferedActor);
        }

        public void EndDefend()
        {
            waitTurn = 0;
            ReferedActor.BattleActorInfo.defenseBonus = 0;
            OnDefendEnd?.Invoke();
            
            RestaureMana?.Invoke();
            RestaureMana?.RemoveAllListeners();
            
            ReferedActor.StateIcons?.buff_def.Hide();
            ReferedActor.StateIcons?.RefreshBuff_Def(ReferedActor);
        }
        
        public override void OnTurnStart()
        {
            base.OnTurnStart();

            if (Fury.IsInFury)
                return;

            if (waitTurn == 0)
            {
                EndDefend();
            }
            else
            {
                waitTurn--;
                if(waitTurn == 0)
                    EndDefend();
            }
        }
    }
}
