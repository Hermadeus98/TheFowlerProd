using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Nrjwolf.Tools.AttachAttributes;
using UnityEditor.Recorder;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TheFowler
{
    public class ActionPickerElement : UIElement
    {
        [SerializeField] private string actionKey;

        public bool canInput = false;

        [SerializeField] private CanvasGroup CanvasGroup;

        [SerializeField] private PlayerActionType playerActionType;
        public enum PlayerActionType
        {
            NONE,
            SPELL,
            PARRY,
            ATTACK,
            FURY,
        }

        [SerializeField]
        private Image ImageInputSelector;
        
        public bool CheckInput(PlayerInput input, out PlayerActionType playerActionType)
        {
            if (!canInput)
            {
                playerActionType = PlayerActionType.NONE;
                CanvasGroup.alpha = .5f;
                return false;
            }
            else
            {
                CanvasGroup.alpha = 1f;
            }

            if (input.actions[actionKey].WasPressedThisFrame())
            {
                FeedbackOnClick();
                playerActionType = this.playerActionType;
                return true;
            }

            playerActionType = PlayerActionType.NONE;
            return false;
        }

        private Sequence seq;
        protected void FeedbackOnClick()
        {
            seq?.Kill();
            seq = DOTween.Sequence();
            seq.Append(ImageInputSelector.DOColor(Color.gray, .1f));
            seq.Append(ImageInputSelector.DOColor(Color.white, .1f).SetDelay(.1f));
            seq.Play();
        }
    }
}
