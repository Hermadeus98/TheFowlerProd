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

            StartCoroutine(FinishIE());
        }

        IEnumerator FinishIE()
        {
            UI.GetView<InfoBoxView>(UI.Views.InfoBox).Hide();
            
            UI.CloseView(UI.Views.SkillPicking);
            UI.CloseView(UI.Views.ActionPicking);
            UI.CloseView(UI.Views.TargetPicking);
            UI.CloseView("FuryView");

            if (BattleManager.CurrentBattle.BattleEvents.OnEndBattle != null)
            {
                yield return BattleManager.CurrentBattle.BattleEvents.OnEndBattle;
            }

            yield return new WaitForEndOfFrame();
            FinishBattle();
            yield return new WaitForEndOfFrame();
            BattleManager.CurrentBattle.EndPhase();
            
            TargetSelector.Quit();
            TargetSelector.ResetSelectedTargets();
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
            
            SoundManager.PlaySound(AudioGenericEnum.TF_Main_SetExplo, gameObject);
            HeartBeating.Instance.isBeating = false;
        }
    }
}
