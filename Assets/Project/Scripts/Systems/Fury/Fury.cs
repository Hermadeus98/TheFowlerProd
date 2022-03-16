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

            FeedbackFury();

            BattleManager.CurrentRound.BlockNextTurn();

            Coroutiner.Play(Feedback());
        }

        private static void FeedbackFury()
        {
            var actor = BattleManager.CurrentBattleActor;
            if (actor is AllyActor cast)
            {
                var element = UI.GetView<AlliesDataView>(UI.Views.AlliesDataView)
                    .allyDatas[cast];
                element.Fury(true);
            }
            
            UI.OpenView("FuryView");
        }
        
        private static IEnumerator Feedback()
        {
            yield return new WaitForSeconds(1f);
            ImagePopup.Instance.PopupFury();

            var actionPickingView = UI.GetView<ActionPickingView>(UI.Views.ActionPicking);
            actionPickingView.AllowFury(true);
            
            BattleManager.CurrentRound.RestartTurn();
        }
        
        public static void StopFury()
        {
            QRDebug.Log("FURY", FrenchPallet.TOMATO_RED, "END");
            
            IsInFury = false;
            var actionPickingView = UI.GetView<ActionPickingView>(UI.Views.ActionPicking);
            actionPickingView.AllowFury(false);
            
            StopFeedback();
        }

        private static void StopFeedback()
        {
            var element = UI.GetView<AlliesDataView>(UI.Views.AlliesDataView).datas;
            element.ForEach(w => w.Fury(false));
            UI.CloseView("FuryView");
        }
    }
}
