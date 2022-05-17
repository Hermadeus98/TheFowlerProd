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
                BattleManager.CurrentBattleActor.punchline.PlayPunchline(PunchlineCallback.SKILL_EXECUTION);
                
                if (BattleManager.IsAllyTurn)
                {
                    ((AllyActor) BattleManager.CurrentBattleActor).hasPunchline = true;
                    
                    if (Player.SelectedSpell.IsNotNull())
                    {
                        var actor = BattleManager.CurrentBattleActor;
                        
                        //actor.Mana.RemoveMana(Player.SelectedSpell.ManaCost);
                        /*actor.GetBattleComponent<SpellHandler>().ApplyCooldown(Player.SelectedSpell);

                        if (Player.SelectedSpell.sequenceBinding != SequenceEnum.NULL)
                        {
                            var action = actor.SignalReceiver_CastSpell.GetReaction(actor.SignalAsset_CastSpell);
                            action.AddListener(delegate
                            {
                                StartCoroutine(Player.SelectedSpell.Cast(actor, TargetSelector.SelectedTargets.ToArray()));
                            });
                            
                            var sequence = actor.SequenceHandler.GetSequence(Player.SelectedSpell.sequenceBinding);
                            sequence.Play();
                            yield return new WaitForSeconds((float)sequence.duration);
                            
                            action.RemoveAllListeners();
                            
                            Debug.Log("EVENT : ON_LIFE (Ally Death)");
                
                            if (BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnLife() != null)
                            {
                                yield return BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnLife()
                                    .NarrativeEvent();
                            }
                            
                            BattleManager.CurrentBattle.TurnSystem.NextTurn();
                            yield break;*/
                        
                            yield return Player.SelectedSpell.Cast(actor, TargetSelector.SelectedTargets.ToArray());
                            BattleManager.CurrentBattle.TurnSystem.NextTurn();
                            yield break;
                    }
                    else
                    {
                        yield return Player.SelectedSpell.Cast(BattleManager.CurrentBattleActor, TargetSelector.SelectedTargets.ToArray());
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
                        yield return new WaitForSeconds(.2f);
                        CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "EnemiesSpell");
                    }
                    
                    if (BattleManager.CurrentBattleActor is EnemyActor enemyActor)
                    {
                        if (enemyActor.Brain != null)
                        {
                            enemyActor.AI.StartThink();

                            BattleManager.CurrentBattle.BattleGameLogComponent.AddDatas(enemyActor.AI.SelectedSpell, enemyActor, TargetSelector.SelectedTargets);
                            var spellBox = UI.GetView<EnemySpellBox>("EnemySpellBox");
                            spellBox.Show();
                            spellBox.Refresh(enemyActor.AI.SelectedSpell);

                            /*var action = enemyActor.SignalReceiver_CastSpell.GetReaction(enemyActor.SignalAsset_CastSpell);
                            action.AddListener(delegate
                            {
                                StartCoroutine(enemyActor.AI.SelectedSpell.Cast(enemyActor,
                                    TargetSelector.SelectedTargets.ToArray()));
                            });*/

                            //var sequence = enemyActor.SequenceHandler.GetSequence(enemyActor.AI.SelectedSpell.sequenceBinding);

                            /*if (sequence == null)
                            {
                                Debug.LogError($"SEQUENCE \"{enemyActor.AI.SelectedSpell.sequenceBinding }\" IS MISSING FOR {enemyActor.name}", enemyActor.AI.SelectedSpell);
                                BattleManager.CurrentBattle.TurnSystem.NextTurn();
                                yield break;
                            };*/

                            //sequence.Play();
                            //yield return new WaitForSeconds((float)sequence.duration);
                            
                            //action.RemoveAllListeners();

                            yield return enemyActor.AI.SelectedSpell.Cast(enemyActor,
                                TargetSelector.SelectedTargets.ToArray());
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
