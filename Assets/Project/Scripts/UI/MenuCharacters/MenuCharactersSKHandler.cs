using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TheFowler
{
    public class MenuCharactersSKHandler : MonoBehaviour
    {
        public static MenuCharactersSKHandler Instance;

        [TabGroup("References")]
        public Animator phoebe, robyn, abi;
        [TabGroup("References")]
        [SerializeField] private GameObject handler;
        [TabGroup("References")]
        public Cinemachine.CinemachineVirtualCamera camMenu, camSkillTree, camInitative;
        [TabGroup("Menu")]
        public Transform position1, position2, position3;
        [TabGroup("Tree")]
        public Transform positionTreeRobyn, positionTreeAby, positionTreePhoebe;


        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
        }

        public void Initialize()
        {
            handler.SetActive(true);
            phoebe.Rebind();
            robyn.Rebind();
            abi.Rebind();
            Player.canOpenPauseMenu = false;
        }

        public void Close()
        {
            handler.SetActive(false);
            Player.canOpenPauseMenu = true;
        }

        public void DisableEveryone()
        {
            phoebe.gameObject.SetActive(false);
            robyn.gameObject.SetActive(false);
            abi.gameObject.SetActive(false);
        }

        public void InMenu()
        {
            camMenu.enabled = true;
            camSkillTree.enabled = false;
            camInitative.enabled = false;
        }

        public void InInitative()
        {
            camMenu.enabled = false;
            camSkillTree.enabled = false;
            camInitative.enabled = true;
        }

        public void InSkillTree()
        {
            camMenu.enabled = false;
            camSkillTree.enabled = true;
            camInitative.enabled = false;
        }

        public void SetActorTree(int ID)
        {
            switch (ID)
            {
                case 0:
                    Instance.robyn.gameObject.SetActive(true);
                   Instance.robyn.transform.position = Instance.positionTreeRobyn.position;
                    break;
                case 1:
                    Instance.abi.gameObject.SetActive(true);
                    Instance.abi.transform.position = Instance.positionTreeAby.position;
                    break;
                case 2:
                    Instance.phoebe.gameObject.SetActive(true);
                    Instance.phoebe.transform.position = Instance.positionTreePhoebe.position;
                    break;
            }

        }

    }
}

