using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class BattleActorManager : MonoBehaviour
    {
        [SerializeField] private BattleActorData[] data;

        private void OnDisable()
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i].Reset();
            }
        }
    }
}

