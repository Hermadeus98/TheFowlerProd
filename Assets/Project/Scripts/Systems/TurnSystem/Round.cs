using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    [Serializable]
    public class Round
    {
        public List<TurnActor> TurnActors;
        public TurnActor currentTurnActor;
        
        private int currentTurnIndex;

        public Round(IEnumerable<TurnActor> turnActors)
        {
            TurnActors = new List<TurnActor>(turnActors);
            currentTurnIndex = 0;
        }

        public void PlayTurn()
        {
            currentTurnActor = TurnActors[currentTurnIndex];
            currentTurnActor.OnTurnStart();
        }

        public void NextTurn()
        {
            
        }
    }
}
