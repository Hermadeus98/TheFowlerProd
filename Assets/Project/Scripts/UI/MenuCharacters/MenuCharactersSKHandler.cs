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

    }
}

