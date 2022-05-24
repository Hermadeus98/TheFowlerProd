using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class MenuCharactersSKHandler : MonoBehaviour
    {
        public static MenuCharactersSKHandler Instance;
        public Animator phoebe, robyn, abi;
        [SerializeField] private GameObject handler;
        public Transform position1, position2,position3, position4;
        public Cinemachine.CinemachineVirtualCamera camMenu, camSkillTree, camInitative;

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
        }

        public void Close()
        {
            handler.SetActive(false);
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

    }
}

