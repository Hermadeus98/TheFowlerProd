using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class AllyActor : BattleActor
    {
        [SerializeField] private CinemachineVirtualCamera deathCam, deathCamDown, resurectCam;

        [HideInInspector] public bool hasShowDeathSequence = false;

        private float turnTime = 0;
        [HideInInspector] public bool hasPunchline = false;

        [ReadOnly] public BattleActor resurector;
        
        public override void OnTurnStart()
        {
            base.OnTurnStart();

            actorTurn = new PlayerTurn();
            actorTurn.OnTurnStart();           

            if(LocalisationManager.language == Language.ENGLISH)
            {
                UI.GetView<ActionPickingView>(UI.Views.ActionPicking).actions[1].inputName.text = battleActorData.DefendSpell.SpellName;
                UI.GetView<ActionPickingView>(UI.Views.ActionPicking).actions[1].description.text = battleActorData.DefendSpell.SpellDescription;
            }
            else
            {
                UI.GetView<ActionPickingView>(UI.Views.ActionPicking).actions[1].inputName.text = battleActorData.DefendSpell.SpellNameFrench;
                UI.GetView<ActionPickingView>(UI.Views.ActionPicking).actions[1].description.text = battleActorData.DefendSpell.SpellDescriptionFrench;
            }



            turnTime = 0;
            hasPunchline = false;

        }

        protected override void Update()
        {
            if (BattleManager.CurrentBattle != null)
            {
                if (!hasPunchline && BattleManager.CurrentBattleActor == this)
                {
                    turnTime += Time.deltaTime;

                    if (turnTime > 30f)
                    {
                        punchline.PlayPunchline(PunchlineCallback.OVERTIME);
                        hasPunchline = true;
                    }
                }
            }

            base.Update();
        }

        public override void OnTurnEnd()
        {
            base.OnTurnEnd();

            turnTime = 0;
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
            
            SplitScreen.Instance.SetPunchLine(BattleManager.CurrentBattleActor.punchline.ReferedPunchlinesData.GetRandom(PunchlineCallback.KILL));
            
            yield return new WaitForSeconds(.4f);
            
            BattleActorAnimator.Death();
            punchline.PlayPunchline(PunchlineCallback.DEATH);
            
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

        [Button]
        private void TestResurection()
        {
            mustResurect = true;
            StartCoroutine(ResurectionCoroutine());
        }

        public IEnumerator ResurectionCoroutine()
        {
            if(!mustResurect && !BattleActorInfo.isDeath)
                yield break;
            
            mustResurect = false;
            
            UIBattleBatch.Instance.Hide();
            UIBattleBatch.SetUIGuardsVisibility(false);
            
            SplitScreen.Instance.Show(deathCamDown, resurector.CameraBatchBattle.CameraReferences["OnResurectStart"].virtualCamera);
            SplitScreen.Instance.SetBigCamera(resurector.CameraBatchBattle.CameraReferences["OnResurectStart"].virtualCamera);

            var data = punchline.ReferedPunchlinesData.GetRandom(PunchlineCallback.RECEIVING_REVIVE);
            SplitScreen.Instance.SetPunchLine(data);
            
            yield return new WaitForSeconds(.4f);

            Health.Resurect(40f);
            
            SplitScreen.Instance.SetLittleCamera(resurectCam);

            yield return new WaitForSeconds(2.5f);
            
            SplitScreen.Instance.Hide();
            UIBattleBatch.Instance.Show();
            UIBattleBatch.SetUIGuardsVisibility(true);
            
            yield break;
        }
    }
}
