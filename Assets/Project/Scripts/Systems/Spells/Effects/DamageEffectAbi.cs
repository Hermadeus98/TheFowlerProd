using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DamageEffectAbi : DamageEffect
    {
        public ParticleSystem visualTrail;
        public float trailDuration = 1f;
        public ParticleSystem visualImpact;
        
        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            for (int i = 0; i < receivers.Length; i++)
            {
                var trail = GameObject.Instantiate(visualTrail, emitter.transform.position, Quaternion.identity);
                trail.GetComponentInChildren<VFXDistanceScaler>().SetTarget(receivers[i].transform);
                trail.Play();
            }

            yield return new WaitForSeconds(trailDuration);
            
            for (int i = 0; i < receivers.Length; i++)
            {
                var impact = GameObject.Instantiate(visualImpact, receivers[i].transform.position + receivers[i].transform.forward, Quaternion.identity);
                impact.Play();
            }

            Damage(damage, emitter, receivers);
        }

        public override void OnSimpleCast(BattleActor emitter, BattleActor[] receivers)
        {
            //base.OnSimpleCast(emitter, receivers);
            emitter.StartCoroutine(OnCast(emitter, receivers));
        }
    }
}
