using System;
using System.Linq;
using QRCode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using MoreMountains.Feedbacks;
namespace TheFowler
{
    public class TwoDCutsceneView : UIView
    {
        [TabGroup("References")]
        [SerializeField] private Image cutsceneTwoD;
        public void Show(Sprite overrideSprite)
        {
            base.Show();
            cutsceneTwoD.sprite = overrideSprite;
        }
    }
}

