using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace TheFowler
{
    public class Defense : BattleActorComponent
    {
        [SerializeField] private MMFeedbacks buff, debuff;
        
        [Button]
        public void BuffDefense(int value)
        {
            ReferedActor.BattleActorInfo.defenseBonus += value;

            if (ReferedActor.BattleActorInfo.defenseBonus > SpellData.Instance.maxBuffDefense)
                ReferedActor.BattleActorInfo.defenseBonus = SpellData.Instance.maxBuffDefense;

            if (ReferedActor.BattleActorInfo.defenseBonus > 0)
            {
                ReferedActor.BattleActorAnimator.StartDefend();
            }
            
            ReferedActor.StateIcons?.RefreshBuff_Def(ReferedActor);
            buff?.PlayFeedbacks();
        }

        [Button]
        public void DebuffDefense(int value)
        {
            
            
            ReferedActor.BattleActorInfo.defenseBonus -= value;
            
            if (ReferedActor.BattleActorInfo.defenseBonus < SpellData.Instance.minBuffDefense)
                ReferedActor.BattleActorInfo.defenseBonus = SpellData.Instance.minBuffDefense;

            if (ReferedActor.BattleActorInfo.defenseBonus <= 0)
            {
                ReferedActor.BattleActorAnimator.EndDefend();
            }
            
            ReferedActor.StateIcons?.RefreshBuff_Def(ReferedActor);
            debuff?.PlayFeedbacks();
        }

        [Button]
        public void ResetDefense()
        {
            ReferedActor.BattleActorInfo.defenseBonus = 0;
            
            ReferedActor.BattleActorAnimator.EndDefend();
            
            ReferedActor.StateIcons?.RefreshBuff_Def(ReferedActor);
        }
    }
}
