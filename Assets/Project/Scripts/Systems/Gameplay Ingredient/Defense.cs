using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace TheFowler
{
    public class Defense : BattleActorComponent
    {
        [Button]
        public void BuffDefense(int value)
        {
            ReferedActor.BattleActorInfo.defenseBonus += value;

            if (ReferedActor.BattleActorInfo.defenseBonus > DamageCalculator.maxBuffDefense)
                ReferedActor.BattleActorInfo.defenseBonus = DamageCalculator.maxBuffDefense;

            if (ReferedActor.BattleActorInfo.defenseBonus > 0)
            {
                ReferedActor.BattleActorAnimator.StartDefend();
            }
            
            ReferedActor.StateIcons?.RefreshBuff_Def(ReferedActor);
        }

        [Button]
        public void DebuffDefense(int value)
        {
            ReferedActor.BattleActorInfo.defenseBonus -= value;
            
            if (ReferedActor.BattleActorInfo.defenseBonus < DamageCalculator.minBuffDefense)
                ReferedActor.BattleActorInfo.defenseBonus = DamageCalculator.minBuffDefense;

            if (ReferedActor.BattleActorInfo.defenseBonus <= 0)
            {
                ReferedActor.BattleActorAnimator.EndDefend();
            }
            
            ReferedActor.StateIcons?.RefreshBuff_Def(ReferedActor);
        }

        [Button]
        public void ResetDefense()
        {
            ReferedActor.BattleActorInfo.defenseBonus = 0;
            
            ReferedActor.BattleActorAnimator.EndDefend();
            
            ReferedActor.StateIcons?.RefreshBuff_Def(ReferedActor);
        }
        
        /*[SerializeField] private int waitTurn;

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
            ReferedActor.BattleActorInfo.defenseBonus += bonus;
            OnDefendStart?.Invoke();
            
            ReferedActor.BattleActorAnimator.StartDefend();
            
            ReferedActor.StateIcons?.buff_def.Show();
            ReferedActor.StateIcons?.RefreshBuff_Def(ReferedActor);
        }

        public void DebuffDefense(int turnCount, float malus)
        {
            waitTurn = turnCount;
            ReferedActor.BattleActorInfo.defenseBonus -= malus;
            
            ReferedActor.StateIcons?.buff_def.Show();
            ReferedActor.StateIcons?.RefreshBuff_Def(ReferedActor);
        }

        public void EndDefend()
        {
            waitTurn = 0;
            ReferedActor.BattleActorInfo.defenseBonus = 0;
            OnDefendEnd?.Invoke();
            
            ReferedActor.BattleActorAnimator.EndDefend();

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
        }*/
    }
}
