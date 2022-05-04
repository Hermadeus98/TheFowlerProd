using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheFowler
{
    public class SkillPickingView : UIView
    {
        [TabGroup("References")] [SerializeField]
        public SkillSelector skillSelector;

        [TabGroup("References")] [SerializeField]
        public TextMeshProUGUI descriptionText;

        [TabGroup("References")]
        [SerializeField]
        public GameObject descriptionBox;

        [TabGroup("References")]
        [SerializeField]
        public TextMeshProUGUI easyDescriptionText, targetDescription, previewText;
        [TabGroup("References")]
        [SerializeField]
        public Vector2 basicPosition, breakdownPosition;


        private bool isBreakdown;

        private BattleActor battleActor;


        public override void Show()
        {
            base.Show();
            if (BattleManager.IsReducingCD)
            {
                previewText.gameObject.SetActive(true);
            }
            else
            {
                previewText.gameObject.SetActive(false);
            }


            skillSelector.Refresh(BattleManager.CurrentBattleActor.GetBattleComponent<SpellHandler>());
            skillSelector.Show();

            //GetComponent<RectTransform>().anchoredPosition = basicPosition;
        }

        public void Show(bool _isBreakdown)
        {
            base.Show();
            skillSelector.Show();
            isBreakdown = _isBreakdown;

            GetComponent<RectTransform>().anchoredPosition = breakdownPosition;
        }

        public override void Hide()
        {
            base.Hide();

            if (isBreakdown)
            {
                isBreakdown = false;
                skillSelector.Hide();
            }

            skillSelector.canNavigate = true;
            skillSelector.Hide();

            GetComponent<RectTransform>().anchoredPosition = basicPosition;
        }

        private void Update()
        {
            if (isActive)
            {
                if(isBreakdown && TargetSelector.SelectedTargets[0].GetBattleComponent<SpellHandler>() != null)
                {
                    if(battleActor != TargetSelector.SelectedTargets[0])
                    {
                        skillSelector.Refresh(TargetSelector.SelectedTargets[0].GetBattleComponent<SpellHandler>());
                        battleActor = TargetSelector.SelectedTargets[0];
                    }

                }
            }
        }
    }
}
