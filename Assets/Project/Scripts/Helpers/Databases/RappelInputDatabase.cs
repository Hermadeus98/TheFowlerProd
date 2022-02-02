using System;
using System.Collections;
using System.Collections.Generic;
using QRCode;
using UnityEngine;

namespace TheFowler
{
    [CreateAssetMenu(menuName = CreateAssetMenuPath.rappelInputDB)]
    public class RappelInputDatabase : Database<Touch, TouchInfosBD>
    {
        public InOutComponent InOutFeedback;
        [Range(0, 1)] public float initialFade = .65f;
    }

    [Serializable]
    public class TouchInfosBD
    {
        public Sprite touchImage;
    }

    public enum Touch
    {
        A,
        B,
        X,
        Y,
        LEFT_TRIGGER,
        LEFT_BUMPER,
        RIGHT_TRIGGER,
        RIGHT_BUMPER,
        PAUSE,
        OPTION,
        ARROW_RIGHT,
        ARROW_UP,
        ARROW_DOWN,
        ARROW_LEFT,
    }
}
