using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class StateIcons : UIElement
    {
        public StateIcon taunt, stun; 

        public BuffIcon buff_def, buff_att;
        
        public void HideAll()
        {
            taunt.Hide();
            stun.Hide();
            buff_def.Hide();
            buff_att.Hide();
        }

        public void RefreshBuff_Def(BattleActor actor)
        {
            var def = actor.BattleActorInfo.defenseBonus;
            
            if (def != 0)
            {
                buff_def.Show();
            }
            else
            {
                buff_def.Hide();
            }
            
            buff_def.Refresh(def);
        }

        public void Refresh_Att(BattleActor actor)
        {
            var att = actor.BattleActorInfo.debuffMalus + actor.BattleActorInfo.debuffMalus;

            if (att != 0)
            {
                buff_att.Show();
            }
            else
            {
                buff_att.Hide();
            }
            
            buff_def.Refresh(att);
        }
    }
}
