using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class StateIcons : UIElement
    {
        public StateIcon taunt, stun; 

        public BuffIcon buff_def, buff_att, buff_CD;
        
        public void HideAll()
        {
            taunt.Hide();
            stun.Hide();
            buff_def.Hide();
            buff_att.Hide();
        }

        public void RefreshBuff_Def(BattleActor actor)
        {
            if(actor.BattleActorInfo.defenseBonus == 0)
                buff_def.gameObject.SetActive(false);
            else
            {
                buff_def.gameObject.SetActive(true);
                buff_def.Refresh(actor.BattleActorInfo.defenseBonus);
            }
        }

        public void Refresh_Att(BattleActor actor)
        {
            if(actor.BattleActorInfo.attackBonus == 0)
                buff_att.gameObject.SetActive(false);
            else
            {
                buff_att.gameObject.SetActive(true);
                buff_att.Refresh(actor.BattleActorInfo.attackBonus);
            }
        }
        public void Refresh_CD(BattleActor actor)
        {
            if (actor.BattleActorInfo.attackBonus == 0)
                buff_CD.gameObject.SetActive(false);
            else
            {
                buff_CD.gameObject.SetActive(true);
                buff_CD.Refresh(actor.BattleActorInfo.attackBonus);
            }
        }

    }
}
