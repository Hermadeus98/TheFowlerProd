using System.Collections;
using System.Collections.Generic;
using QRCode.Utils;
using UnityEngine;

namespace TheFowler
{
    public abstract class Turn
    {
        private TurnTransitionView _turnTransitionView;
        
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

            _turnTransitionView = UI.OpenView<TurnTransitionView>(UI.Views.TurnTransition);
            yield return new WaitForSeconds(_turnTransitionView.WaitTime);
        }
    }
}
