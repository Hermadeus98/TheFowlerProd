using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class BattleActor : GameplayMonoBehaviour, TurnActor
    {
        [SerializeField] private CameraBatch cameraBatchBattle;
        public CameraBatch CameraBatchBattle => cameraBatchBattle;
        
        public void OnTurnStart()
        {
            Debug.Log(gameObject.name + " start turn");
        }

        public void OnTurnEnd()
        {
            Debug.Log(gameObject.name + " start turn");

        }

        public bool SkipTurn()
        {
            
            Debug.Log(gameObject.name + " skip turn");

            return false;
        }
    }
}
