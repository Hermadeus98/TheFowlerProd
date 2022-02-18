using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class Waypoints : SerializedMonoBehaviour
    {
        [SerializeField] private PathCreator path;
        
        [SerializeField] private Transform[] waypoints;
        public Transform[] GetWayPoints => waypoints;

        public GameInstructions OnCompleteInstructions;

        [Button]
        public void Follow(int characters)
        {
            var character = Player.GetCharacters((CharacterEnum)characters);
            var controller = character.Controller.SetController<NavMeshController>(ControllerEnum.NAV_MESH_CONTROLLER);
            controller.MoveAlongWayPoints(this, OnCompleteInstructions.Call);
        }

        [Button]
        public void FollowPath(int characters)
        {
            var character = Player.GetCharacters((CharacterEnum)characters);
            var controller = character.Controller.SetController<PathController>(ControllerEnum.PATH_CONTROLLER);
            controller.MoveAlongWayPath(path, OnCompleteInstructions.Call);
        }
    }
}
