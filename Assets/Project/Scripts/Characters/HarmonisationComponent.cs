using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace TheFowler
{
    public class HarmonisationComponent : MonoBehaviour
    {

        [TabGroup("References")]
        public CanvasGroup harmo, talk;
        [TabGroup("References")]
        [SerializeField] private PlayerInput Inputs;

        private bool triggeredHarmo = false;
        private bool canHarmo = false;
        private GameplayPhase phase;
        private float disAbi, disPhoebe;
        public void LaunchHarmonisation()
        {
            triggeredHarmo = false;
            canHarmo = false;
            harmo.alpha = 0;
            talk.alpha = 0;
            phase.PlayPhase();

        }

        public void InitializeHarmonisation(GameplayPhase newPhase)
        {
            harmo.alpha = 1;
            triggeredHarmo = true;
            phase = newPhase;
        }

        public void ChangeTalkState(bool value)
        {
            if (triggeredHarmo)
            {
                talk.alpha = value ? 1 : 0;
                canHarmo = value ? true : false;
            }


        }

        private void CheckInput()
        {
            if (Inputs.actions["Validate"].WasPressedThisFrame())
            {
                LaunchHarmonisation();
            }
        }

        //private void CheckDistanceFromCenterOfScreen()
        //{
        //    Vector2 posAbi = CameraManager.Camera.WorldToScreenPoint(Player.Abigael.transform.position);
        //    Vector2 posPhoebe = CameraManager.Camera.WorldToScreenPoint(Player.Pheobe.transform.position);
        //    Vector2 centerScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        //    disAbi = Vector2.Distance(posAbi, centerScreen);
        //    disPhoebe = Vector2.Distance(posAbi, centerScreen);

        //}

        public void Update()
        {
            if (canHarmo)
            {
                CheckInput();
            }

        }
    }
}

