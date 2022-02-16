using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class MiniGameTest : MiniGame
    {
        protected override void Update()
        {
            base.Update();
            CheckInputs();
        }

        public void CheckInputs()
        {
            if(!isActive)
                return;

            if (Inputs.actions["A"].WasPressedThisFrame())
            {
                EventBricks.CurrentBrick.Input();
            }
        }
    }
}
