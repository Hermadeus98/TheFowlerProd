using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
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

        [TabGroup("References")]
        [SerializeField] GameObject triangle;

        public bool isBreakdown;

        private BattleActor battleActor;

        public PlayerInput Inputs;


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

            if (isBreakdown)
            {
                UI.GetView<TargetPickingView>(UI.Views.TargetPicking).Hide();
                triangle.GetComponent<CanvasGroup>().alpha = 0;
            }

        }

        public static List<EnemyActor> enemies = new List<EnemyActor>();
        
        public override void Hide()
        {
            base.Hide();

            if (isBreakdown)
            {
                isBreakdown = false;
            }

            skillSelector.canNavigate = true;
            skillSelector.Hide();

            triangle.GetComponent<CanvasGroup>().alpha = 1;

            GetComponent<RectTransform>().anchoredPosition = basicPosition;
            
            if (enemies.Count > 0)
            {
                enemies.ForEach(delegate(EnemyActor w)
                {
                    w.resist.Hide();
                    w.weak.Hide();
                });
            }
        }

        private void Update()
        {


            //if (isActive)
            //{
            //    if(isBreakdown && TargetSelector.SelectedTargets[0].GetBattleComponent<SpellHandler>() != null)
            //    {
            //        if(battleActor != TargetSelector.SelectedTargets[0])
            //        {
            //            skillSelector.Refresh(TargetSelector.SelectedTargets[0].GetBattleComponent<SpellHandler>());
            //            battleActor = TargetSelector.SelectedTargets[0];
            //        }

            //    }
            //}
        }

        public void ShowTriangle(bool state) => triangle.SetActive(state);
    }
}
