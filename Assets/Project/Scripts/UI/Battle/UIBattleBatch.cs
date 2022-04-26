using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.Utilities;
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

        public static void SetUIGuardsVisibility(bool state)
        {
            if(state)
            {
                BattleManager.CurrentBattle.Enemies.Cast<EnemyActor>().ForEach(w => w.UI.DOFade(1f, .1f));
            }
            else
            {
                BattleManager.CurrentBattle.Enemies.Cast<EnemyActor>().ForEach(w => w.UI.DOFade(0f, .1f));
            }
        }
    }
}
