using DG.Tweening;
using QRCode.Extensions;
using TheFowler;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TheFowler
{
    public class RappelInput : GameplayMonoBehaviour
    {
        [SerializeField] public Touch Touch;
        [SerializeField] private RappelInputDatabase rappelInputDatabase;
        [SerializeField] private PlayerInput Inputs;
        [SerializeField] private Image touchImage;
        [SerializeField]
        private Image rappelInputFill;
        private Sequence feedbackFade;

        protected override void OnStart()
        {
            base.OnStart();
            touchImage.color.SetAlpha(rappelInputDatabase.initialFade);
            touchImage.sprite = rappelInputDatabase.GetElement(Touch).touchImage;
        }

        private void FixedUpdate()
        {
            if (Inputs.actions["Next"].WasPressedThisFrame())
            {
                Feedback();
            }
        }

        private void Feedback()
        {
            var inout = rappelInputDatabase.InOutFeedback;

            feedbackFade?.Kill();
            feedbackFade = DOTween.Sequence();
            feedbackFade.Append(touchImage.DOFade(1f, inout.in_duration).SetEase(inout.in_ease));
            feedbackFade.Append(touchImage.DOFade( rappelInputDatabase.initialFade, inout.out_duration).SetDelay(inout.between_duration).SetEase(inout.out_ease));
            feedbackFade.Play();
        }

        public void RappelInputFeedback(float elapsedTime)
        {
            rappelInputFill.fillAmount = elapsedTime;
        }
    }
}
