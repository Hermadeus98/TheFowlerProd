using System.Collections;
using System.Collections.Generic;
using QRCode.Utils;
using UnityEngine;

namespace TheFowler
{
    public abstract class Turn
    {
        private TurnTransitionView _turnTransitionView;

        public bool haveSaidPunchline = false;
        
        public virtual void OnTurnStart()
        {
            BattleManager.lastTouchedActors.Clear();
            
            Coroutiner.Play(ActorTransition());

            haveSaidPunchline = false;
        }

        public virtual void OnTurnEnd()
        {
            
        }

        IEnumerator ActorTransition()
        {
            BattleManager.CurrentBattle.ChangeBattleState<BattleState_ActionPicking>(BattleStateEnum.ACTION_PICKING);

            if (BattleManager.IsEnemyTurn && !BattleManager.lastTurnWasEnemiesTurn)
            {
                yield return Transition();
                _turnTransitionView = UI.GetView<TurnTransitionView>(UI.Views.TurnTransition);
                yield return new WaitForSeconds(_turnTransitionView.WaitTime - .2f);
            }
            else if(BattleManager.IsAllyTurn)
            {
                yield return Transition();
                _turnTransitionView = UI.GetView<TurnTransitionView>(UI.Views.TurnTransition);
                yield return new WaitForSeconds(_turnTransitionView.WaitTime - .2f);
                
                CameraManager.Instance.SetCamera(BattleManager.CurrentBattleActor.CameraBatchBattle,
                    CameraKeys.BattleKeys.ActionPicking);

                if (!haveSaidPunchline)
                {
                    if (BattleManager.CurrentRound.overrideTurnActor == BattleManager.CurrentBattleActor)
                    {
                        BattleManager.CurrentBattleActor.punchline.PlayPunchline(PunchlineCallback.RECEIVING_BREAKDOWN);
                    }
                    else
                    {
                        BattleManager.CurrentBattleActor.punchline.PlayPunchline(PunchlineCallback.START_TURN);
                    }

                    haveSaidPunchline = true;
                }
            }
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
