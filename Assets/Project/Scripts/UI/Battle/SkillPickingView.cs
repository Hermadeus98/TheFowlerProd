using System.Collections.Generic;
using DG.Tweening;
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

        [SerializeField] private Image triangleImage;

        public bool isBreakdown;

        private BattleActor battleActor;

        public PlayerInput Inputs;

        private Tween triTween;
        
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

            triTween?.Kill();
            triTween = triangleImage.DOFade(1f, .2f).SetDelay(.8f);
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

            GetComponent<RectTransform>().anchoredPosition = basicPosition;
            
            if (enemies.Count > 0)
            {
                enemies.ForEach(delegate(EnemyActor w)
                {
                    w.resist.Hide();
                    w.weak.Hide();
                });
            }
            
            triTween?.Kill();
            triTween = triangleImage.DOFade(0f, .2f);
        }

        public void ShowTriangle(bool state) => triangle.SetActive(state);
    }
}
