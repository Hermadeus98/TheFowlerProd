using System;
using Sirenix.OdinInspector;

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
            for (int i = 0; i < instructions.Length; i++)
            {
                switch (instructions[i])
                {
                    case GameInstructionEnum.SET_PLAYER_CONTROLLER_TO_PLAYER_CONTROLLER:
                        Player.Robyn.Controller.SetController(ControllerEnum.PLAYER_CONTROLLER);
                        break;
                    case GameInstructionEnum.SET_PLAYER_CONTROLLER_TO_NAVMESH_CONTROLLER:
                        Player.Robyn.Controller.SetController(ControllerEnum.NAV_MESH_CONTROLLER);
                        break;
                    case GameInstructionEnum.SET_PLAYER_WALKING:
                        Player.Robyn.Controller.SetControllerMovement(ControllerMovement.WALK);
                        break;
                    case GameInstructionEnum.SET_PLAYER_RUNNING:
                        Player.Robyn.Controller.SetControllerMovement(ControllerMovement.RUN);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
