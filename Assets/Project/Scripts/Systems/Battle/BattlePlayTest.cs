using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class BattlePlayTest : SerializedMonoBehaviour
    {
        public Battle battle;
        public GameObject graphisme;

        [Button]
        void Play()
        {
            graphisme.SetActive(true);
            battle.gameObject.SetActive(true);
            battle.PlayPhase();
        }
    }
}
