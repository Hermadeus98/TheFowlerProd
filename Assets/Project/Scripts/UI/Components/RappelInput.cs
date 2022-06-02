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
        [SerializeField] private Color touchColor;
        [SerializeField] private TMPro.TextMeshProUGUI label;
        [SerializeField]
        private Image rappelInputFill;
        private Sequence feedbackFade;

        [SerializeField] private Image back;

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



            if(elapsedTime <= 0)
            {
                touchImage.color = Color.white;
                label.color = Color.white;

                touchImage.rectTransform.localScale = new Vector3(1f, 1f, 1);
            }
            else
            {

                touchImage.color = touchColor;
                label.color = touchColor;
                touchImage.rectTransform.localScale = new Vector3(.9f, .9f, 1);
            }

            if (elapsedTime > .95f)
            {
                OnComplete();
            }
        }

        private bool isComplete = false;

        public void ResetRappelInput()
        {
            isComplete = false;
            back.rectTransform.localScale = Vector3.one;
            back.DOFade(1f, 0.01f);


        }

        public void OnComplete()
        {
            if (isComplete)
                return;
            
            isComplete = true;
            
            back.rectTransform.DOScale(1.7f, .2f).SetEase(Ease.OutSine);
            back.DOFade(0f, .2f).SetEase(Ease.OutSine).OnComplete(ResetRappelInput);
        }
    }
}
