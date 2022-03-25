using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace TheFowler
{
    public class DamageEffectRobyn : DamageEffect
    {
        //public float damage;
        public VisualEffect VisualEffectAttack;
        public float waitAttackEffect = 1f;
        public VisualEffect VisualEffectAttackShock;

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
            //emitter.BattleActorAnimator.AttackCast();
            //SoundManager.PlaySound(audioEvent, emitter.gameObject);
            //yield return new WaitForSeconds(emitter.BattleActorAnimator.AttackCastDuration());

            for (int i = 0; i < receivers.Length; i++)
            {
                var attackEffect = GameObject.Instantiate(VisualEffectAttack, receivers[i].transform.position, Quaternion.identity);
                attackEffect.Play();
            }

            yield return new WaitForSeconds(waitAttackEffect);
            
            for (int i = 0; i < receivers.Length; i++)
            {
                var attackEffect = GameObject.Instantiate(VisualEffectAttackShock, receivers[i].transform.position, Quaternion.identity);
                attackEffect.Play();
            }

            foreach (var receiver in receivers)
            {
                var _damage = DamageCalculator.CalculateDamage(damage, emitter, receiver, ReferedSpell.SpellType, out var resistanceFaiblesseResult);

                SoundManager.PlaySoundDamageTaken(receiver, resistanceFaiblesseResult);
                
                receiver.Health.TakeDamage(
                    _damage
                );
            }
            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override void OnSimpleCast(BattleActor emitter, BattleActor[] receivers)
        {
            //base.OnSimpleCast(emitter, receivers);
            emitter.StartCoroutine(OnCast(emitter, receivers));
        }
    }
}
