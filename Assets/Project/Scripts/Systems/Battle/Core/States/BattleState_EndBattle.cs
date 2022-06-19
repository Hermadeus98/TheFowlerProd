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

            //End Battle Event
            Debug.Log("EVENT : ON_BATTLE_END");
            if (BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnEndBattle() != null)
            {
                yield return BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnEndBattle()
                    .NarrativeEvent();
            }

            yield return new WaitForEndOfFrame();
            StartCoroutine(FinishBattle());
            //FinishBattle();
            yield return new WaitForEndOfFrame();
            BattleManager.CurrentBattle.EndPhase();
            
            TargetSelector.Quit();
            TargetSelector.ResetSelectedTargets();
        }

        private IEnumerator FinishBattle()
        {
            //Player.Robyn?.gameObject.SetActive(true);
            //Player.Abigael?.gameObject.SetActive(true);
            //Player.Pheobe?.gameObject.SetActive(true);

            UI.CloseView(UI.Views.ActionPicking);
            UI.CloseView(UI.Views.SkillPicking);
            UI.CloseView(UI.Views.TargetPicking);
            UI.CloseView(UI.Views.AlliesDataView);

            //UI.GetView<TurnTransitionView>(UI.Views.TurnTransition).CameraSwipTransition(null);
            UI.GetView<TurnTransitionView>(UI.Views.TurnTransition).ForceHide();


            HeartBeating.Instance.isBeating = false;


            yield return new WaitForSeconds(.5f);

            BattleManager.CurrentBattle.Enemies.ForEach(w => w.gameObject.SetActive(false));
            BattleManager.CurrentBattle.Allies.ForEach(w => w.gameObject.SetActive(false));
            

            
            SoundManager.PlaySound(AudioGenericEnum.TF_Main_SetExplo, gameObject);

            

            yield break;
        }
    }
}
