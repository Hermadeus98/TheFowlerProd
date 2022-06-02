using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu(menuName = CreateAssetMenuPath.battleActorData)]
    public class BattleActorData : SerializedScriptableObject
    {
        [TitleGroup("Identity")] [SerializeField]
        private ActorType dataType;
        public enum ActorType
        {
            ALLY,
            GUARD
        }

        [TitleGroup("Identity")] 
        public string actorName;
        [TitleGroup("Identity")]
        public Sprite sprite;

        [TitleGroup("Identity")] 
        [HideIf("@this.dataType == ActorType.GUARD")] public Spell[] Spells;

        [TitleGroup("Identity")]
        [HideIf("@this.dataType == ActorType.GUARD")] public Spell[] DefaultSpells;

        [TitleGroup("Identity")]
        [HideIf("@this.dataType == ActorType.GUARD")] public BattleActorData defaultData;

        [TitleGroup("Identity")] [ShowIf("@this.dataType == ActorType.GUARD")]
        public EnemyType enemyType = EnemyType.MOB;
        
        [TitleGroup("Identity")] [ShowIf("@this.dataType == ActorType.GUARD")]
        public Archetype archetype = Archetype.NORMAL;



        public enum Archetype
        {
            NORMAL,
            TANK,
            SUPPORT,
            DPS,
        }
        
        public enum EnemyType
        {
            MOB,
            TRASHMOB,
            BOSS
        }
            
         [TitleGroup("Identity")] 
         [HideIf("@this.dataType == ActorType.GUARD")]
         public Spell
            BasicAttackSpell,
            DefendSpell,
            BatonPass,
            FurySpell;

        [TitleGroup("Identity")] public Spell.SpellTypeEnum actorType;
        
        [HideIf("@this.dataType == ActorType.ALLY")]
        [TitleGroup("Identity")] public BehaviourTree brain;

        [HideIf("@this.dataType == ActorType.GUARD")]
        [TitleGroup("Progression")] public Spell[] AllSpells;

        [HideIf("@this.dataType == ActorType.GUARD")]
        [TitleGroup("Progression")] public int complicityLevel = 1;
        
        [HideIf("@this.dataType == ActorType.GUARD")]
        [TitleGroup("Progression")] public int initiativeOrder = 0;

        [HideIf("@this.dataType == ActorType.GUARD")]
        [TitleGroup("Progression")] public bool hasNewSkills;

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
            ChangeState();
        }

        public void ChangeComplicityLevel(int newValue)
        {
            if (newValue != complicityLevel)
            {
                hasNewSkills = true;
            }

            complicityLevel = newValue;



            ChangeState();
        }

        private void ChangeState()
        {
            for (int i = 0; i < AllSpells.Length; i++)
            {
                if(AllSpells[i].unlockOrder <= complicityLevel)
                {
                    if(AllSpells[i].spellState == SkillState.LOCKED)
                    {
                        AllSpells[i].spellState = SkillState.UNEQUIPPED;

                        
                    }
                }
            }
        }

        [Button]
        public void Reset()
        {
            Spells = new Spell[DefaultSpells.Length];

            for (int i = 0; i < Spells.Length; i++)
            {
                Spells[i] = DefaultSpells[i];
                Spells[i].CurrentCooldown = Spells[i].Cooldown;
                Spells[i].isRechargingCooldown = false;
            }

            complicityLevel = 1;

            for (int i = 0; i < AllSpells.Length; i++)
            {
                if (AllSpells[i].unlockOrder < complicityLevel)
                {
                    AllSpells[i].spellState = SkillState.BASIC;
                }
                else
                {
                    AllSpells[i].spellState = SkillState.LOCKED;

                }
            }

            hasNewSkills = false;

        }


    }

    
}
