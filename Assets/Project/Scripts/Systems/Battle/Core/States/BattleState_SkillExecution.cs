using System;
using System.Collections;
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
                        yield return Player.SelectedSpell.Cast(actor, TargetSelector.SelectedTargets.ToArray());
                        BattleManager.CurrentBattle.TurnSystem.NextTurn();
                        yield break;
                    }
                }    
                
                else if (BattleManager.IsEnemyTurn)
                {
                    CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "EnemiesSpellStart");
                    yield return new WaitForSeconds(.2f);
                    
                    /*if (!BattleManager.lastTurnWasEnemiesTurn)
                    {
                        CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "EnemiesSpellStart");
                        yield return new WaitForSeconds(1f);
                        CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "EnemiesSpell");
                    }
                    else
                    {
                        CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "EnemiesSpellStart");
                        yield return new WaitForSeconds(.2f);
                        CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "EnemiesSpell");
                    }*/
                    
                    if (BattleManager.CurrentBattleActor is EnemyActor enemyActor)
                    {
                        if (enemyActor.Brain != null)
                        {
                            enemyActor.AI.StartThink();

                            BattleManager.CurrentBattle.BattleGameLogComponent.AddDatas(enemyActor.AI.SelectedSpell, enemyActor, TargetSelector.SelectedTargets);
                            var spellBox = UI.GetView<EnemySpellBox>("EnemySpellBox");
                            spellBox.Show();
                            spellBox.Refresh(enemyActor.AI.SelectedSpell);
                            
                            yield return enemyActor.AI.SelectedSpell.Cast(enemyActor,
                                TargetSelector.SelectedTargets.ToArray());
                        }
                    }
                }

                yield return new WaitForSeconds(.2f);

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
