using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace TheFowler
{
    public class Buff : BattleActorComponent
    {
        [SerializeField] private MMFeedbacks buff, debuff;

        public ParticleSystem[] ps;

        public ParticleSystem[] ps_Debuff;
        
        [Button]
        public void BuffAttack(int value)
        {
            ReferedActor.BattleActorInfo.AttackBonus += value;

            if (ReferedActor.BattleActorInfo.AttackBonus > SpellData.Instance.maxBuffAttack)
                ReferedActor.BattleActorInfo.AttackBonus = SpellData.Instance.maxBuffAttack;
            
            ReferedActor.StateIcons?.Refresh_Att(ReferedActor);
            buff?.PlayFeedbacks();
            
            Apply(ReferedActor.BattleActorInfo.AttackBonus, SpellData.Instance.maxBuffAttack, SpellData.Instance.minBuffAttack);
        }

        [Button]
        public void DebuffAttack(int value)
        {
            ReferedActor.BattleActorInfo.AttackBonus -= value;
            
            if (ReferedActor.BattleActorInfo.DefenseBonus < SpellData.Instance.minBuffAttack)
                ReferedActor.BattleActorInfo.DefenseBonus = SpellData.Instance.minBuffAttack;
            
            ReferedActor.StateIcons?.Refresh_Att(ReferedActor);
            //debuff?.PlayFeedbacks();
            
            Apply(ReferedActor.BattleActorInfo.AttackBonus, SpellData.Instance.maxBuffAttack, SpellData.Instance.minBuffAttack);
        }

        [Button]
        public void ResetAttack()
        {
            ReferedActor.BattleActorInfo.AttackBonus = 0;
            ReferedActor.StateIcons?.Refresh_Att(ReferedActor);
            StopVFX();
        }
        
        private void Apply(float value, float max, float min)
        {
            if (value > 0)
            {
                ps.ForEach(w => w.Stop());
                ps_Debuff.ForEach(w => w.Stop()); 
                
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
                ps_Debuff.ForEach(w => w.Stop()); 
                
                if (value >= min * .25f)
                    ps_Debuff[0].Play();
                else if (value >= min * .5f)
                    ps_Debuff[1].Play();
                else if (value >= min * .75f)
                    ps_Debuff[2].Play();
            }
        }

        public void StopVFX()
        {
            ps.ForEach(w => w.Stop());
            ps_Debuff.ForEach(w => w.Stop());
        }
    }
}
