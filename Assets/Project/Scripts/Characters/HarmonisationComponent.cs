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
        [TabGroup("References")]
        [SerializeField] private RectTransform talkRT;
        private bool triggeredHarmo = false;
        private bool canHarmo = false;
        private bool validDist = false;
        private GameplayPhase phase;
        public Transform dest;
        public ActorEnum actor;
        public void LaunchHarmonisation()
        {
            triggeredHarmo = false;
            canHarmo = false;
            harmo.alpha = 0;
            talk.alpha = 0;
            phase.PlayPhase();
            switch (actor)
            {
                case ActorEnum.ABIGAEL:
                    Player.Abigael.Controller.SetController(ControllerEnum.NAV_MESH_FOLLOWER);
                    break;
                case ActorEnum.PHEOBE:
                    Player.Pheobe.Controller.SetController(ControllerEnum.NAV_MESH_FOLLOWER);
                    break;

            }

        }

        public void InitializeHarmonisation(GameplayPhase newPhase, Transform goTo)
        {
            harmo.alpha = 1;
            triggeredHarmo = true;
            phase = newPhase;
            dest = goTo;
        }

        public void ChangeTalkState(bool value)
        {
            if (value)
            {
                if (triggeredHarmo && validDist)
                {
                    talk.alpha = value ? 1 : 0;
                    canHarmo = value ? true : false;
                }
            }
            else
            {
                if (triggeredHarmo )
                {
                    talk.alpha = value ? 1 : 0;
                    canHarmo = value ? true : false;
                }
            }



        }

        public void CheckDistance()
        {
            switch (actor)
            {
                case ActorEnum.ABIGAEL:
                    if (Vector3.Distance(Player.Abigael.pawnTransform.position, dest.position) < 10)
                    {
                        validDist = true;
                    }
                    else
                    {
                        validDist = false;
                    }
                    break;
                case ActorEnum.PHEOBE:
                    if (Vector3.Distance(Player.Pheobe.pawnTransform.position, dest.position) < 10)
                    {
                        validDist = true;
                    }
                    else
                    {
                        validDist = false;
                    }
                    break;

            }
            
        }

        private void CheckInput()
        {
            talkRT.LookAt(Player.Robyn?.transform);

            if (Inputs.actions["Validate"].WasPressedThisFrame())
            {
                LaunchHarmonisation();
            }
        }

        private void CheckDistanceFromCenterOfScreen()
        {
            //Vector2 posAbi = CameraManager.Camera.WorldToViewportPoint(Player.Abigael.transform.position);
            //Vector2 posPhoebe = CameraManager.Camera.WorldToViewportPoint(Player.Pheobe.transform.position);
            //Vector2 centerScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            //disAbi = Vector2.Distance(posAbi, centerScreen);
            //disPhoebe = Vector2.Distance(posPhoebe, centerScreen);

            

        }

        public void Update()
        {
            if (canHarmo)
            {
                CheckInput();
            }

            if (triggeredHarmo)
            {
                CheckDistance();
            }

        }
    }
}

