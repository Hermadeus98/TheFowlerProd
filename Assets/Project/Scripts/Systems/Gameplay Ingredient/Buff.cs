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
        
        [Button]
        public void BuffAttack(int value)
        {
            ReferedActor.BattleActorInfo.attackBonus += value;

            if (ReferedActor.BattleActorInfo.attackBonus > SpellData.Instance.maxBuffAttack)
                ReferedActor.BattleActorInfo.attackBonus = SpellData.Instance.maxBuffAttack;
            
            ReferedActor.StateIcons?.Refresh_Att(ReferedActor);
            buff?.PlayFeedbacks();
            
            Apply(ReferedActor.BattleActorInfo.attackBonus, SpellData.Instance.maxBuffAttack, SpellData.Instance.minBuffAttack);
        }

        [Button]
        public void DebuffAttack(int value)
        {
            ReferedActor.BattleActorInfo.attackBonus -= value;
            
            if (ReferedActor.BattleActorInfo.defenseBonus < SpellData.Instance.minBuffAttack)
                ReferedActor.BattleActorInfo.defenseBonus = SpellData.Instance.minBuffAttack;
            
            ReferedActor.StateIcons?.Refresh_Att(ReferedActor);
            //debuff?.PlayFeedbacks();
            
            Apply(ReferedActor.BattleActorInfo.attackBonus, SpellData.Instance.maxBuffAttack, SpellData.Instance.minBuffAttack);
        }

        [Button]
        public void ResetAttack()
        {
            ReferedActor.BattleActorInfo.attackBonus = 0;
            
            ReferedActor.StateIcons?.Refresh_Att(ReferedActor);
        }
        
        private void Apply(float value, float max, float min)
        {
            if (value > 0)
            {
                ps.ForEach(w => w.Stop());
                
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
            }
        }

        public void StopVFX()
        {
            ps.ForEach(w => w.Stop());
        }
    }
}
