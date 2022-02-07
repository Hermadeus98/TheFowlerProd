using System;
using System.Collections.Generic;

namespace TheFowler
{
    [Serializable]
    public class TurnSystem
    {
        public List<TurnActor> TurnActors = new List<TurnActor>();

        public Round CurrentRound;
        
        public TurnSystem(TurnActor[] turnActors)
        {
            TurnActors = new List<TurnActor>(turnActors);
        }
        
        public void StartTurnSystem()
        {
            CurrentRound = new Round(TurnActors);
        }
    }

    public interface TurnActor
    {
        public void OnTurnStart();
        public void OnTurnEnd();
        public bool SkipTurn();
    }
}
