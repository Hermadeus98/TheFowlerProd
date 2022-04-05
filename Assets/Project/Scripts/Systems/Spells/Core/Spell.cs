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

        BattleActor[] receiversReminder;
        private void OnEnable()
        {
            Effects.ForEach(w => w.ReferedSpell = this);
        }

        public IEnumerator Cast(BattleActor emitter, BattleActor[] receivers)
        {
            yield return new WaitForSeconds(executionDurationBeforeCast);

            receiversReminder = new BattleActor[0];

            switch (ExecutionType)
            {
                case ExecutionTypeEnum.SIMULTANEOUS:
                    for (int i = 0; i < Effects.Length; i++)
                    {
                        if(Effects[i].GetType() != typeof(BatonPassEffect))
                            Fury.StopBreakDown();
                        
                        if(i == 0)
                        {
                            Effects[i].SetCamera();
                            receiversReminder = new BattleActor[receivers.Length];
                            for (int j = 0; j < receivers.Length; j++)
                            {
                                receiversReminder[j] = receivers[j];
                            }


                        }

                        else
                        {
                            switch (Effects[i].TargetType)
                            {
                                case TargetTypeEnum.SELF:
                                    receivers = new BattleActor[1];
                                    receivers[0] = BattleManager.CurrentBattleActor;
                                    break;
                                case TargetTypeEnum.ALL_ENEMIES:
                                    receivers = new BattleActor[BattleManager.CurrentBattle.Enemies.Count];
                                    for (int j = 0; j < BattleManager.CurrentBattle.Enemies.Count; j++)
                                    {
                                        receivers[j] = BattleManager.CurrentBattle.Enemies[j];
                                    }
                                    break;
                                case TargetTypeEnum.ALL_ALLIES:
                                    receivers = new BattleActor[BattleManager.CurrentBattle.Allies.Count];
                                    for (int j = 0; j < BattleManager.CurrentBattle.Allies.Count; j++)
                                    {
                                        receivers[j] = BattleManager.CurrentBattle.Allies[j];
                                    }
                                    break;
                                default:
                                    receivers = receiversReminder;
                                    break;


                            }
                        }
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
                            Fury.StopBreakDown();
                        
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
            receiversReminder = new BattleActor[0];
            Effects[0].SetCamera();

            for (int i = 0; i < Effects.Length; i++)
            {
                if (i == 0)
                {
                    
                    receiversReminder = new BattleActor[receivers.Length];
                    for (int j = 0; j < receivers.Length; j++)
                    {
                        receiversReminder[j] = receivers[j];
                    }

                }
                else
                {
                    switch (Effects[i].TargetType)
                    {
                        case TargetTypeEnum.SELF:
                            receivers = new BattleActor[1];
                            receivers[0] = BattleManager.CurrentBattleActor;
                            break;
                        case TargetTypeEnum.ALL_ENEMIES:
                            receivers = new BattleActor[BattleManager.CurrentBattle.Enemies.Count];
                            for (int j = 0; j < BattleManager.CurrentBattle.Enemies.Count; j++)
                            {
                                receivers[j] = BattleManager.CurrentBattle.Enemies[j];
                            }
                            break;
                        case TargetTypeEnum.ALL_ALLIES:
                            receivers = new BattleActor[BattleManager.CurrentBattle.Allies.Count];
                            for (int j = 0; j < BattleManager.CurrentBattle.Allies.Count; j++)
                            {
                                receivers[j] = BattleManager.CurrentBattle.Allies[j];
                            }
                            break;
                        default:
                            receivers = receiversReminder;
                            break;


                    }
                }

                Effects[i].OnSimpleCast(emitter, receivers);
            }

            
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
