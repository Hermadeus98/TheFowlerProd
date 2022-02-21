using DG.Tweening;
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

        private PathController bindedPathController;
        [SerializeField] private PathController.PathControllerType PathControllerType;
        [SerializeField] private float duration = 4f;
        [SerializeField] private AnimationCurve ease;
        
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
            bindedPathController = character.Controller.SetController<PathController>(ControllerEnum.PATH_CONTROLLER);
            bindedPathController.path = path;
            bindedPathController.MoveAlongWayPath(path, OnCompleteInstructions.Call);
            bindedPathController.pathControllerType = PathControllerType;
            
            DOTween.To(
                () => bindedPathController.verticalBinding,
                (x) => bindedPathController.verticalBinding = x,
                1f,
                duration
                ).SetEase(ease);
        }
    }
}
