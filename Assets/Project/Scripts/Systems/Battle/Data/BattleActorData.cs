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

        [TitleGroup("Identity")] public Spell
            BasicAttackSpell,
            DefendSpell,
            BatonPass;

        [TitleGroup("Identity")] public Spell.SpellTypeEnum actorType;

        [TitleGroup("Identity")] public int complicityLevel = 1;

        [TitleGroup("Data Binding")]
        public enum BindingType{DEFAULT,REMOTE_SETTINGS}
        public BindingType bindingType = BindingType.DEFAULT;

        [TitleGroup("Data Binding"), ShowIf("@this.bindingType == BindingType.REMOTE_SETTINGS")]
        public string datakey_default = "ActorData_Default";
        [TitleGroup("Data Binding"), ShowIf("@this.bindingType == BindingType.REMOTE_SETTINGS")]
        public string datakey_easy = "ActorData_Easy";
        [TitleGroup("Data Binding"), ShowIf("@this.bindingType == BindingType.REMOTE_SETTINGS")]
        public string datakey_medium = "ActorData_Medium";
        [TitleGroup("Data Binding"), ShowIf("@this.bindingType == BindingType.REMOTE_SETTINGS")]
        public string datakey_hard = "ActorData_Hard";
        
        [TitleGroup("Data Binding"), ShowIf("@this.bindingType == BindingType.DEFAULT")]
        public float health = 15.5f;
        [TitleGroup("Data Binding"), ShowIf("@this.bindingType == BindingType.DEFAULT")]
        public int mana = 3;

        public void AddComplicity(int addValue)
        {
            complicityLevel += addValue;
        }

        public void ChangeComplicityLevel(int newValue)
        {
            complicityLevel = newValue;
        }
    }

    
}
