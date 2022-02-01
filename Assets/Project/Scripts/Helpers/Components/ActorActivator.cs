using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class ActorActivator : SerializedMonoBehaviour
    {
        [SerializeField] private bool desactivateRobyn, desactivateAbigael, desactivatePhoebe;
        [SerializeField] private GameObject[] dialogueActors;
        
        public void ActivateActor()
        {
            SetActorActive(false);
            dialogueActors.ForEach(w => w.SetActive(true));
        }

        public void DesactivateActor()
        {
            SetActorActive(true);
            dialogueActors.ForEach(w => w.SetActive(false));
        }

        private void SetActorActive(bool state)
        {
            if (desactivateRobyn)
            {
                Player.Robyn.gameObject.SetActive(state);
            }
            if (desactivateAbigael)
            {
                Player.Abigael.gameObject.SetActive(state);
            }
            if (desactivatePhoebe)
            {
                Player.Pheobe.gameObject.SetActive(state);
            }
        }
    }
}
