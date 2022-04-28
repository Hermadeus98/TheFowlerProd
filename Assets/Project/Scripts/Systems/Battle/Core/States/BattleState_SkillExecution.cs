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
                        
                        //actor.Mana.RemoveMana(Player.SelectedSpell.ManaCost);
                        actor.GetBattleComponent<SpellHandler>().ApplyCooldown(Player.SelectedSpell);

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
                            
                            BattleManager.CurrentBattle.TurnSystem.NextTurn();
                            yield break;
                        }
                        else
                        {
                            yield return Player.SelectedSpell.Cast(actor, TargetSelector.SelectedTargets.ToArray());
                        }
                    }    
                }
                else if (BattleManager.IsEnemyTurn)
                {
                    if (!BattleManager.lastTurnWasEnemiesTurn)
                    {
                        CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "EnemiesSpellStart");
                        yield return new WaitForSeconds(2f);
                        CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "EnemiesSpell");
                    }
                    else
                    {
                        CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "EnemiesSpellStart");
                        yield return new WaitForSeconds(2f);
                        CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "EnemiesSpell");
                    }
                    
                    if (BattleManager.CurrentBattleActor is EnemyActor enemyActor)
                    {
                        if (enemyActor.Brain != null)
                        {
                            

                            enemyActor.AI.StartThink();

                            BattleManager.CurrentBattle.BattleGameLogComponent.AddDatas(enemyActor.AI.SelectedSpell, enemyActor, TargetSelector.SelectedTargets);

                            var action = enemyActor.SignalReceiver_CastSpell.GetReaction(enemyActor.SignalAsset_CastSpell);
                            action.AddListener(delegate
                            {
                                enemyActor.AI.SelectedSpell.SimpleCast(enemyActor,
                                    TargetSelector.SelectedTargets.ToArray());
                            });




                            var sequence = enemyActor.SequenceHandler.GetSequence(enemyActor.AI.SelectedSpell.sequenceBinding);
                            sequence.Play();
                            yield return new WaitForSeconds((float)sequence.duration);
                            
                            action.RemoveAllListeners();

                            /*yield return enemyActor.AI.SelectedSpell.Cast(enemyActor,
                                TargetSelector.SelectedTargets.ToArray());*/

                        }
                    }

                }

                yield return new WaitForSeconds(.5f);

                if (BattleManager.IsEnemyTurn)
                {
                    //EVENT DEATH
                    if (BattleManager.CurrentBattle.lastDeath is AllyActor deadActor)
                    {
                        Debug.Log("EVENT : ON_DEATH_OF (Ally Death)");
                        Debug.Log(BattleManager.CurrentBattle.lastDeath.gameObject.name);

                        yield return deadActor.OnDeathSequence();
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
