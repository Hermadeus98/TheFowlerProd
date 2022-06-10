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
            for (int i = 0; i < receivers.Length; i++)
            {
                CameraManager.Instance.SetCamera(receivers[i].CameraBatchBattle, "OnBasicAttack");

                yield return new WaitForSeconds(1f);

                var guardBasicAttack = Object.Instantiate(SpellData.Instance.Guard_BasicAttackBinding,
                    emitter.transform);
                guardBasicAttack.emitter = emitter;
                guardBasicAttack.receiver = receivers[i];
                guardBasicAttack.BindData(delegate
                {
                    Damage(damage, emitter, new []{receivers[i]});
                });

                yield return new WaitForSeconds(.1f);
                
                guardBasicAttack.Play();
                
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
