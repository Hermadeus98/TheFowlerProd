using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class UIBattleBatch : MonoBehaviour
    {
        public static UIBattleBatch Instance;
        private void Awake() => Instance = this;

        public CanvasGroup CanvasGroup;

        public void Show() => CanvasGroup.alpha = 1f;
        public void Hide() => CanvasGroup.alpha = 0f;
    }
}
