using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRCode;

namespace TheFowler
{
    public class MenuPauseHandler : MonoBehaviourSingleton<MenuPauseHandler>
    {
        [SerializeField] private GameObject handler;
        [SerializeField] private Transform position1, position2, position3;
        [SerializeField] private Transform Robyn, Phoebe, Aby;
        [SerializeField] private NumberOfAllies numberOfAlliesSO;

        public void Initialize()
        {
            handler.SetActive(true);
            CheckNumberOfAllies();
            ReplaceActors();

        }

        public void Close()
        {
            handler.SetActive(false);
        }

        public void ReplaceActors()
        {
            Robyn.transform.position = position1.position;
            Aby.transform.position = position2.position;
            Phoebe.transform.position = position3.position;
        }

        public void CheckNumberOfAllies()
        {
            if(numberOfAlliesSO.numberOfAllies == 3) 
            {
                Phoebe.gameObject.SetActive(true);
            }
            else
            {
                Phoebe.gameObject.SetActive(false);
            }
        }
    }
}

