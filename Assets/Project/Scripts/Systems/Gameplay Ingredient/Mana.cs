using System.Collections;
using System.Collections.Generic;
using QRCode.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class Mana : BattleActorComponent
    {
        [SerializeField] private int maxMana;
        [SerializeField] private int currentMana;

        public IntUnityEvent onRemoveMana, onWinMana;
        public int CurrentMana => currentMana;
        public int MaxMana => maxMana;

        public float NormalizedMana
        {
            get
            {
                if (maxMana == 0)
                    return 1f;
                else
                {
                    return (float)currentMana / (float)maxMana;
                }
            }
        }

        
        public void Initialize(int mana)
        {
            maxMana = currentMana = mana;
            ReferedActor.AllyData?.Refresh();
        }

        [Button]
        public bool HaveEnoughMana(int mana)
        {
            return currentMana >= mana;
        }

        [Button]
        public void AddMana(int mana)
        {
            currentMana += mana;
            if (currentMana > maxMana) currentMana = maxMana;
            onWinMana?.Invoke(currentMana);
            ReferedActor.AllyData?.Refresh();
        }

        [Button]
        public void RemoveMana(int mana)
        {
            currentMana -= mana;
            if (currentMana < 0) currentMana = 0;
            onRemoveMana?.Invoke(currentMana);
            ReferedActor.AllyData?.Refresh();
        }

        [Button]
        public void RemoveAllMana()
        {
            currentMana = 0;
            onRemoveMana?.Invoke(currentMana);
            ReferedActor.AllyData?.Refresh();
        }

        [Button]
        public void RestoreMana()
        {
            currentMana = maxMana;
            onWinMana?.Invoke(currentMana);
            ReferedActor.AllyData?.Refresh();
        }
    }
}
