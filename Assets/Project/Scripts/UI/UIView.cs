using DG.Tweening;
using Nrjwolf.Tools.AttachAttributes;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIView : UIElement
    {
        [TabGroup("Main Settings")]
        [SerializeField] private bool registerView;
        [TabGroup("Main Settings")]
        [SerializeField, EnableIf("@this.registerView == true")] private string viewName;

        [TabGroup("Main Settings")] [SerializeField]
        protected bool useDefaultAnimShow = true;
        [TabGroup("Main Settings")] [SerializeField]
        protected bool useDefaultAnimHide = true;

        [TabGroup("References")]
        [SerializeField, GetComponent] private CanvasGroup canvasGroup;

        protected CanvasGroup CanvasGroup
        {
            get
            {
                if (canvasGroup.IsNull())
                    canvasGroup = GetComponent<CanvasGroup>();

                return canvasGroup;
            }
        }
        
        public string ViewName => viewName;

        protected Tween openTween;

        public override void Show()
        {
            base.Show();
            if (useDefaultAnimShow)
            {
                openTween = CanvasGroup.DOFade(1f, .1f);
                CanvasGroup.interactable = true;
                CanvasGroup.blocksRaycasts = true;
            }
        }

        public override void Hide()
        {
            base.Hide();
            if (useDefaultAnimHide)
            {
                openTween = CanvasGroup.DOFade(0f, .1f);
                CanvasGroup.interactable = false;
                CanvasGroup.blocksRaycasts = true;
            }
        }

        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            if(registerView) UI.RegisterView(this);
        }

        protected override void UnregisterEvent()
        {
            base.UnregisterEvent();
            if(registerView) UI.UnregisterView(this);
        }
    }
}
