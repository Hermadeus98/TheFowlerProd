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
            
            CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.Allies[0].CameraBatchBattle);

            if (BattleManager.CurrentBattle.FinishDirectly)
                StartCoroutine(Stop());
        }

        IEnumerator Stop()
        {
            yield return new WaitForSeconds(2f);
            BattleManager.CurrentBattle.ChangeBattleState(BattleStateEnum.END_BATTLE);
        }
    }
}
