using System.Collections;
using System.Collections.Generic;
using QRCode.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class CooldownComponent : BattleActorComponent
    {
        public override void Initialize()
        {
            base.Initialize();
        }

        public void BuffCD(int value)
        {
            ReferedActor.BattleActorInfo.CooldownBonus += value;
            if (ReferedActor.BattleActorInfo.CooldownBonus > SpellData.Instance.maxBuffCD)
                ReferedActor.BattleActorInfo.CooldownBonus = SpellData.Instance.maxBuffCD;


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
            ReferedActor.BattleActorInfo.CooldownBonus = 0;

            ReferedActor.StateIcons?.Refresh_CD(ReferedActor);
        }
    }
}
