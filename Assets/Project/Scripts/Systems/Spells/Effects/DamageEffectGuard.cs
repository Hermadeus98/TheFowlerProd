using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class DamageEffectGuard : DamageEffect
    {
        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            foreach (var r in receivers)
            {
                //CameraManager.Instance.GetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "EnemiesSpell").LookAt = r.transform;
                
                var attack = GameObject.Instantiate(SpellData.Instance.Guard_PS_BasicAttack_Projectile);

                yield return new WaitForSeconds(SpellData.Instance.Guard_Timer_BasicAttack_ProjectileDuration);

                var impact = GameObject.Instantiate(SpellData.Instance.Guard_PS_BasicAttack_Impact, r.transform);
            }
            
            Damage(500, emitter, receivers);
            
            yield return new WaitForSeconds(SpellData.Instance.Guard_Timer_BasicAttack_ImpactDuration);
        }

        public override void OnSimpleCast(BattleActor emitter, BattleActor[] receivers)
        {
            emitter.StartCoroutine(OnCast(emitter, receivers));
        }
    }
}
