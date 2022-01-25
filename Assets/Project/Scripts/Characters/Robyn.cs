using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class Robyn : MonoBehaviour
    {
        public Controller Controller;
        
        public Transform pawnTransform;

        private void Start()
        {
            Player.Robyn = this;
        }
    }
}
