using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheFowler
{
    public class BattleState_Fury : BattleState
    {
        public ITurnActor selectedActorForFury;

        public override void OnStateEnter(EventArgs arg)
        {
            Debug.Log("FURYYYYYYYYYYYYYYYY");
            
            CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "Allies");
            TargetSelector.Initialize(TargetTypeEnum.SOLO_ALLY);

            var transition = UI.GetView<TurnTransitionView>(UI.Views.TurnTransition);
            transition.CameraSwipTransition(null);
            
            ImagePopup.Instance.PopupFury();
        }

        public override void OnStateExecute()
        {
            base.OnStateExecute();
            if (BattleManager.IsAllyTurn)
            {
                TargetSelector.Navigate(inputs.actions["NavigateLeft"].WasPressedThisFrame(), inputs.actions["NavigateRight"].WasPressedThisFrame());
                
                if (TargetSelector.Select(inputs.actions["Select"].WasPressedThisFrame(), out var targets))
                {
                    selectedActorForFury = targets.ElementAt(0);
                    Apply();
                }
            }
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);

            TargetSelector.Quit();
            TargetSelector.ResetSelectedTargets();
        }

        private void Apply()
        {
            BattleManager.CurrentRound.OverrideTurn(selectedActorForFury);
            
            var killer = BattleManager.CurrentBattleActor;
            //BattleManager.CurrentRound.RestartTurn();

        }
    }
}
