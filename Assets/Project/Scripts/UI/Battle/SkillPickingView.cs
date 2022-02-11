using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class SkillPickingView : UIView
    {
        [TabGroup("References")] [SerializeField]
        public SkillSelector skillSelector;

        [TabGroup("References")] [SerializeField]
        public AnimatedText descriptionText;

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
