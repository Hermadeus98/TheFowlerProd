using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QRCode;
using QRCode.Extensions;
using QRCode.Utils;
using Sirenix.Utilities;
using UnityEngine;
using DG.Tweening;

namespace TheFowler
{
    public static class Fury
    {
        public static bool IsInFury { get; private set; }
        public static bool IsInBreakdown { get; set; }
        public static int FuryPoint = 0;
        

        public static void AddFuryPoint(int point)
        {
            SoundManager.PlaySound(AudioGenericEnum.TF_SFX_Combat_Generic_FuryGain, null);

            //FuryPoint += point;

            if (FuryPoint >= 100)
            {
                AllowFury();
            }

            var furyView = UI.GetView<FuryView>("FuryView");
            furyView.SetFuryFill(FuryPoint, 100);
            
            QRDebug.Log("FURY", FrenchPallet.TOMATO_RED, $"You have {FuryPoint} FuryPoints.");
        }

        public static void ResetFury()
        {
            FuryPoint = 0;

            var furyView = UI.GetView<FuryView>("FuryView");
            furyView.SetFuryFill(FuryPoint, 100);

        }

        private static Coroutine batonPass;
        
        /// <summary>
        /// 
        /// </summary>
        public static void PlayBreakDown()
        {
            //Pas de breakdown si le dernier ennemie meurt.
            if (BattleManager.CurrentBattle.Enemies.All(w => w.BattleActorInfo.isDeath))
            {
                StopBreakDown();
                return;
            }
            
            QRDebug.Log("FURY", FrenchPallet.TOMATO_RED, "START");
            IsInFury = true; 
            IsInBreakdown = true;
            BattleManager.IsReducingCD = true;

            if (batonPass != null)
                Coroutiner.Instance.StopCoroutine(batonPass);
            Coroutiner.Instance.StartCoroutine(LaunchBatonPass());

            UI.GetView<SkillPickingView>(UI.Views.SkillPicking).isBreakdown = true;
        }

        /// <summary>
        /// Permet de lancer le baton Pass après un leger délais (lié à la fin du spell).
        /// </summary>
        /// <returns></returns>
        private static IEnumerator LaunchBatonPass()
        {
            yield return new WaitForSeconds(1f);
            BattleManager.CurrentRound.BlockNextTurn();

            //EVENT DEATH
            Debug.Log("EVENT : ON_DEATH_OF (Enemy Death)");
            if (BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnDeathOf() != null)
            {
                yield return BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnDeathOf()
                    .NarrativeEvent();
            }
            
            if (BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnDeathCount() != null)
            {
                yield return BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnDeathCount()
                    .NarrativeEvent();
            }

            BatonPass();
            
            yield break;
        }

        public static void StopBreakDown()
        {
            QRDebug.Log("FURY", FrenchPallet.TOMATO_RED, "END");
            IsInFury = false;
            StopFeedbackBreackDown();

            //UI.GetView<SkillPickingView>(UI.Views.SkillPicking).Hide();
        }

        private static void StopFeedbackBreackDown()
        {
            var element = UI.GetView<AlliesDataView>(UI.Views.AlliesDataView).datas;
            element.ForEach(w => w.Fury(false));


        }

        /// <summary>
        /// Permet de donner le tour à un adversaire.
        /// </summary>
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

        

        public static void AllowFury()
        {
            var actionPickingView = UI.GetView<ActionPickingView>(UI.Views.ActionPicking);
            actionPickingView.AllowFury(true);
        }

        private static void StopFury()
        {
            var actionPickingView = UI.GetView<ActionPickingView>(UI.Views.ActionPicking);
            actionPickingView.AllowFury(false);

            var alliesDataView = UI.GetView<AlliesDataView>(UI.Views.AlliesDataView);
            alliesDataView.StopFury();
        }

        public static void PlayFury(Spell spell)
        {
            Coroutiner.Play(DebugFury(spell));
        }

        private static IEnumerator DebugFury(Spell spell)
        {
            QRDebug.Log("FURY", FrenchPallet.TOMATO_RED, $"MINI GAME {spell.SpellName}");

            FeedbackFury();
            yield return spell.Cast(BattleManager.CurrentBattleActor, TargetSelector.GetAllEnemies());
            StopFeedbackFury();

            yield return new WaitForSeconds(2f);
            
            BattleManager.CurrentRound.ResetOverrideTurn();
            BattleManager.CurrentBattle.NextTurn();
            StopFury();
        }
        
        /// <summary>
        /// Feedback quand le joueur est dans le mini jeu de la fury
        /// </summary>
        private static void FeedbackFury()
        {
            UI.GetView<FuryView>("FuryView").FeedbackFury(true);
        }

        private static void StopFeedbackFury()
        {
            UI.GetView<FuryView>("FuryView").FeedbackFury(false);
        }
    }
}
