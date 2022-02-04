using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class BattleActor : GameplayMonoBehaviour
    {
        [SerializeField] private CameraBatch cameraBatchBattle;


        public CameraBatch CameraBatchBattle => cameraBatchBattle;
    }
}
