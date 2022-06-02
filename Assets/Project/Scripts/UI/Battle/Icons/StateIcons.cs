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

        public void Reset()
        {
            taunt?.gameObject.SetActive(false);
            stun?.gameObject.SetActive(false);
            buff_def?.gameObject.SetActive(false);
            buff_att?.gameObject.SetActive(false);
            buff_CD?.gameObject.SetActive(false);
        }
        
        public void HideAll()
        {
            taunt.Hide();
            stun.Hide();
            buff_def.Hide();
            buff_att.Hide();
            buff_CD.Hide();
        }

        public void RefreshBuff_Def(BattleActor actor)
        {
            if(actor.BattleActorInfo.DefenseBonus == 0)
                buff_def.gameObject.SetActive(false);
            else
            {
                buff_def.gameObject.SetActive(true);
                buff_def.Refresh(actor.BattleActorInfo.DefenseBonus);
                buff_def.Show();
            }
        }

        public void Refresh_Att(BattleActor actor)
        {
            if(actor.BattleActorInfo.AttackBonus == 0)
                buff_att.gameObject.SetActive(false);
            else
            {
                buff_att.gameObject.SetActive(true);
                buff_att.Refresh(actor.BattleActorInfo.AttackBonus);
                buff_att.Show();
            }
        }
        public void Refresh_CD(BattleActor actor)
        {
            if (actor.BattleActorInfo.CooldownBonus == 0)
                buff_CD.gameObject.SetActive(false);
            else
            {
                buff_CD.gameObject.SetActive(true);
                buff_CD.Refresh(actor.BattleActorInfo.CooldownBonus);
                buff_CD.Show();
            }
        }

    }
}
