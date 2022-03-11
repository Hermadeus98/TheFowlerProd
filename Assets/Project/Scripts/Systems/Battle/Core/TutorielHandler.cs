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
        private BattleActor actor;

        private void Awake()
        {
            inputName = string.Empty;
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

            UI.GetView<ActionPickingView>(UI.Views.ActionPicking).Hide();
            UI.GetView<AlliesDataView>(UI.Views.AlliesDataView).Hide();
        }

        public void HidePanel()
        {
            isShowed = false;

            Tutoriel.isTutoriel = false;

            var view = UI.GetView<TutorielView>(UI.Views.Tuto);
            view.Hide();

            UI.GetView<AlliesDataView>(UI.Views.AlliesDataView).Show();
            UI.GetView<ActionPickingView>(UI.Views.ActionPicking).Show();

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

        public void SetTargetNull() => actorTarget = null;

        public void SetData(BattleActorData data) => actor.BattleActorData = data;
        public void SetActor(BattleActor act) => actor = act;

        private void Update()
        {
            
            WaitInput();

            if (isShowed)
            {
                UI.GetView<ActionPickingView>(UI.Views.ActionPicking).Hide();
                UI.GetView<AlliesDataView>(UI.Views.AlliesDataView).Hide();
            }

            if(actorTarget != null)
            {
                actorTarget.SelectAsTarget();
                actorTarget.OnTarget();
            }

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

