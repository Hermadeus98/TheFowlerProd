using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DamageEffect : Effect
    {
        public float damage;

        public override void PreviewEffect(BattleActor emitter)
        {
            base.PreviewEffect(emitter);
        }

        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            var actor = BattleManager.CurrentBattleActor;
                        
            //actor.Mana.RemoveMana(Player.SelectedSpell.ManaCost);

            if (emitter is AllyActor)
            {
                actor.GetBattleComponent<SpellHandler>().ApplyCooldown(Player.SelectedSpell);
            }

            if (ReferedSpell.sequenceBinding != SequenceEnum.NULL)
            {
                var action = actor.SignalReceiver_CastSpell.GetReaction(actor.SignalAsset_CastSpell);
                action.AddListener(delegate
                {
                    emitter.StartCoroutine(DamageExecution(emitter,receivers));
                });

                var sequence = actor.SequenceHandler.GetSequence(ReferedSpell.sequenceBinding);
                
                if (sequence == null)
                {
                    if (emitter is EnemyActor enemyActor)
                    {
                        Debug.LogError(
                            $"SEQUENCE \"{enemyActor.AI.SelectedSpell.sequenceBinding}\" IS MISSING FOR {enemyActor.name}",
                            enemyActor.AI.SelectedSpell);
                        BattleManager.CurrentBattle.TurnSystem.NextTurn();
                        yield break;
                    }
                }
                
                sequence.Play();
                yield return new WaitForSeconds((float) sequence.duration);

                action.RemoveAllListeners();

                if (emitter is AllyActor)
                {
                    Debug.Log("EVENT : ON_LIFE (Ally Death)");
                    if (BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnLife() != null)
                    {
                        yield return BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnLife()
                            .NarrativeEvent();
                    }
                }
                yield break;
            }
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        protected virtual IEnumerator DamageExecution(BattleActor emitter, BattleActor[] receivers)
        {
            Damage(damage, emitter, receivers);
            yield break;
        }
    }
}
