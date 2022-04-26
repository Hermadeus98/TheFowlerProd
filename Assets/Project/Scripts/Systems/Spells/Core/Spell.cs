using System;
using System.Collections;
using System.Linq;
using QRCode.Utils;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu(menuName = CreateAssetMenuPath.spell)]
    public class Spell : SerializedScriptableObject
    {
        [TitleGroup("Main Settings")]
        public string SpellName;
        
        [TitleGroup("Main Settings")]
        public int ManaCost;

        [TitleGroup("Main Settings")] public int Cooldown;
        [TitleGroup("Main Settings")] public int CurrentCooldown;
        [TitleGroup("Main Settings")] public Sprite logoBuff;

        [TitleGroup("Main Settings")] public TargetTypeEnum TargetType;
        [TitleGroup("Main Settings")] public SpellPowerEnum SpellPower;
        
        [TitleGroup("Main Settings"), TextArea(3,5)] 
        public string SpellDescription;

        [TitleGroup("Main Settings"), TextArea(3, 5)]
        public string TargetDescription, EasySpellDescription;
        
        [TitleGroup("Effects")] public SpellTypeEnum SpellType;
        
        [TitleGroup("Effects")] 
        public Effect[] Effects;

        public SequenceEnum sequenceBinding;

        private BattleActor[] receiversReminder { get; set; }


        [TitleGroup("Progression")]
        public Sprite sprite;
        [TitleGroup("Progression")]
        public SkillState spellState;
        [TitleGroup("Progression")]
        public int unlockOrder;

        [ReadOnly] public bool isRechargingCooldown;

        private void OnEnable()
        {
            Effects.ForEach(w => w.ReferedSpell = this);
        }

        public IEnumerator Cast(BattleActor emitter, BattleActor[] receivers)
        {
            yield return new WaitForSeconds(.3f);

            for (int i = 0; i < Effects.Length; i++)
            {
                yield return Effects[i].OnCast(emitter, receivers);
            }
            
            yield return new WaitForSeconds(.3f);
        }

        public void SimpleCast(BattleActor emitter, BattleActor[] receivers)
        {
            Effects.ForEach(w => w.OnSimpleCast(emitter, receivers));
        }

        private void OnDisable()
        {
            CurrentCooldown = 0;
        }

        public bool ContainEffect<T>(out T component) where T : Effect
        {
            for (int i = 0; i < Effects.Length; i++)
            {
                if (Effects[i] is T)
                {
                    component = Effects[i] as T;
                    return true;
                }
            }

            component = null;
            return false;
        }

        public enum SpellTypeEnum
        {
            NULL = 0,
            CLAW = 1,
            BEAK = 2,
            FEATHER = 3,
        }

        public enum SpellPowerEnum
        {
            NULL = 0,
            EASY = 1,
            MEDIUM = 2,
            HARD = 3
        }
    }
}
