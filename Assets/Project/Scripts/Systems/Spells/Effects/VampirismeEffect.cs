using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class VampirismeEffect : Effect
    {
        public float damage;
        public bool preserveEnemies = false;

        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            var dmg = Damage(damage, emitter, receivers);
            
            yield return new WaitForSeconds(SpellData.Instance.StateEffect_WaitTime);
            
            if (emitter is AllyActor)
            {
                CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "Allies");
            }
            else if(emitter is EnemyActor)
            {
                CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "Enemies");
            }
            
            Heal(dmg, emitter, new []{emitter});

            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }
    }
}
