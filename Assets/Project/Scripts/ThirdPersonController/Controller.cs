using System;
using System.Collections;
using System.Collections.Generic;
using CMF;
using QRCode;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class Controller : GameplayMonoBehaviour
    {
        [HideInInspector] public StateMachine Controllers;
        [SerializeField] private ControllerEnum controllerOnStart;
        [SerializeField, ReadOnly] private ControllerEnum currentController;

        public Istate[] controllers;

        [HideInInspector] public ControllerArg ControllerArg = new ControllerArg();
        
        [SerializeField] protected ControllerMovement ControllerMovement;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            Controllers = new StateMachine(controllers, UpdateMode.Update, ControllerArg);
            SetController(controllerOnStart);
            SetControllerMovement(ControllerMovement);
        }

        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            GameState.onGameStateChange += OnGameStateChange;
        }

        protected override void UnregisterEvent()
        {
            base.UnregisterEvent();
            GameState.onGameStateChange -= OnGameStateChange;
        }

        [Button]
        public CharacterControllerBase SetController(ControllerEnum controllerEnum)
        {
            switch (controllerEnum)
            {
                case ControllerEnum.PLAYER_CONTROLLER:
                    Controllers.SetState("PlayerController", ControllerArg);
                    break;
                case ControllerEnum.NAV_MESH_CONTROLLER:
                    Controllers.SetState("NavMeshController", ControllerArg);
                    break;
                case ControllerEnum.NAV_MESH_FOLLOWER:
                    Controllers.SetState("NavMeshFollower", ControllerArg);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(controllerEnum), controllerEnum, null);
            }

            currentController = controllerEnum;
            var newCurrentController = Controllers.CurrentState as CharacterControllerBase;
            newCurrentController.OnChangeController();
            return newCurrentController;
        }

        public T SetController<T>(ControllerEnum controllerEnum) where T : CharacterControllerBase
        {
            return SetController(controllerEnum) as T;
        }

        [Button]
        public void SetControllerMovement(ControllerMovement controllerMovement)
        {
            this.ControllerMovement = controllerMovement;
            foreach (var state in Controllers.States.Values)
            {
                if (state is CharacterControllerBase cast)
                {
                    cast.OnSetControllerMovement(controllerMovement);
                } 
            }
        }

        private void OnGameStateChange(GameStateEnum gameStateEnum)
        {
            switch (gameStateEnum)
            {
                case GameStateEnum.LAUNCH:
                    break;
                case GameStateEnum.BATTLE:
                    SetController(ControllerEnum.NAV_MESH_CONTROLLER);
                    break;
                case GameStateEnum.CINEMATIC:
                    break;
                case GameStateEnum.EXPLORATION:
                    SetController(ControllerEnum.PLAYER_CONTROLLER);
                    break;
                case GameStateEnum.HARMONISATION:
                    break;
                case GameStateEnum.CUTSCENE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameStateEnum), gameStateEnum, null);
            }
        }
    }

    public class ControllerArg : EventArgs
    {
        
    }
    
    public enum ControllerEnum
    {
        NAV_MESH_CONTROLLER,
        PLAYER_CONTROLLER,
        NAV_MESH_FOLLOWER,
    }
}
