using System;
using System.Collections.Generic;
using System.Linq;

namespace TheFowler
{
    [Serializable]
    public class TurnSystem
    {
        public List<ITurnActor> TurnActors = new List<ITurnActor>();

        public Round CurrentRound;
        
        public TurnSystem(IEnumerable<ITurnActor> turnActors)
        {
            TurnActors = new List<ITurnActor>(turnActors);
        }
        
        public void StartTurnSystem()
        {
            NewRound();
        }

        public void NewRound()
        {
            CurrentRound = new Round(TurnActors);
            CurrentRound.NextTurn();
        }

        public void NextTurn()
        {
            if(!CurrentRound.roundIsFinish)
                CurrentRound.NextTurn();
            else
            {
                NewRound();
            }
        }

        public void ResetTurn()
        {
            CurrentRound.OverrideTurn(BattleManager.CurrentBattleActor);
        }
    }

    public interface ITurnActor
    {
        public void OnTurnStart();
        public void OnTurnEnd();
        public bool SkipTurn();

        public bool IsAvailable();
    }
}
