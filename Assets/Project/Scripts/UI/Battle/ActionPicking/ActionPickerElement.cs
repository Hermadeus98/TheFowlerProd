using System.Collections;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

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
                playerActionType = this.playerActionType;
                return true;
            }

            playerActionType = PlayerActionType.NONE;
            return false;
        }
    }
    
}
