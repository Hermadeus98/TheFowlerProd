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
    public class Controller : SerializedMonoBehaviour
    {
        [HideInInspector] public StateMachine Controllers;
        [SerializeField] private ControllerEnum controllerOnStart;

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
            
            return Controllers.CurrentState as CharacterControllerBase;
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
