using System;
using System.Collections;
using System.Collections.Generic;
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

        [TitleGroup("Main Settings")] public int Cooldown;
        [TitleGroup("Main Settings"), HideInInspector ] public int InitialCooldown;
        [TitleGroup("Main Settings")] public int CurrentCooldown;
        [TitleGroup("Main Settings")] public Sprite logoBuff;

        [TitleGroup("Main Settings")] public TargetTypeEnum TargetType;
        [TitleGroup("Main Settings")] public SpellPowerEnum SpellPower;
        
        [TitleGroup("Main Settings"), TextArea(3,5)] 
        public string SpellDescription;
        
        [TitleGroup("Effects")] public SpellTypeEnum SpellType;
        
        [TitleGroup("Effects")] 
        public Effect[] Effects;

        public SequenceEnum sequenceBinding;

        [TitleGroup("Progression")]
        public Sprite sprite;
        [TitleGroup("Progression")]
        [ReadOnly] public SkillState spellState;
        [TitleGroup("Progression")]
        public int unlockOrder;

        [ReadOnly] public bool isRechargingCooldown;

        private void OnEnable()
        {
            Effects.ForEach(w => w.ReferedSpell = this);
            InitialCooldown = Cooldown;
        }

        public IEnumerator Cast(BattleActor emitter, BattleActor[] receivers)
        {
            if (BattleManager.lastTouchedActors == null)
                BattleManager.lastTouchedActors = new List<BattleActor>();
            BattleManager.lastTouchedActors.AddRange(receivers);
            
            yield return new WaitForSeconds(.3f);

            for (int i = 0; i < Effects.Length; i++)
            {
                Effects[i].Emitter = emitter;
                Effects[i].Receivers = new BattleActor[receivers.Length];
                for (int j = 0; j < receivers.Length; j++)
                {
                    Effects[i].Receivers[j] = receivers[j];
                }
                
                yield return Effects[i].OnCast(emitter, receivers);
            }
            
            yield return new WaitForSeconds(.3f);
        }

        private void OnDisable()
        {
            CurrentCooldown = 0;
        }

        public void Reset()
        {
            CurrentCooldown = 0;
            Cooldown = InitialCooldown;
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
