using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class DestructionSystem : SerializedMonoBehaviour
    {
        private class LevelOfDestruction
        {
            public GameObject[] ObjectsToDesactivate;
            public GameObject[] ObjectToActivate;
        }

        [SerializeField] private LevelOfDestruction[] LevelOfDestructions;
        private int iteration;
        [SerializeField] private MMFeedbacks feedback;

        [Button]
        public void ResetDestruction()
        {
            iteration = 0;
            LevelOfDestructions[0].ObjectsToDesactivate.ForEach(w => w.SetActive(false));
            LevelOfDestructions[0].ObjectToActivate.ForEach(w => w.SetActive(true));
        }

        [Button]
        public void Destruct()
        {
            Iterate();
        }

        private void Iterate()
        {
            iteration++;
            
            if(iteration >= LevelOfDestructions.Length)
                return;

            feedback.PlayFeedbacks();
            LevelOfDestructions[iteration].ObjectsToDesactivate.ForEach(w => w.SetActive(false));
            LevelOfDestructions[iteration].ObjectToActivate.ForEach(w => w.SetActive(true));
        }
    }
}