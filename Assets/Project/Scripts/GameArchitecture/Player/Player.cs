using System;
using System.Collections;
using System.Collections.Generic;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public static class Player
    {
        public static Robyn Robyn { get; set; }
        public static Abigael Abigael { get; set; }
        public static Pheobe Pheobe { get; set; }
        
        public static Spell SelectedSpell { get; set; }

        public struct SavedData
        {
            public float health;
            public int mana;
            public SpellHandler spellHandler;
        }

        public static SavedData RobynSavedData, AbiSavedData, PhoebeSavedData;
        
        public static void Initialize()
        {
            if (GameState.gameArguments.noloadingChapter)
            {
                SpawnRobyn();
                SpawnAbigael();
                SpawnPhoebe();
            }
            else
            {
                if(GameState.gameArguments.currentChapterData.LoadRobyn)
                    SpawnRobyn();
                else
                    UnLoadCharacter(Robyn);
                
                if(GameState.gameArguments.currentChapterData.LoadAbigael)
                    SpawnAbigael();
                else
                    UnLoadCharacter(Abigael);
                
                if(GameState.gameArguments.currentChapterData.LoadPhoebe)
                    SpawnPhoebe();
                else
                    UnLoadCharacter(Pheobe);
            }
        }

        private static void SpawnRobyn()
        {
            if (Robyn.IsNull())
                PlayerSpawn.current.SpawnRobyn();
            else
                PlayerSpawn.current.ReplaceRobyn();
        }

        private static void SpawnAbigael()
        {
            if(Abigael.IsNull())
                PlayerSpawn.current.SpawnAbigael();
            else
                PlayerSpawn.current.ReplaceAbigael();
        }

        private static void SpawnPhoebe()
        {
            if(Pheobe.IsNull())
                PlayerSpawn.current.SpawnPheobe();
            else
                PlayerSpawn.current.ReplacePheobe();
        }

        private static void UnLoadCharacter(Character character)
        {
            if (!character.IsNull())
            {
                character.CharacterInfo.IsLoaded = false;
                character.gameObject.SetActive(false);
            }
        }

        public static Character GetCharacters(CharacterEnum characterEnum)
        {
            switch (characterEnum)
            {
                case CharacterEnum.ROBYN:
                    return Robyn;
                case CharacterEnum.ABIGAEL:
                    return Abigael;
                case CharacterEnum.PHEOEBE:
                    return Pheobe;
                default:
                    throw new ArgumentOutOfRangeException(nameof(characterEnum), characterEnum, null);
            }

            return null;
        }
    }
    
    public enum CharacterEnum
    {
        ROBYN = 0,
        ABIGAEL = 1,
        PHEOEBE = 2,
    }
}
