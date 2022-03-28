using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QRCode;
using QRCode.Extensions;
using QRCode.Utils;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public static class Fury
    {
        public static bool IsInFury { get; private set; }

        public static int FuryPoint = 0;

        public static void AddFuryPoint(int point)
        {
            FuryPoint += point;

            if (FuryPoint >= 20)
            {
                AllowJam();
            }

            var furyView = UI.GetView<FuryView>("FuryView");
            furyView.SetFuryFill(FuryPoint, 20);
            
            QRDebug.Log("FURY", FrenchPallet.TOMATO_RED, $"You have {FuryPoint} FuryPoints.");
        }
        
        public static void PlayFury()
        {
            //Pas de fury si le dernier ennemie meurt.
            if (BattleManager.CurrentBattle.Enemies.All(w => w.BattleActorInfo.isDeath))
            {
                StopFury();
                return;
            }
            
            QRDebug.Log("FURY", FrenchPallet.TOMATO_RED, "START");

            IsInFury = true;

            //FeedbackFury();

            

            Coroutiner.Play(Feedback());
        }

        private static void FeedbackFury()
        {
            /*var actor = BattleManager.CurrentBattleActor;
            if (actor is AllyActor cast)
            {
                var element = UI.GetView<AlliesDataView>(UI.Views.AlliesDataView)
                    .allyDatas[cast];
                element.Fury(true);
            }*/
            
            UI.GetView<FuryView>("FuryView").FeedbackFury(true);
        }
        
        private static IEnumerator Feedback()
        {
            yield return new WaitForSeconds(1f);
            ImagePopup.Instance.PopupFury();
            
            
            BattleManager.CurrentRound.BlockNextTurn();
            BatonPass();

            /*var actionPickingView = UI.GetView<ActionPickingView>(UI.Views.ActionPicking);
            actionPickingView.AllowFury(true);*/
            
            //BattleManager.CurrentRound.RestartTurn();
        }
        
        public static void StopFury()
        {
            QRDebug.Log("FURY", FrenchPallet.TOMATO_RED, "END");
            
            IsInFury = false;
            /*var actionPickingView = UI.GetView<ActionPickingView>(UI.Views.ActionPicking);
            actionPickingView.AllowFury(false);*/
            
            StopFeedback();
            
            UI.GetView<FuryView>("FuryView").FeedbackFury(false);
        }

        private static void StopFeedback()
        {
            var element = UI.GetView<AlliesDataView>(UI.Views.AlliesDataView).datas;
            element.ForEach(w => w.Fury(false));
        }

        public static void BatonPass()
        {
            Player.SelectedSpell = BattleManager.CurrentBattleActor.BattleActorData.BatonPass;
            var skillExecutionState = BattleManager.CurrentBattle.BattleState.GetState("SkillExecution") as BattleState_SkillExecution;
            skillExecutionState.fury = true;
            
            var skillPickingView =
                BattleManager.CurrentBattle.ChangeBattleState<BattleState_TargetPicking>(BattleStateEnum
                    .TARGET_PICKING);
            skillPickingView.ReturnToActionMenu = true;
        }

        public static void AllowJam()
        {
            /*var actor = BattleManager.CurrentBattleActor;
            if (actor is AllyActor cast)
            {
                var element = UI.GetView<AlliesDataView>(UI.Views.AlliesDataView)
                    .allyDatas[cast];
                element.Fury(true);
            }*/
            
            var actionPickingView = UI.GetView<ActionPickingView>(UI.Views.ActionPicking);
            actionPickingView.AllowFury(true);
        }

        public static void StopJam()
        {
            var actionPickingView = UI.GetView<ActionPickingView>(UI.Views.ActionPicking);
            actionPickingView.AllowFury(false);

            var alliesDataView = UI.GetView<AlliesDataView>(UI.Views.AlliesDataView);
            alliesDataView.StopFury();
        }
    }
}
