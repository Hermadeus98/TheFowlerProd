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
            yield return base.OnCast(emitter, receivers);
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        protected override IEnumerator DamageExecution(BattleActor emitter, BattleActor[] receivers)
        {
            for (int i = 0; i < receivers.Length; i++)
            {
                if (emitter == BattleManager.CurrentBattle.robyn)
                {
                    var attackEffect = GameObject.Instantiate(
                        SpellData.Instance.Robyn_VisualEffect_BasicAttack_BirdFalling, receivers[i].transform.position,
                        Quaternion.identity);
                    attackEffect.gameObject.AddComponent<BillBoard>();
                    attackEffect.Play();
                }
                else if (emitter == BattleManager.CurrentBattle.phoebe)
                {
                    var attackEffect = GameObject.Instantiate(
                        SpellData.Instance.Robyn_VisualEffect_BasicAttack_BirdFalling_Phoebe, receivers[i].transform.position,
                        Quaternion.identity);
                    attackEffect.gameObject.AddComponent<BillBoard>();
                    attackEffect.Play();
                }
            }

            yield return new WaitForSeconds(SpellData.Instance.Robyn_Timer_BasicAttack_BirdFallingDuration);
            
            for (int i = 0; i < receivers.Length; i++)
            {
                var attackEffect = GameObject.Instantiate(SpellData.Instance.Robyn_VisualEffect_BasicAttack_Shock, receivers[i].transform.position, Quaternion.identity);
                attackEffect.Play();

                var lightPosition = new Vector3(receivers[i].transform.position.x, receivers[i].transform.position.y + .25f, receivers[i].transform.position.z);
                GameObject.Instantiate(SpellData.Instance.Robyn_Flash_BasicAttack_Shock, lightPosition, Quaternion.identity);
            }
            
            Damage(damage, emitter, receivers);
            yield break;
        }
    }
}
