using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    /// <summary>
    /// Class with useful events.
    /// </summary>
    [Serializable]
    public class GameInstructions
    {
        [EnumPaging] public GameInstructionEnum[] instructions;

        public void Call()
        {
            if(instructions.IsNullOrEmpty())
                return;
            
            for (int i = 0; i < instructions.Length; i++)
            {
                switch (instructions[i])
                {
                    case GameInstructionEnum.SET_PLAYER_CONTROLLER_TO_PLAYER_CONTROLLER:
                        var controller = Player.Robyn?.Controller.SetController<ThirdPersonController>(ControllerEnum.PLAYER_CONTROLLER);
                        controller?.SetCameraToTPSCamera();
                        break;
                    
                    case GameInstructionEnum.SET_PLAYER_CONTROLLER_TO_NAVMESH_CONTROLLER:
                        Player.Robyn?.Controller.SetController(ControllerEnum.NAV_MESH_CONTROLLER);
                        break;
                    
                    case GameInstructionEnum.SET_PLAYER_WALKING:
                        Player.Robyn?.Controller.SetControllerMovement(ControllerMovement.WALK);
                        break;
                    
                    case GameInstructionEnum.SET_PLAYER_RUNNING:
                        Player.Robyn?.Controller.SetControllerMovement(ControllerMovement.RUN);
                        break;

                    case GameInstructionEnum.SET_ALLIES_CONTROLLER_TO_NAVMESH_CONTROLLER:
                        Player.Abigael?.Controller.SetController(ControllerEnum.NAV_MESH_CONTROLLER);
                        if (Player.Pheobe != null)
                            Player.Pheobe?.Controller.SetController(ControllerEnum.NAV_MESH_CONTROLLER);
                        break;
                    case GameInstructionEnum.SET_ALLIES_CONTROLLER_TO_NAVMESH_FOLLOWER:
                        Player.Abigael?.Controller.SetController(ControllerEnum.NAV_MESH_FOLLOWER);
                        if (Player.Pheobe != null)
                            Player.Pheobe?.Controller.SetController(ControllerEnum.NAV_MESH_FOLLOWER);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
