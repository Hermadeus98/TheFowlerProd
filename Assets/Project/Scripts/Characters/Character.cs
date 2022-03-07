using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
namespace TheFowler
{
    public class Character : GameplayMonoBehaviour
    {
        [TabGroup("References")]
        public Controller Controller;
        [TabGroup("References")]
        public Transform pawnTransform;
        [TabGroup("References")]
        public HarmonisationComponent harmonisationComponent;

        public CharacterInfo CharacterInfo = new CharacterInfo();

        protected override void OnStart()
        {
            base.OnStart();
            
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            CharacterInfo.IsLoaded = true;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            CharacterInfo.IsLoaded = false;
        }



    }

    [Serializable]
    public class CharacterInfo
    {
        /// <summary>
        /// This value define if the character is loaded in the scene.
        /// </summary>
        [ShowInInspector, ReadOnly] public bool IsLoaded { get; set; }
    }
}
