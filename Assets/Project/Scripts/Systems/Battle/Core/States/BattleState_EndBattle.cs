using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class BattleState_EndBattle : BattleState
    {
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            FinishBattle();
            BattleManager.CurrentBattle.EndPhase();
        }

        private void FinishBattle()
        {
            Player.Robyn?.gameObject.SetActive(true);
            Player.Abigael?.gameObject.SetActive(true);
            Player.Pheobe?.gameObject.SetActive(true);
            
            BattleManager.CurrentBattle.Enemies.ForEach(w => w.gameObject.SetActive(false));
            BattleManager.CurrentBattle.Allies.ForEach(w => w.gameObject.SetActive(false));
            
            UI.CloseView(UI.Views.ActionPicking);
            UI.CloseView(UI.Views.SkillPicking);
            UI.CloseView(UI.Views.TargetPicking);
            UI.CloseView(UI.Views.AlliesDataView);
        }
    }
}
