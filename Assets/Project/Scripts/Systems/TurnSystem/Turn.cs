using System.Collections;
using System.Collections.Generic;
using QRCode.Utils;
using UnityEngine;

namespace TheFowler
{
    public abstract class Turn
    {
        private TurnInfoView TurnInfoView;
        
        public virtual void OnTurnStart()
        {
            TurnInfoView = UI.OpenView<TurnInfoView>("TurnInfoView");
            Play();
        }

        public virtual void OnTurnEnd()
        {
            
        }
        
        public void Play()
        {
            Coroutiner.Play(wait());
        }

        IEnumerator wait()
        {
            yield return new WaitForSeconds(TurnInfoView.WaitTime);
            BattleManager.CurrentBattle.ChangeBattleState<BattleState_ActionPicking>(BattleStateEnum.ACTION_PICKING);
        }
    }
}
