using System;
using System.Collections;
using UnityEngine;

namespace TheFowler
{
    public class BattleState_StartBattle : BattleState
    {
        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);
            
            SetCamera("Default");
            
            UI.CloseView("LoseView");

            SoundManager.PlaySound(AudioGenericEnum.TF_Main_SetBattle, gameObject);

            if (BattleManager.CurrentBattle.FinishDirectly)
                StartCoroutine(Stop());

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).Hide();
        }

        IEnumerator Stop()
        {
            yield return new WaitForSeconds(2f);
            BattleManager.CurrentBattle.ChangeBattleState(BattleStateEnum.END_BATTLE);
        }
    }
}
