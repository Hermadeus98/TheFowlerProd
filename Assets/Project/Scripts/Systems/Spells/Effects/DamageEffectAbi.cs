using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DamageEffectAbi : DamageEffect
    {
        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            for (int i = 0; i < receivers.Length; i++)
            {
                var trail = GameObject.Instantiate(SpellData.Instance.Abi_PS_BasicAttack_Trail, emitter.transform.position, Quaternion.identity);
                trail.GetComponentInChildren<VFXDistanceScaler>().SetTarget(receivers[i].transform);
                trail.Play();
            }

            yield return new WaitForSeconds(SpellData.Instance.Abi_Timer_BasicAttack_TrailDuration);
            
            for (int i = 0; i < receivers.Length; i++)
            {
                var impact = GameObject.Instantiate(SpellData.Instance.Abi_PS_BasicAttack_Impact, receivers[i].transform.position + receivers[i].transform.forward, Quaternion.identity);
                impact.Play();
                
                GameObject.Instantiate(SpellData.Instance.Abi_Flash_BasicAttack_Shock, receivers[i].transform.position + receivers[i].transform.forward, Quaternion.identity);
            }

            Damage(damage, emitter, receivers);
        }
    }
}
