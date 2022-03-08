using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QRCode;
using QRCode.Extensions;
using QRCode.Utils;
using UnityEngine;

namespace TheFowler
{
    public static class Fury
    {
        public static bool IsInFury { get; private set; }
        
        public static void PlayFury()
        {
            //Pas de fury si le dernier ennemie meurt.
            if(BattleManager.CurrentBattle.Enemies.All(w => w.BattleActorInfo.isDeath))
                return;
            
            QRDebug.Log("FURY", FrenchPallet.TOMATO_RED, "START");

            IsInFury = true;

            BattleManager.CurrentRound.BlockNextTurn();

            Coroutiner.Play(Feedback());
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
        }
    }
}
