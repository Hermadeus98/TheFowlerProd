using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class DestructionSystem : SerializedMonoBehaviour
    {
        public static DestructionSystem Instance;
        private bool isDestruct;

        private class LevelOfDestruction
        {
            public GameObject[] ObjectsToDesactivate;
            public GameObject[] ObjectToActivate;
            public bool useCam;

            
        }

        [SerializeField] private LevelOfDestruction[] LevelOfDestructions;
        private int iteration;
        [SerializeField] private MMFeedbacks feedback, feedbackPP;

        private Cinemachine.CinemachineVirtualCameraBase destructionCam;
        private Animator anim;

        public bool isDestrucCam = false;
        private bool isDoingDestruction = false;

        public bool useAnimatic = true;

        [SerializeField] private AK.Wwise.Event destructionRobynSFX;
        [SerializeField] private AK.Wwise.Event destructionAbiSFX;
        [SerializeField] private AK.Wwise.Event destructionPhoebeSFX;

        private void Awake()
        {
            Instance = this;
        }

        [Button]
        public void ResetDestruction()
        {
            iteration = 0;
            LevelOfDestructions[0].ObjectsToDesactivate.ForEach(w => w.SetActive(false));
            LevelOfDestructions[0].ObjectToActivate.ForEach(w => w.SetActive(true));
        }

        [Button]
        public void Destruct()
        {
            Iterate();
        }

        private void Iterate()
        {
            iteration++;
            
            if(iteration >= LevelOfDestructions.Length)
                return;

            if (useAnimatic && LevelOfDestructions[iteration].useCam)
            {
                StartCoroutine(SetCam());
                return;
            }

            useAnimatic = true;
            feedback?.PlayFeedbacks();
            LevelOfDestructions[iteration].ObjectsToDesactivate.ForEach(w => w.SetActive(false));
            LevelOfDestructions[iteration].ObjectToActivate.ForEach(w => w.SetActive(true));
        }

        private void Update()
        {
            if (isDoingDestruction)
            {

                UI.GetView<SkillPickingView>(UI.Views.SkillPicking).Inputs.enabled = false;
                BattleManager.CurrentBattle.Inputs.enabled = false;

                UIBattleBatch.Instance.CanvasGroup.alpha = 0;
            }
        }

        [Button]
        public void Boom()
        {
            StartCoroutine(SetCam());
        }

        [Button]
        public IEnumerator SetCam()
        {

    
            if (!isDestruct)
            {

                isDestruct = true;

                anim = null;
                UIBattleBatch.Instance.Hide();
                UIBattleBatch.SetUIGuardsVisibility(false);

                destructionCam = BattleManager.CurrentBattle.BattleCameraBatch.CameraReferences["Destruction"].virtualCamera;

                for (int i = 0; i < BattleManager.GetAllAllies().Length; i++)
                {
                    if (!BattleManager.GetAllAllies()[i].BattleActorInfo.isDeath)

                    {
                        if (BattleManager.GetAllAllies()[i].BattleActorData.actorName == "Robyn")
                        {
                            SplitScreen.Instance.Show(BattleManager.CurrentBattle.BattleCameraBatch.CameraReferences["Destruction_Robyn"].virtualCamera, destructionCam);
                            anim = BattleManager.GetAllAllies()[i].BattleActorAnimator.Animator;

                            destructionRobynSFX.Post(gameObject);
                            BattleManager.CurrentBattle.robyn.punchline.PlayPunchline(PunchlineCallback.SKILL_EXECUTION);

                            break;
                        }
                        else if (BattleManager.GetAllAllies()[i].BattleActorData.actorName == "Abigail")
                        {
                            SplitScreen.Instance.Show(BattleManager.CurrentBattle.BattleCameraBatch.CameraReferences["Destruction_Abi"].virtualCamera, destructionCam);
                            anim = BattleManager.GetAllAllies()[i].BattleActorAnimator.Animator;

                            destructionAbiSFX.Post(gameObject);
                            BattleManager.CurrentBattle.abi.punchline.PlayPunchline(PunchlineCallback.SKILL_EXECUTION);

                            break;
                        }

                        else if (BattleManager.GetAllAllies()[i].BattleActorData.actorName == "Phoebe")
                        {
                            SplitScreen.Instance.Show(BattleManager.CurrentBattle.BattleCameraBatch.CameraReferences["Destruction_Phoebe"].virtualCamera, destructionCam);
                            anim = BattleManager.GetAllAllies()[i].BattleActorAnimator.Animator;

                            destructionPhoebeSFX.Post(gameObject);
                            BattleManager.CurrentBattle.phoebe.punchline.PlayPunchline(PunchlineCallback.SKILL_EXECUTION);

                            break;
                        }

                    }
                }


                SplitScreen.Instance.SetBigCamera(destructionCam);


                yield return new WaitForSeconds(1f);

                feedbackPP?.PlayFeedbacks();
                anim.SetTrigger("SpellExecution1");

                yield return new WaitForSeconds(.5f);

                feedback?.PlayFeedbacks();
                yield return new WaitForSeconds(.2f);
                LevelOfDestructions[1].ObjectsToDesactivate.ForEach(w => w.SetActive(false));
                LevelOfDestructions[1].ObjectToActivate.ForEach(w => w.SetActive(true));

                AkSoundEngine.SetState("Environement", "Destroyed");

                yield return new WaitForSeconds(4.5f);


                SplitScreen.Instance.Hide();
                UIBattleBatch.Instance.Show();
                UIBattleBatch.SetUIGuardsVisibility(true);


                UI.GetView<SkillPickingView>(UI.Views.SkillPicking).Inputs.enabled = true;
                BattleManager.CurrentBattle.Inputs.enabled = true;

                UIBattleBatch.Instance.CanvasGroup.alpha = 1;
            }

           

        }
    }
}