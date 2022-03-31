using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class BattleState_SkillExecution : BattleState
    {
        [ShowInInspector] public bool fury { get; set; }

        public override void OnStateEnter(EventArgs arg)
        {
            base.OnStateEnter(arg);

            if (BattleManager.IsAllyTurn)
            {
                //SetCamera(CameraKeys.BattleKeys.SkillExecutionDefault);
            }
            else if (BattleManager.IsEnemyTurn)
            {
                SetCamera(CameraKeys.BattleKeys.SkillExecutionGuard);
            }

            StartCoroutine(Cast());

            BattleManager.CurrentBattleActor.punchline.PlayPunchline(PunchlineEnum.SKILLEXECUTION);

            UI.GetView<InfoBoxView>(UI.Views.InfoBox).Hide();
        }

        IEnumerator Cast()
        {
            if (fury)
            {
                BattleManager.CurrentBattle.ChangeBattleState<BattleState_Fury>(BattleStateEnum.FURY);
            }
            else
            {
                if (BattleManager.IsAllyTurn)
                {
                    if (Player.SelectedSpell.IsNotNull())
                    {
                        var actor = BattleManager.CurrentBattleActor;
                        actor.Mana.RemoveMana(Player.SelectedSpell.ManaCost);

                        if (Player.SelectedSpell.sequenceBinding != SequenceEnum.NULL)
                        {
                            var action = actor.SignalReceiver_CastSpell.GetReaction(actor.SignalAsset_CastSpell);
                            action.AddListener(delegate
                            {
                                Player.SelectedSpell.SimpleCast(actor, TargetSelector.SelectedTargets.ToArray());
                            });
                            
                            var sequence = actor.SequenceHandler.GetSequence(Player.SelectedSpell.sequenceBinding);
                            sequence.Play();
                            yield return new WaitForSeconds((float)sequence.duration);
                            
                            action.RemoveAllListeners();
                        }
                        else
                        {
                            yield return Player.SelectedSpell.Cast(actor, TargetSelector.SelectedTargets.ToArray());
                        }
                    }    
                }
                else if (BattleManager.IsEnemyTurn)
                {
                    if (BattleManager.CurrentBattleActor is EnemyActor enemyActor)
                    {
                        if (enemyActor.Brain != null)
                        {
                            enemyActor.AI.StartThink();

                            yield return enemyActor.AI.SelectedSpell.Cast(enemyActor,
                                TargetSelector.SelectedTargets.ToArray());
                        }
                    }
                }

                yield return new WaitForSeconds(2f);

                if (BattleManager.IsEnemyTurn)
                {
                    //EVENT DEATH
                    if (BattleManager.CurrentBattle.lastDeath is AllyActor)
                    {
                        Debug.Log("EVENT : ON_DEATH_OF (Ally Death)");
                    }
                }
                
                BattleManager.CurrentBattle.TurnSystem.NextTurn();
            }
        }

        public override void OnStateExit(EventArgs arg)
        {
            base.OnStateExit(arg);

            if (fury)
            {
                var furyState = BattleManager.CurrentBattle.BattleState.GetState("Fury") as BattleState_Fury;
                furyState.selectedActorForFury = TargetSelector.SelectedTargets[0];
            }
            else
            {
                BattleManager.CurrentRound.ResetOverrideTurn();
            }
            
            //fury = false;

            TargetSelector.ResetSelectedTargets();
        }
    }
}
