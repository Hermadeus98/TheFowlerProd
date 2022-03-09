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
                buff_def.Refresh(def);
            }
            else
            {
                buff_def.Hide();
            }
            
        }

        public void Refresh_Att(BattleActor actor)
        {
            var att = actor.BattleActorInfo.debuffMalus + actor.BattleActorInfo.debuffMalus;

            if (att != 0)
            {
                buff_att.Show();
                buff_att.Refresh(att);
            }
            else
            {
                buff_att.Hide();
            }
            
        }
    }
}
