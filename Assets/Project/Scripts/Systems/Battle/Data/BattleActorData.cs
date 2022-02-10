using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu(menuName = CreateAssetMenuPath.battleActorData)]
    public class BattleActorData : SerializedScriptableObject
    {
        [TitleGroup("Identity")] 
        public string actorName;

        [TitleGroup("Identity")] 
        public Spell[] Spells;
    }
}
