using System;
using Cinemachine;
using UnityEngine;

namespace TheFowler
{
    public class Robyn : Character
    {
        public CinemachineFreeLook tpsCam;

        protected override void OnEnable()
        {
            base.OnEnable();

            Player.canOpenPauseMenu = true;
        }
        protected override void OnStart()
        {
            base.OnStart();
            Player.Robyn = this;
        }
    }
}
