using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class Waypoints : SerializedMonoBehaviour
    {
        [SerializeField] private Transform[] waypoints;
        public Transform[] GetWayPoints => waypoints;

        public GameInstructions OnCompleteInstructions;

        public void Follow(int characters)
        {
            var character = Player.GetCharacters((CharacterEnum)characters);
            var controller = character.Controller.SetController<NavMeshController>(ControllerEnum.NAV_MESH_CONTROLLER);
            controller.MoveAlongWayPoints(this, OnCompleteInstructions.Call);
        }
    }
}
