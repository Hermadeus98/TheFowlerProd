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
        
        public VisualEffect VisualEffectAttackShock;
        
        protected float waitAttackEffect = .85f;

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
                attackEffect.gameObject.AddComponent<BillBoard>();
                attackEffect.Play();
            }

            yield return new WaitForSeconds(waitAttackEffect);
            
            for (int i = 0; i < receivers.Length; i++)
            {
                var attackEffect = GameObject.Instantiate(VisualEffectAttackShock, receivers[i].transform.position, Quaternion.identity);
                attackEffect.Play();
            }
            
            Damage(damage, emitter, receivers);
            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override void OnSimpleCast(BattleActor emitter, BattleActor[] receivers)
        {
            emitter.StartCoroutine(OnCast(emitter, receivers));
        }
    }
}
