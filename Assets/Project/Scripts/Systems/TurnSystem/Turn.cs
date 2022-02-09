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
            Coroutiner.Play(ActorTransition());
        }

        public virtual void OnTurnEnd()
        {
            
        }

        IEnumerator ActorTransition()
        {
            BattleManager.CurrentBattle.ChangeBattleState<BattleState_ActionPicking>(BattleStateEnum.ACTION_PICKING);

            TurnInfoView = UI.OpenView<TurnInfoView>("TurnInfoView");
            yield return new WaitForSeconds(TurnInfoView.WaitTime);
        }
    }
}
