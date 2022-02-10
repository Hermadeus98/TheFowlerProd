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
        
        [TitleGroup("Main Settings")] 
        public ExecutionTypeEnum ExecutionType;
        public enum ExecutionTypeEnum
        {
            SIMULTANEOUS,
            CONSECUTIVE,
        }

        [TitleGroup("Effects")] 
        public Effect[] Effects;
        

        public IEnumerator Cast()
        {
            switch (ExecutionType)
            {
                case ExecutionTypeEnum.SIMULTANEOUS:
                    for (int i = 0; i < Effects.Length; i++)
                    {
                        Coroutiner.Play(Effects[i].OnBeginCast());
                        Coroutiner.Play(Effects[i].OnCast());
                        Coroutiner.Play(Effects[i].OnFinishCast());
                    }
                    break;
                case ExecutionTypeEnum.CONSECUTIVE:
                    for (int i = 0; i < Effects.Length; i++)
                    {
                        yield return Effects[i].OnBeginCast();
                        yield return Effects[i].OnCast();
                        yield return Effects[i].OnFinishCast();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            yield break;
        }
    }
}
