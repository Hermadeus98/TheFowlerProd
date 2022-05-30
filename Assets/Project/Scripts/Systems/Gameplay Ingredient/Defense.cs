using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace TheFowler
{
    public class Defense : BattleActorComponent
    {
        [SerializeField] private MMFeedbacks buff, debuff;
        
        public ParticleSystem[] ps;

        public ParticleSystem[] ps_Debuff;
        
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
            
            Apply(ReferedActor.BattleActorInfo.defenseBonus, SpellData.Instance.maxBuffDefense, SpellData.Instance.minBuffDefense);
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
            
            Apply(ReferedActor.BattleActorInfo.defenseBonus, SpellData.Instance.maxBuffDefense, SpellData.Instance.minBuffDefense);
        }

        [Button]
        public void ResetDefense()
        {
            ReferedActor.BattleActorInfo.defenseBonus = 0;
            
            ReferedActor.BattleActorAnimator.EndDefend();
            
            ReferedActor.StateIcons?.RefreshBuff_Def(ReferedActor);
            
            StopVFX();
        }
        
        private void Apply(float value, float max, float min)
        {
            if (value > 0)
            {
                ps.ForEach(w => w.Stop());
                ps_Debuff?.ForEach(w => w.Stop()); 
                
                if (value <= max * .25f)
                    ps[0].Play();
                else if (value <= max * .5f)
                    ps[1].Play();
                else if (value <= max * .75f)
                    ps[2].Play();
            }
            else if (value < 0)
            {
                ps.ForEach(w => w.Stop());
                ps_Debuff?.ForEach(w => w.Stop());

                if (!ps_Debuff.IsNullOrEmpty())
                {
                    if (value >= min * .25f)
                        ps_Debuff[0].Play();
                    else if (value >= min * .5f)
                        ps_Debuff[1].Play();
                    else if (value >= min * .75f)
                        ps_Debuff[2].Play();
                }
            }
        }

        public void StopVFX()
        {
            ps.ForEach(w => w.Stop());
            ps_Debuff?.ForEach(w => w.Stop());
        }
    }
}
