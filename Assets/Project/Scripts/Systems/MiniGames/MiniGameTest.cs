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
            
            if(!isActive)
                return;
            CheckInputs();
        }

        public void CheckInputs()
        {
            if (Inputs.actions["A"].WasPressedThisFrame())
            {
                EventBricks.CurrentBrick.Input();
            }
        }
    }
}
