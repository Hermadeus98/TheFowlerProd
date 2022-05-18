using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.Feedbacks;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class DamageEffectGuard : DamageEffect
    {
        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield return base.OnCast(emitter, receivers);
        }

        protected override IEnumerator DamageExecution(BattleActor emitter, BattleActor[] receivers)
        {
            foreach (var r in receivers)
            {
                CameraManager.Instance.SetCamera(r.CameraBatchBattle, "OnBasicAttack");

                yield return new WaitForSeconds(1f);

                var guardBasicAttack = Object.Instantiate(SpellData.Instance.Guard_BasicAttackBinding,
                    emitter.transform);
                guardBasicAttack.emitter = emitter;
                guardBasicAttack.receiver = r;
                guardBasicAttack.BindData(delegate
                {
                    Damage(damage, emitter, new []{r});
                });

                yield return new WaitForSeconds(.1f);
                
                guardBasicAttack.Play();
            }

            yield return new WaitForSeconds(SpellData.Instance.Guard_Timer_BasicAttack_ImpactDuration);
        }
    }
}
