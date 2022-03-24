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

        [TitleGroup("Main Settings")] public TargetTypeEnum TargetType;

        [TitleGroup("Main Settings")] public float
            executionDurationBeforeCast = .3f,
            executionDurationAfterCast = .3f;
        
        [TitleGroup("Main Settings"), TextArea(3,5)] 
        public string SpellDescription;

        [TitleGroup("Main Settings"), TextArea(3, 5)]
        public string TargetDescription, EasySpellDescription;
        
        [TitleGroup("Main Settings")] 
        public ExecutionTypeEnum ExecutionType;
        public enum ExecutionTypeEnum
        {
            SIMULTANEOUS,
            CONSECUTIVE,
        }

        [TitleGroup("Effects")] public SpellTypeEnum SpellType;
        
        [TitleGroup("Effects")] 
        public Effect[] Effects;

        public SequenceEnum sequenceBinding;

        private void OnEnable()
        {
            Effects.ForEach(w => w.ReferedSpell = this);
        }

        public IEnumerator Cast(BattleActor emitter, BattleActor[] receivers)
        {
            yield return new WaitForSeconds(executionDurationBeforeCast);

            switch (ExecutionType)
            {
                case ExecutionTypeEnum.SIMULTANEOUS:
                    for (int i = 0; i < Effects.Length; i++)
                    {
                        if(Effects[i].GetType() != typeof(BatonPassEffect))
                            Fury.StopFury();
                        
                        Effects[i].SetCamera();
                        yield return new WaitForSeconds(.3f);
                        Coroutiner.Play(Effects[i].OnBeginCast(emitter, receivers));
                        
                        Coroutiner.Play(Effects[i].OnCast(emitter, receivers));
                        emitter.FeedbackHandler.PlayFeedback(Effects[i].eventName);

                        Coroutiner.Play(Effects[i].OnFinishCast(emitter, receivers));
                    }
                    break;
                case ExecutionTypeEnum.CONSECUTIVE:
                    for (int i = 0; i < Effects.Length; i++)
                    {
                        if(Effects[i].GetType() != typeof(BatonPassEffect))
                            Fury.StopFury();
                        
                        Effects[i].SetCamera();
                        yield return Effects[i].OnBeginCast(emitter, receivers);
                        
                        yield return Effects[i].OnCast(emitter, receivers);
                        emitter.FeedbackHandler.PlayFeedback(Effects[i].eventName);

                        yield return Effects[i].OnFinishCast(emitter, receivers);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            yield return new WaitForSeconds(executionDurationAfterCast);
        }

        public void SimpleCast(BattleActor emitter, BattleActor[] receivers)
        {
            Effects.ForEach(w => w.OnSimpleCast(emitter, receivers));
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
    }
}
