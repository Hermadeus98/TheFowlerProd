using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class MiniGame : GameplayPhase
    {
        [SerializeField] private BattleActor Actor;
        [SerializeField] private Transform ActorPosition;

        protected Vector3 initialPosition;
        protected Quaternion initialRotation;
        
        public override void PlayPhase()
        {
            base.PlayPhase();
        }

        public override void EndPhase()
        {
            base.EndPhase();
        }

        protected override void Update()
        {
            base.Update();

            if (Gamepad.current.aButton.wasPressedThisFrame)
            {
                EndPhase();
            }
        }
    }
}
