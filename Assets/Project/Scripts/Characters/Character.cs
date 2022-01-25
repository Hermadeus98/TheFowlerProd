using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class Character : GameplayMonoBehaviour
    {
        [TabGroup("References")]
        public Controller Controller;
        [TabGroup("References")]
        public Transform pawnTransform;
    }
}
