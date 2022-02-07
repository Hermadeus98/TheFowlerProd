using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    [Serializable]
    public class Round
    {
        public List<ITurnActor> TurnActors;
        public ITurnActor currentTurnActor;
        
        private int currentTurnIndex;

        public bool roundIsFinish;

        public Round(IEnumerable<ITurnActor> turnActors)
        {
            TurnActors = new List<ITurnActor>(turnActors);
            currentTurnIndex = 0;
            roundIsFinish = false;
        }

        private void PlayRound()
        {
            currentTurnActor?.OnTurnEnd();
            currentTurnActor = TurnActors[currentTurnIndex];
            if (currentTurnActor.SkipTurn())
            {
                NextTurn();
            }
            else
            {
                currentTurnActor.OnTurnStart();
            }
        }

        public void NextTurn()
        {
            if (BattleManager.CurrentBattle.CheckVictory())
            {
                currentTurnActor?.OnTurnEnd();
                return;
            }

            PlayRound();

            currentTurnIndex++;

            if (currentTurnIndex == TurnActors.Count)
            {
                currentTurnActor?.OnTurnEnd();
                roundIsFinish = true;
                Debug.Log("FINISH ROUND");
                return;
            }
        }

        /// <summary>
        /// Used for Fury.
        /// </summary>
        /// <param name="turnActor"></param>
        public void OverrideTurn(ITurnActor turnActor)
        {
            currentTurnActor?.OnTurnEnd();
            turnActor.OnTurnStart();
        }
    }
}
