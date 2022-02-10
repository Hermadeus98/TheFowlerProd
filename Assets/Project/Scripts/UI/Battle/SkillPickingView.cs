using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class SkillPickingView : UIView
    {
        [TabGroup("References")] [SerializeField]
        private SkillSelector skillSelector;

        public override void Show()
        {
            base.Show();
            skillSelector.Refresh(BattleManager.CurrentBattleActor.BattleActorData);
            skillSelector.Show();
        }

        public override void Hide()
        {
            base.Hide();
            skillSelector.Hide();
        }
    }
}
