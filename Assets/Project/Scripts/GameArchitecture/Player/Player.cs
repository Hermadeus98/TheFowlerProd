using System;
using System.Collections;
using System.Collections.Generic;
using QRCode.Extensions;
using UnityEngine;

namespace TheFowler
{
    public static class Player
    {
        public static Robyn Robyn { get; set; }

        public static void Initialize()
        {
            if (Robyn.IsNull())
                PlayerSpawn.current.SpawnPlayer();
            else
                PlayerSpawn.current.ReplacePlayer();
        }

        public static Robyn GetCharacters(CharacterEnum characterEnum)
        {
            switch (characterEnum)
            {
                case CharacterEnum.ROBYN:
                    return Robyn;
                    break;
                case CharacterEnum.ABIGAEL:
                    break;
                case CharacterEnum.PHEOEBE:
                    break;
                case CharacterEnum.ABI_PHEOBE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(characterEnum), characterEnum, null);
            }

            return null;
        }
    }
    
    [Flags]
    public enum CharacterEnum
    {
        ROBYN = 0,
        ABIGAEL = 1,
        PHEOEBE = 2,
        ROBYN_PHEOBE = ROBYN | PHEOEBE,
        ROBYN_ABI = ROBYN | ABIGAEL,
        ABI_PHEOBE = ABIGAEL | PHEOEBE,
        ROBYN_ABI_PHEOBE = ABIGAEL | ROBYN | PHEOEBE,
    }
}
