using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;

namespace TheFowler
{
    public class AllyActor : BattleActor
    {
        [SerializeField] private CinemachineVirtualCamera deathCam;
        
        public override void OnTurnStart()
        {
            base.OnTurnStart();
            actorTurn = new PlayerTurn();
            actorTurn.OnTurnStart();
        }

        public override void OnDeath()
        {
            base.OnDeath();
        }

        public IEnumerator OnDeathSequence()
        {
            UIBattleBatch.Instance.Hide();
            SplitScreen.Instance.Show(deathCam, BattleManager.CurrentBattleActor.CameraBatchBattle.CameraReferences["OnDeathJoking"].virtualCamera);
            SplitScreen.Instance.SetPunchLine("HAHAHA TU ES MORT SALE NOOB");
            yield return new WaitForSeconds(1f);
            
            BattleActorAnimator.Death();
            
            if (BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnDeathOf() != null)
            {
                yield return BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnDeathOf()
                    .NarrativeEvent();
            }

            yield return new WaitForSeconds(2.5f);
            
            SplitScreen.Instance.Hide();
            UIBattleBatch.Instance.Show();
        }
    }
}
