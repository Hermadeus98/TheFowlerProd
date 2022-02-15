using System.Collections;
using System.Collections.Generic;
using QRCode;
using UnityEngine;

namespace TheFowler
{
    public class BattleActorComponent : GameplayMonoBehaviour
    {
        [SerializeField] private BattleActor referedActor;

        public BattleActor ReferedActor => referedActor;

        public virtual void Initialize()
        {
            
        }

        public virtual void OnTurnStart()
        {
            
        }
    }
}
