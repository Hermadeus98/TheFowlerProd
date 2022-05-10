using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace TheFowler
{
    public class Buff : BattleActorComponent
    {
        [SerializeField] private MMFeedbacks buff, debuff;
        
        [Button]
        public void BuffAttack(int value)
        {
            ReferedActor.BattleActorInfo.attackBonus += value;

            if (ReferedActor.BattleActorInfo.attackBonus > SpellData.Instance.maxBuffAttack)
                ReferedActor.BattleActorInfo.attackBonus = SpellData.Instance.maxBuffAttack;
            
            ReferedActor.StateIcons?.Refresh_Att(ReferedActor);
            buff?.PlayFeedbacks();
        }

        [Button]
        public void DebuffAttack(int value)
        {
            ReferedActor.BattleActorInfo.attackBonus -= value;
            
            if (ReferedActor.BattleActorInfo.defenseBonus < SpellData.Instance.minBuffAttack)
                ReferedActor.BattleActorInfo.defenseBonus = SpellData.Instance.minBuffAttack;
            
            ReferedActor.StateIcons?.Refresh_Att(ReferedActor);
            debuff.PlayFeedbacks();
        }

        [Button]
        public void ResetAttack()
        {
            ReferedActor.BattleActorInfo.attackBonus = 0;
            
            ReferedActor.StateIcons?.Refresh_Att(ReferedActor);

        }
        
    }
}
