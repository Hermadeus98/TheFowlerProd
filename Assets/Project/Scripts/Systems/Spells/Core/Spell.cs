using System;
using System.Collections;
using QRCode.Utils;
using Sirenix.OdinInspector;
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

        [TitleGroup("Main Settings")] public float executionDuration = 2f;
        
        [TitleGroup("Main Settings"), TextArea(3,5)] 
        public string SpellDescription;
        
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
        
        public IEnumerator Cast()
        {
            yield return new WaitForSeconds(executionDuration);

            switch (ExecutionType)
            {
                case ExecutionTypeEnum.SIMULTANEOUS:
                    for (int i = 0; i < Effects.Length; i++)
                    {
                        Effects[i].SetCamera();
                        Coroutiner.Play(Effects[i].OnBeginCast());
                        Coroutiner.Play(Effects[i].OnCast());
                        Coroutiner.Play(Effects[i].OnFinishCast());
                    }
                    break;
                case ExecutionTypeEnum.CONSECUTIVE:
                    for (int i = 0; i < Effects.Length; i++)
                    {
                        Effects[i].SetCamera();
                        yield return Effects[i].OnBeginCast();
                        yield return Effects[i].OnCast();
                        yield return Effects[i].OnFinishCast();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
