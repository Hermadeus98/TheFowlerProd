using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace TheFowler
{
    public class DamageEffectRobyn : DamageEffect
    {
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            for (int i = 0; i < receivers.Length; i++)
            {
                var attackEffect = GameObject.Instantiate(SpellData.Instance.Robyn_VisualEffect_BasicAttack_BirdFalling, receivers[i].transform.position, Quaternion.identity);
                attackEffect.gameObject.AddComponent<BillBoard>();
                attackEffect.Play();
            }

            yield return new WaitForSeconds(SpellData.Instance.Robyn_Timer_BasicAttack_BirdFallingDuration);
            
            for (int i = 0; i < receivers.Length; i++)
            {
                var attackEffect = GameObject.Instantiate(SpellData.Instance.Robyn_VisualEffect_BasicAttack_Shock, receivers[i].transform.position, Quaternion.identity);
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
