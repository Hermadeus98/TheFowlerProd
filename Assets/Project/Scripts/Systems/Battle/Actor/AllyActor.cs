using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;

namespace TheFowler
{
    public class AllyActor : BattleActor
    {
        [SerializeField] private CinemachineVirtualCamera deathCam, deathCamDown;

        [HideInInspector] public bool hasShowDeathSequence = false;
        
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
            if(hasShowDeathSequence)
                yield break;
            
            hasShowDeathSequence = true;
            
            UIBattleBatch.Instance.Hide();
            UIBattleBatch.SetUIGuardsVisibility(false);
            
            SplitScreen.Instance.Show(deathCam, BattleManager.CurrentBattleActor.CameraBatchBattle.CameraReferences["OnDeathJoking"].virtualCamera);
            SplitScreen.Instance.SetBigCamera(BattleManager.CurrentBattleActor.CameraBatchBattle.CameraReferences["OnDeathJoking"].virtualCamera);
            SplitScreen.Instance.SetPunchLine("HAHAHA TU ES MORT SALE NOOB");
            
            yield return new WaitForSeconds(.4f);
            
            BattleActorAnimator.Death();
            SplitScreen.Instance.SetLittleCamera(deathCamDown);
            
            if (BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnDeathOf() != null)
            {
                yield return BattleManager.CurrentBattle.BattleNarrationComponent.TryGetEvent_OnDeathOf()
                    .NarrativeEvent();
            }

            yield return new WaitForSeconds(2.5f);
            
            SplitScreen.Instance.Hide();
            UIBattleBatch.Instance.Show();
            UIBattleBatch.SetUIGuardsVisibility(true);
        }
    }
}
