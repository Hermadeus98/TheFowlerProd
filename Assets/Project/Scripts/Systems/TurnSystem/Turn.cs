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

            
            _turnTransitionView = UI.GetView<TurnTransitionView>(UI.Views.TurnTransition);
            _turnTransitionView.CameraSwipTransition(delegate
            {
                var actor = BattleManager.CurrentBattleActor;
                CameraManager.Instance.SetCamera(actor.CameraBatchBattle, CameraKeys.BattleKeys.ActionPicking);

                if (BattleManager.IsAllyTurn)
                {
                    SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_Turn_AllyTurn, null);
                }
                else if (BattleManager.IsEnemyTurn)
                {
                    SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_Turn_EnemyTurn, null);
                }
            });

            //_turnTransitionView = UI.OpenView<TurnTransitionView>(UI.Views.TurnTransition);
            yield return new WaitForSeconds(_turnTransitionView.WaitTime);
            
            yield break;
        }
    }
}
