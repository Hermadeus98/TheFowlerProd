using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    [Serializable]
    public class InOutComponent
    {
        [TitleGroup("IN")] public float in_duration = .1f;
        public Ease in_ease = Ease.InSine;
        
        [TitleGroup("BETWEEN")] public float between_duration = .1f;
        
        [TitleGroup("OUT")] public float out_duration = .1f;
        public Ease out_ease = Ease.InSine;
    }
}
