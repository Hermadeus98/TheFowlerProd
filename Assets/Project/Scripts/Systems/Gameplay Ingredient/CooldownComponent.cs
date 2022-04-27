using System.Collections;
using System.Collections.Generic;
using QRCode.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class CooldownComponent : BattleActorComponent
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

        public void SetMana(int mana)
        {
            currentMana = mana;
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

        public void BuffCD(int value)
        {
            ReferedActor.BattleActorInfo.cooldownBonus += value;
            if (ReferedActor.BattleActorInfo.cooldownBonus > SpellData.Instance.maxBuffCD)
                ReferedActor.BattleActorInfo.cooldownBonus = SpellData.Instance.maxBuffCD;


            for (int i = 0; i < ReferedActor.BattleActorData.Spells.Length; i++)
            {
                
                ReferedActor.BattleActorData.Spells[i].Cooldown -= value;

                if (ReferedActor.BattleActorData.Spells[i].Cooldown < 0)
                    ReferedActor.BattleActorData.Spells[i].Cooldown = 0;
            }


            ReferedActor.StateIcons.Refresh_CD(ReferedActor);
        }

        public void ResetCD()
        {
            ReferedActor.BattleActorInfo.cooldownBonus = 0;

            ReferedActor.StateIcons.Refresh_CD(ReferedActor);
        }
    }
}
