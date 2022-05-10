using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class HealEffect : Effect
    {
        [SerializeField] private float healValue = 25;
        
        public override IEnumerator OnBeginCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }

        public override IEnumerator OnCast(BattleActor emitter, BattleActor[] receivers)
        {
            //Anim
            yield return new WaitForSeconds(.2f);
            
            foreach (var receiver in receivers)
            {
                if (receiver is AllyActor)
                {
                    CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "Allies");
                }
                else if(receiver is EnemyActor)
                {
                    CameraManager.Instance.SetCamera(BattleManager.CurrentBattle.BattleCameraBatch, "Enemies");
                }
                
                yield return new WaitForSeconds(.25f);
                
                receiver.Health.Heal(healValue);
                
                yield return new WaitForSeconds(1f);
            }
            
            yield break;
        }

        public override IEnumerator OnFinishCast(BattleActor emitter, BattleActor[] receivers)
        {
            yield break;
        }
    }
}
