using System.Collections;
using System.Collections.Generic;
using QRCode.Extensions;
using UnityEngine;

namespace TheFowler
{
    public static class Player
    {
        private static Robyn robyn;
        
        public static Robyn Robyn
        {
            get => robyn;
        }
        
        public static void Initialize()
        {
            if (robyn.IsNull())
                robyn = PlayerSpawn.current.SpawnPlayer();
            else
                PlayerSpawn.current.ReplacePlayer();
        }
    }
}
