using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    [Serializable]
    public class Round
    {
        public List<ITurnActor> TurnActors;
        public ITurnActor currentTurnActor;
        public ITurnActor overrideTurnActor;
        
        private int currentTurnIndex;

        public bool roundIsFinish;

        private bool blockNextTurn = false;

        public Round(IEnumerable<ITurnActor> turnActors)
        {
            TurnActors = new List<ITurnActor>(turnActors);
            currentTurnIndex = 0;
            roundIsFinish = false;
        }

        private void PlayRound()
        {
            //Debug.Log(currentTurnIndex);
            
            currentTurnActor?.OnTurnEnd();

            currentTurnActor = TurnActors[currentTurnIndex];

            if (currentTurnActor.SkipTurn())
            {
                currentTurnIndex++;
                NextTurn();
            }
            else
            {
                currentTurnActor.OnTurnStart();
                BattleManager.OnTurnChanged?.Invoke();
                currentTurnIndex++;
            }
        }

        public void NextTurn()
        {
            if (BattleManager.CurrentBattle.CheckVictory())
            {
                currentTurnActor?.OnTurnEnd();
                return;
            }
            
            if (blockNextTurn)
            {
                blockNextTurn = false;
                return;
            }
            
            if (currentTurnIndex == TurnActors.Count)
            {
                currentTurnActor?.OnTurnEnd();
                roundIsFinish = true;
                BattleManager.CurrentBattle.TurnSystem.NextTurn();
                return;
            }
            
            PlayRound();

            //currentTurnIndex++;
        }

        /// <summary>
        /// Used for Fury.
        /// </summary>
        /// <param name="turnActor"></param>
        public void OverrideTurn(ITurnActor turnActor)
        {
            currentTurnActor?.OnTurnEnd();
            overrideTurnActor = turnActor;
            turnActor.OnTurnStart();
            BattleManager.OnTurnChanged?.Invoke();
        }

        public void ResetOverrideTurn()
        {
            Debug.Log("RESET OVERRIDE TURN");
            overrideTurnActor?.OnTurnEnd();
            overrideTurnActor = null;
        }

        public void RestartTurn(ITurnActor turnActor = null)
        {
            Debug.Log("RESTART TURN");
            if (turnActor == null)
                OverrideTurn(currentTurnActor);
            else
                OverrideTurn(turnActor);

            //BlockNextTurn();
        }

        public void BlockNextTurn()
        {
            blockNextTurn = true;
            Debug.Log("BlockNextTurn");
        }

        public ITurnActor GetNextAlly()
        {
            if (TurnActors.IsNullOrEmpty())
                return null;
            
            if (currentTurnIndex + 1 < TurnActors.Count)
            {
                if (TurnActors[currentTurnIndex + 1] is AllyActor ally)
                {
                    if (!ally.BattleActorInfo.isDeath)
                    {
                        return ally;
                    }
                }
                
                if (currentTurnIndex + 2 < TurnActors.Count)
                {
                    if(TurnActors[currentTurnIndex + 2] is AllyActor nextAlly)
                    {
                        if (!nextAlly.BattleActorInfo.isDeath)
                        {
                            return nextAlly;
                        }
                    }
                }
                
                return TurnActors[currentTurnIndex + 1];
            }

            return null;
        }
    }
}
