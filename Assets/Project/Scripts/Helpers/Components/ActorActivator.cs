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
        [SerializeField] private GameObject[] ActorsToActivateParents;
        
        public void ActivateActor()
        {
            SetActorActive(false);
            for (int i = 0; i < ActorsToActivateParents.Length; i++)
            {
                for (int a = 0; a < ActorsToActivateParents[i].transform.childCount; a++)
                {
                    ActorsToActivateParents[i].transform.GetChild(a).gameObject.SetActive(true);
                }
            }
            //ActorsToActivate.ForEach(w => w.SetActive(true));
        }

        public void DesactivateActor()
        {
            SetActorActive(true);
            for (int i = 0; i < ActorsToActivateParents.Length; i++)
            {
                for (int a = 0; a < ActorsToActivateParents[i].transform.childCount; a++)
                {
                    ActorsToActivateParents[i].transform.GetChild(a).gameObject.SetActive(true);
                }
            }
            //ActorsToActivate.ForEach(w => w.SetActive(false));
        }

        private void SetActorActive(bool state)
        {
            if (desactivateRobyn)
            {
                Player.Robyn?.gameObject.SetActive(state);
            }
            if (desactivateAbigael)
            {
                Player.Abigael?.gameObject.SetActive(state);
            }
            if (desactivatePhoebe)
            {
                Player.Pheobe?.gameObject.SetActive(state);
            }
        }
    }
}
