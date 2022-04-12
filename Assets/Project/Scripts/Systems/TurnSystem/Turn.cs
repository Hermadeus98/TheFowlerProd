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
            var curActor = BattleManager.CurrentBattleActor;
            
            Debug.Log("EVENT : ON_TURN_OF");
            
            BattleManager.CurrentBattle.ChangeBattleState<BattleState_ActionPicking>(BattleStateEnum.ACTION_PICKING);

            if (BattleManager.IsEnemyTurn && !BattleManager.lastTurnWasEnemiesTurn)
            {
                yield return Transition();
            }
            else if(BattleManager.IsAllyTurn)
            {
                yield return Transition();
            }

            _turnTransitionView = UI.GetView<TurnTransitionView>(UI.Views.TurnTransition);
            yield return new WaitForSeconds(_turnTransitionView.WaitTime - .2f);
            
            if(BattleManager.IsAllyTurn)
                CameraManager.Instance.SetCamera(BattleManager.CurrentBattleActor.CameraBatchBattle, CameraKeys.BattleKeys.ActionPicking);

            yield break;
        }

        private IEnumerator Transition()
        {
            _turnTransitionView = UI.GetView<TurnTransitionView>(UI.Views.TurnTransition);

            _turnTransitionView.CameraSwipTransition(delegate
            {
                var actor = BattleManager.CurrentBattleActor;
                
                CameraManager.Instance.SetCamera(actor.CameraBatchBattle, "EnterTurn");

                
                if (BattleManager.IsAllyTurn)
                {
                    if (!BattleManager.lastTurnWasAlly)
                    {
                        SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_Turn_AllyTurn, null);
                        BattleManager.lastTurnWasAlly = true;
                    }
                    else
                    {
                        SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_SwitchTurn, null);
                    }
                }
                else if (BattleManager.IsEnemyTurn)
                {
                    if (BattleManager.lastTurnWasAlly)
                    {
                        SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_Turn_EnemyTurn, null);
                        BattleManager.lastTurnWasAlly = false;
                    }
                    else
                    {
                        SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_UI_SwitchTurn, null);
                    }
                }
            });

            yield return null;
        }
    }
}
