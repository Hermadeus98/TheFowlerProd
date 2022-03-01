using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class MiniGameMashing : MiniGame
    {
        [SerializeField] private MashingDataProfile[] profile;
        
        private MashingView MashingView { get; set; }
        
        public override void PlayPhase()
        {
            base.PlayPhase();

            MashingView = UI.OpenView<MashingView>("MashingView");
        }

        protected override void Update()
        {
            base.Update();

            for (int i = 0; i < profile.Length; i++)
            {
                
            }
        }
    }

    [Serializable]
    public class MashingDataProfile
    {
        public Touch touch;
        public float duration;
    }
}
