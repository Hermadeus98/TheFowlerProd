using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;
using MoreMountains.Feedbacks;
namespace TheFowler
{
    public class TutorielHandler : MonoBehaviour
    {
        [TabGroup("References"), SerializeField] private PlayerInput Input;
        [TabGroup("References"), SerializeField] private MMFeedbacks tutoFeedbacks;
        [TabGroup("References"), SerializeField] private Battle battle;

        private ActionPickingView ActionPickingView;
        private string inputName;

        private bool isShowed = false;

        private BattleActor actorTarget;
        private BattleActor actorTargetEndPreview;
        private BattleActor actor;

        private ActionPickingView actionView;
        private AlliesDataView alliesView;
        private void Awake()
        {
            inputName = string.Empty;


        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(.5f);
            actionView = UI.GetView<ActionPickingView>(UI.Views.ActionPicking);

            alliesView = UI.GetView<AlliesDataView>(UI.Views.AlliesDataView);
        }

        public void SetIsTutoriel(bool value)
        {
            Tutoriel.isTutoriel = value;
            
        }

        public void BlockInputs()
        {
            SetBasicAttack(false);
            SetFury(false);
            SetParry(false);
            SetSkill(false);
        }

        public void ShowInputs()
        {
            SetBasicAttack(true);
            SetFury(false);
            SetParry(true);
            SetSkill(true);
        }

        public void SetLockSkill(bool value)=> Tutoriel.LockSkill = value;

        public void SetBasicAttack(bool value) => Tutoriel.BasicAttack = value;
        public void SetFury(bool value) => Tutoriel.Fury = value;
        public void SetParry(bool value) => Tutoriel.Parry = value;
        public void SetSkill(bool value) => Tutoriel.Skill = value;


        public void ShowPanel(string panel)
        {
            isShowed = true;
            Tutoriel.isTutoriel = true;

            BlockInputs();

            var view = UI.GetView<TutorielView>(UI.Views.Tuto);
            view.Show((PanelTutoriel)System.Enum.Parse(typeof(PanelTutoriel), panel));

            //UI.GetView<ActionPickingView>(UI.Views.ActionPicking).Hide();
            //UI.GetView<AlliesDataView>(UI.Views.AlliesDataView).Hide();

            actionView?.HideTuto();
            alliesView?.Hide();

        }

        public void HidePanel()
        {
            isShowed = false;

            Tutoriel.isTutoriel = false;

            var view = UI.GetView<TutorielView>(UI.Views.Tuto);
            view.Hide();


            actionView?.Show();
            alliesView?.Show();

            ActionPickingView = UI.GetView<ActionPickingView>(UI.Views.ActionPicking);
            if (Tutoriel.BasicAttack) ActionPickingView.AllowBasicAttack(true);
            else ActionPickingView.AllowBasicAttack(false);

            if (Tutoriel.Parry) ActionPickingView.AllowParry(true);
            else ActionPickingView.AllowParry(false);

            if (Tutoriel.Skill) ActionPickingView.AllowSkill(true);
            else ActionPickingView.AllowSkill(false);

            if (Tutoriel.Fury) ActionPickingView.AllowFury(true);
            else ActionPickingView.AllowFury(false);
        }

        public void SetInputName(string value) => inputName = value;

        public void KillTuto()
        {
            Tutoriel.Kill();

            HidePanel();
            SetTargetNull();
            battle.ResetTurn();

            ActionPickingView.AllowBasicAttack(true);
            ActionPickingView.AllowParry(true);
            ActionPickingView.AllowSkill(true);

            UI.GetView<TutorielView>(UI.Views.Tuto).gameObject.SetActive(false);

            SetTargetNull();
            SetTargetPreviewNull();

            //this.enabled = false;
        }
        
        private void WaitInput()
        {
            if (string.IsNullOrEmpty(inputName)) return;

            if (Input.actions[inputName].WasPerformedThisFrame())
            {
                tutoFeedbacks.ResumeFeedbacks();
                inputName = "";
            }
        }

        public void LockTarget(BattleActor actor)
        {
            if (actor == null) { actorTarget = null; Tutoriel.LockTarget = false; return; }
            else
            {
                
                Tutoriel.LockTarget = true;
                actorTarget = actor;
                
            }

        }

        public void EndPreview(BattleActor actor)
        {
            if (actor == null) {return; }
            else
            {
                actorTargetEndPreview = actor;

            }

        }

        public void SetTargetNull() => actorTarget = null;
        public void SetTargetPreviewNull() => actorTargetEndPreview = null;

        public void SetData(BattleActorData data) => actor.BattleActorData = data;
        public void SetAI(BehaviourTree AI)
        {
            EnemyActor newActor = actor as EnemyActor;
            AIEnemy newAI = new AIEnemy(AI, newActor);
            newActor.SetAI(newAI);
            actor = newActor;

        }
        public void SetActor(BattleActor act) => actor = act;


        private void Update()
        {
            
            WaitInput();

            if (isShowed)
            {
                actionView?.HideTuto();
                alliesView?.Hide();
            }

            if(actorTarget != null)
            {
                
                actorTarget.SelectAsTarget();
                actorTarget.OnTarget();
                PreviewManager.ShowActorPreview(actorTarget);
            }

            if(actorTargetEndPreview != null)
            {
                actorTargetEndPreview.OnEndTarget();
                PreviewManager.HideActorPreview(actorTargetEndPreview);
            }

        }

        private IEnumerator WaitChangeActionView(bool value)
        {

            yield return new WaitForSeconds(.1f);

            actionView.HideTuto();

            alliesView.Hide();
        }

        private void OnDisable()
        {
            Tutoriel.Kill();
            ShowInputs();
        }


    }
    [System.Serializable]
    public enum PanelTutoriel
    {
        BASICATTACK,
        BASICATTACK2,
        SPELL,
        TYPES,
        FURY,
        TARGET,
        BUFF,
        PARRY,
        HEAL,
        DONE
    }

}

