using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class ActorPositionner : SerializedMonoBehaviour
    {
        [SerializeField] private ActorPosition[] actorPositions;

        [Button]
        public void UpdatePosition()
        {
            for (int i = 0; i < actorPositions.Length; i++)
            {
                var controller = SetStaticController(actorPositions[i].actor);
                controller.TeleportTo(actorPositions[i].transform);
            }
        }

        private StaticController SetStaticController(ActorEnum actorEnum)
        {
            switch (actorEnum)
            {
                case ActorEnum.ROBYN:
                    return Player.Robyn.Controller.SetController<StaticController>(ControllerEnum.STATIC_CONTROLLER);
                case ActorEnum.ABIGAEL:
                    return Player.Abigael.Controller.SetController<StaticController>(ControllerEnum.STATIC_CONTROLLER);
                case ActorEnum.PHEOBE:
                    return Player.Pheobe.Controller.SetController<StaticController>(ControllerEnum.STATIC_CONTROLLER);
                default:
                    throw new ArgumentOutOfRangeException(nameof(actorEnum), actorEnum, null);
            }
        }
    }

    [Serializable]
    public struct ActorPosition
    {
        public ActorEnum actor;
        public Transform transform;
    }
}
